using AutoMapper;
using Microsoft.EntityFrameworkCore;
using XDev_Model;
using XDev_Model.Entities;
using XDev_Model.Interfaces;
using XDev_UnitWork.Custom;
using XDev_UnitWork.DTO;
using XDev_UnitWork.DTO.SaleOrder;
using XDev_UnitWork.Interfaces;
using Microsoft.Reporting.NETCore;
using static NPOI.HSSF.Util.HSSFColor;
using XDev_UnitWork.DTO.Material;
using NPOI.SS.Formula.Functions;

namespace XDev_UnitWork.Business
{
    public class SaleOrderBL : GenericBL<ISaleOrderRep>, ISaleOrderBL
    {
        private readonly IWebHostEnvironment env;

        public SaleOrderBL(ApplicationDbContext dbContext,
                           IHttpContextAccessor contextAccessor,
                           IMapper mapper,
                           IWebHostEnvironment env) : base(dbContext, contextAccessor, mapper)
        {
            this.env = env;
        }

        public Task<bool> AnyAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AnyAsync(string code)
        {
            throw new NotImplementedException();
        }

        public async Task CreateAsync(SaleOrderDTO dto)
        {
            var sotype = await DbContext.SaleOrderType.FirstOrDefaultAsync(f => f.Id == dto.SaleOrderTypeId);

            var numSO = UtilsExtension.NumberNextRange(sotype.RangeId, DbContext.Database.GetConnectionString());

            var model = Mapper.Map<SaleOrder>(dto);
            model.Number = numSO.ToString();

            await Repository.CreateAsync(model);

            // Bloquear Inventario
            try
            {

                var sot = DbContext.SaleOrderType.AsNoTracking().FirstOrDefault(f => f.Id == dto.SaleOrderTypeId);
                var mIds = dto.Positions.Select(p => p.MaterialId).ToList<Guid>();
                var queryWH = await (from mwh in DbContext.MaterialWareHouse.AsNoTracking()
                                     where mwh.BranchId == dto.BranchId && mIds.Contains(mwh.MaterialId)
                                     select mwh).ToListAsync();

                foreach (var wh in queryWH)
                {
                    var pos = dto.Positions.FirstOrDefault(x => x.WareHouseId == wh.WareHouseId && x.MaterialId == wh.MaterialId);
                    if (pos is not null)
                    {
                        // Reduce inventario
                        if (sot.Inventory == "R")
                        {
                            wh.Stock -= pos.Quantity;
                            wh.LockedStock += pos.Quantity;
                        }

                        // Incrementa inventario
                        if (sot.Inventory == "I")
                        {
                            wh.InTransitStock += pos.Quantity;
                        }

                        DbContext.MaterialWareHouse.Update(wh);
                        await DbContext.SaveChangesAsync();
                    }
                }                
            }
            catch (Exception)
            {

            }
        }

        public async Task DeleteAsync(Guid id)
        {
            var transaction = await DbContext.Database.BeginTransactionAsync();

            try
            {
                var model = await DbContext.SaleOrder.Include(i => i.Positions).FirstOrDefaultAsync(f => f.Id == id);
                if (model is not null)
                {
                    if (model.Invoiced)
                        throw new CustomTogoException("Pedido de venta ya facturado, no se puede eliminar");

                    var mIds = model.Positions.Select(p => p.MaterialId).ToList<Guid>();
                    var entity = model;
                    var sot = DbContext.SaleOrderType.AsNoTracking().FirstOrDefault(f => f.Id == model.SaleOrderTypeId);

                    DbContext.SaleOrder.Remove(model);
                    await DbContext.SaveChangesAsync();

                    
                    var queryWH = await (from mwh in DbContext.MaterialWareHouse.AsNoTracking()
                                         where mwh.BranchId == entity.BranchId && mIds.Contains(mwh.MaterialId)
                                         select mwh).ToListAsync();

                    foreach (var pos in entity.Positions)
                    {
                        if (pos.MaterialTypeCode == "B")
                        {
                            var wh = queryWH.FirstOrDefault(f => f.MaterialId == pos.MaterialId && f.WareHouseId == pos.WareHouseId);
                            if (wh is not null)
                            {
                                // Reduce inventario
                                if (sot.Inventory == "R")
                                {
                                    wh.Stock += pos.Quantity;
                                    wh.LockedStock -= pos.Quantity;
                                }

                                // Incrementa inventario
                                if (sot.Inventory == "I")
                                {
                                    wh.InTransitStock -= pos.Quantity;
                                }

                                DbContext.MaterialWareHouse.Update(wh);
                                await DbContext.SaveChangesAsync();
                            }
                        }
                    }

                    transaction.Commit();
                }
            }
            catch (Exception)
            {

            }
        }

        public async Task<SaleOrderDTO> GetByIdAsync(params object[] ids)
        {
            var model = await Repository.GetFirstorDefaultAsync(f => f.Id == Guid.Parse(ids[0].ToString()),
                                                                include: s => s.Include(i => i.Positions.OrderBy(o => o.Position))
                                                                                .ThenInclude(t => t.Conditions.OrderBy(o => o.Position))
                                                                               .Include(i => i.SaleOrderSporadicPartner));
            if (model is null)
                return new SaleOrderDTO();

            if (model.PartnerId is null)
                model.Sporadic = true;

            var dto = Mapper.Map<SaleOrderDTO>(model);

            if (model.PartnerId != Guid.Empty)
            {
                var partner = await DbContext.Partner.FirstOrDefaultAsync(f => f.Id == model.PartnerId);
                if (partner is not null)
                {
                    dto.PartnerCode = partner.Code;
                    dto.PartnerName = partner.Name;
                }
            }

            return dto;
        }

        public Task<List<SaleOrderDTO>> GetListAsync(PaginationDTO pagination)
        {
            throw new NotImplementedException();
        }

        public async Task<List<SaleOrderListDTO>> GetSOListAsync(PaginationDTO pagination)
        {
            var query = (from so in DbContext.SaleOrder.AsNoTracking()
                         join sot in DbContext.SaleOrderType.AsNoTracking() on so.SaleOrderTypeId equals sot.Id
                         join cur in DbContext.Currency.AsNoTracking() on so.CurrencyId equals cur.Id
                         join co in DbContext.Company.AsNoTracking() on so.CompanyId equals co.Id
                         join pa in DbContext.Partner.AsNoTracking() on so.PartnerId equals pa.Id into partners
                         from pas in partners.DefaultIfEmpty()

                         select new SaleOrderListDTO
                         {
                             Id = so.Id,
                             Number = so.Number,
                             Date = so.Date,
                             SaleOrderType = sot.Name,
                             PartnerCode = pas.Code,
                             NetAmount = so.NetAmount,
                             TaxAmount = so.TaxAmount,
                             CurrencyCode = cur.Code,
                             CompanyCode = co.Code,
                             RefDocument = so.RefDocument,
                             Delivered = so.Delivered,
                             Invoiced = so.Invoiced,
                         }
                         ).AsQueryable();

            query = query.CreateFilterAndOrder(pagination);
            return await query.CreatePaging<SaleOrderListDTO, SaleOrderListDTO>(pagination, ContextAccessor.HttpContext);
        }

        public Task<List<SaleOrderDTO>> GetListAsync()
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(SaleOrderDTO dto)
        {
            var model = await Repository.GetFirstorDefaultAsync(f => f.Id == dto.Id,
                                                                include: s => s.Include(i => i.Positions).ThenInclude(t => t.Conditions)
                                                                               .Include(i => i.SaleOrderSporadicPartner));

            if (model is not null)
            {
                if (model.Invoiced)
                    throw new CustomTogoException("Pedido de venta ya fue facturado");

                try
                {
                    model.RefDate = dto.RefDate;
                    model.RefDocument = dto.RefDocument;
                    model.Sporadic = dto.Sporadic;
                    model.SaleOrderSporadicPartner = Mapper.Map<SaleOrderSporadicPartner>(dto.SaleOrderSporadicPartner);
                    //model.Date = dto.Date;
                    //model.SaleOrderTypeId = dto.SaleOrderTypeId;
                    //model.CompanyId = dto.CompanyId;
                    //model.BranchId = dto.BranchId;
                    model.PointSaleId = dto.PointSaleId;
                    model.CurrencyId = dto.CurrencyId;
                    model.PartnerId = dto.Sporadic ? null : dto.PartnerId;
                    model.TaxAmount = dto.TaxAmount;
                    model.NetAmount = dto.NetAmount;
                    model.Per1 = dto.Per1;
                    model.Ret1 = dto.Ret1;
                    model.Ret10 = dto.Ret10;

                    var sot = await DbContext.SaleOrderType.AsNoTracking().FirstOrDefaultAsync(f => f.Id == model.SaleOrderTypeId);
                    var mIds = dto.Positions.Select(p => p.MaterialId).ToList<Guid>();
                    var wareHouse = await (from mwh in DbContext.MaterialWareHouse.AsNoTracking()
                                           where mwh.BranchId == dto.BranchId && mIds.Contains(mwh.MaterialId)
                                           select mwh).ToListAsync();


                    var newPos = new List<SaleOrderPosition>();

                    foreach (var pos in dto.Positions)
                    {
                        var wh = wareHouse.FirstOrDefault(f => f.WareHouseId == pos.WareHouseId && f.MaterialId == pos.MaterialId);
                        var modelpos = model.Positions.FirstOrDefault(f => f.Id == pos.Id);
                        if (modelpos is null)
                        {
                            modelpos = Mapper.Map<SaleOrderPosition>(pos);
                            newPos.Add(modelpos);

                            if (wh is not null)
                            {
                                // Reduce inventario
                                if (sot.Inventory == "R")
                                {
                                    wh.Stock -= pos.Quantity;
                                    wh.LockedStock += pos.Quantity;
                                }

                                // Incrementa inventario
                                if (sot.Inventory == "I")
                                {
                                    wh.InTransitStock += pos.Quantity;
                                }

                                DbContext.MaterialWareHouse.Update(wh);
                            }
                        }
                        else
                        {
                            if (wh is not null)
                            {
                                //Validar si es el mismo almacén
                                if (modelpos.WareHouseId != pos.WareHouseId)
                                {
                                    // Liberar inventario almacén anterior
                                    var whOld = wareHouse.FirstOrDefault(f => f.WareHouseId == modelpos.WareHouseId && f.MaterialId == modelpos.MaterialId);
                                    if (whOld is not null)
                                    {
                                        if (sot.Inventory == "R")
                                        {
                                            whOld.Stock += modelpos.Quantity;
                                            whOld.LockedStock -= modelpos.Quantity;
                                        }

                                        if (sot.Inventory == "I")
                                            whOld.InTransitStock -= modelpos.Quantity;

                                        DbContext.MaterialWareHouse.Update(whOld);
                                    }

                                    // Bloquear inventario almacén actual
                                    // Reduce inventario
                                    if (sot.Inventory == "R")
                                    {
                                        wh.Stock -= pos.Quantity;
                                        wh.LockedStock += pos.Quantity;
                                    }

                                    // Incrementa inventario
                                    if (sot.Inventory == "I")
                                    {
                                        wh.InTransitStock += pos.Quantity;
                                    }

                                    DbContext.MaterialWareHouse.Update(wh);
                                }
                                else
                                {
                                    // Validar cantidad
                                    if (modelpos.Quantity > pos.Quantity)
                                    {
                                        if (sot.Inventory == "R")
                                        {
                                            wh.Stock += modelpos.Quantity - pos.Quantity;
                                            wh.LockedStock -= modelpos.Quantity - pos.Quantity;
                                        }

                                        if (sot.Inventory == "I")
                                        {
                                            wh.InTransitStock -= modelpos.Quantity - pos.Quantity;
                                        }

                                        DbContext.MaterialWareHouse.Update(wh);
                                    }

                                    if (modelpos.Quantity < pos.Quantity)
                                    {
                                        if (sot.Inventory == "R")
                                        {
                                            wh.Stock -= pos.Quantity - modelpos.Quantity;
                                            wh.LockedStock += pos.Quantity - modelpos.Quantity;
                                        }

                                        if (sot.Inventory == "I")
                                        {
                                            wh.InTransitStock += pos.Quantity - modelpos.Quantity;
                                        }

                                        DbContext.MaterialWareHouse.Update(wh);                                        
                                    }
                                }
                            }

                            model.Positions.Remove(modelpos);
                            modelpos = Mapper.Map<SaleOrderPosition>(pos);
                            model.Positions.Add(modelpos);
                        }
                    }

                    await Repository.UpdateAsync(model, dto.ConcurrencyStamp);

                    if (newPos.Count > 0)
                    {
                        await DbContext.SaleOrderPosition.AddRangeAsync(newPos);
                        await DbContext.SaveChangesAsync();
                    }

                    var delPos = model.Positions.Where(w => dto.Positions.All(f => f.Id != w.Id)).ToList();
                    if (delPos.Count > 0)
                    {                        
                        mIds = delPos.Select(p => p.MaterialId).ToList<Guid>();
                        wareHouse = await (from mwh in DbContext.MaterialWareHouse.AsNoTracking()
                                             where mwh.BranchId == model.BranchId && mIds.Contains(mwh.MaterialId)
                                             select mwh).ToListAsync();

                        foreach(var del in delPos)
                        {
                            var wh = wareHouse.FirstOrDefault(f => f.MaterialId == del.MaterialId && f.WareHouseId == del.WareHouseId);
                            if (wh is not null)
                            {
                                // Reduce inventario
                                if (sot.Inventory == "R")
                                {
                                    wh.Stock += del.Quantity;
                                    wh.LockedStock -= del.Quantity;
                                }

                                // Incrementa inventario
                                if (sot.Inventory == "I")
                                {
                                    wh.InTransitStock -= del.Quantity;
                                }

                                DbContext.MaterialWareHouse.Update(wh);
                            }
                        }

                        DbContext.SaleOrderPosition.RemoveRange(delPos);
                        await DbContext.SaveChangesAsync();
                    }



                }
                catch (DbUpdateConcurrencyException ex)
                {
                    var user = await DbContext.Users.AsNoTracking().FirstOrDefaultAsync(f => f.Id == model.LastUpdatedBy);
                    throw new CustomTogoException($"El registro fue modificado por el usuario '{user.UserName}'");
                }
            }

        }

        public async Task<MemoryStream> GetPdfFormAsync(Guid id)
        {
            var model = await Repository.GetFirstorDefaultAsync(f => f.Id == id,
                                                                include: s => s.Include(i => i.Positions.OrderBy(o => o.Position))
                                                                               .ThenInclude(t => t.Conditions));

            if (model is null) throw new CustomTogoException("Pedido de venta no existe");

            var currency = await DbContext.Currency.AsNoTracking().FirstOrDefaultAsync(f => f.Id == model.CurrencyId);
            var soType = await DbContext.SaleOrderType.AsNoTracking().FirstOrDefaultAsync(f => f.Id == model.SaleOrderTypeId);
            var company = await DbContext.Company.Include(i => i.CompanyIDS)
                                                 .Include(i => i.CompanyEconomicActivities)
                                                    .ThenInclude(t => t.EconomicActivity)
                                                 .Include(i => i.Addresses)
                                                        .ThenInclude(t => t.Emails)
                                                     .Include(i => i.Addresses)
                                                        .ThenInclude(t => t.Phones)
                                                     .Include(i => i.Addresses)
                                                        .ThenInclude(i => i.Country)
                                                     .Include(i => i.Addresses)
                                                        .ThenInclude(i => i.Region)
                                                     .Include(i => i.Addresses)
                                                        .ThenInclude(i => i.City)
                                                 .AsNoTracking().FirstOrDefaultAsync(f => f.Id == model.CompanyId);

            var totGravado = model.Positions.Where(w => w.PriceType == "GV").Sum(s => s.NetAmount);
            var totExento = model.Positions.Where(w => w.PriceType == "EX").Sum(s => s.NetAmount);
            var totNoGravado = model.Positions.Where(w => w.PriceType == "NG").Sum(s => s.NetAmount);
            var totNoSujeto = model.Positions.Where(w => w.PriceType == "NS").Sum(s => s.NetAmount);

            var imagePath = $"{env.WebRootPath}\\image\\LogoAvaLink.png";
            var image = File.ReadAllBytes(imagePath);

            var docnit = company.CompanyIDS.FirstOrDefault(f => f.NIFNum == "1");
            var docnrc = company.CompanyIDS.FirstOrDefault(f => f.NIFNum == "2");

            var acteco = company.CompanyEconomicActivities.FirstOrDefault(f => f.Principal == true);
            if (acteco is null)
                acteco = company.CompanyEconomicActivities.FirstOrDefault();

            var taxRate = model.Positions.Where(w => w.Conditions.Any(a => a.Type == "I")).FirstOrDefault();

            var address = company.Addresses.FirstOrDefault();

            var header = new SaleOrderPdfFormDTO
            {
                CurrencyCode = currency.Code,
                SOType = soType.Name,
                SODate = model.Date,
                SONumber = model.Number,
                Gravado = totGravado,
                Exempt = totExento,
                NoGravado = totNoGravado,
                NoSujeto = totNoSujeto,
                TaxAmount = model.TaxAmount,
                Percepcion1 = model.Per1,
                Retencion1 = model.Ret1,
                Retencion10 = model.Ret10,
                CompanyLogo = image,
                CompanyName = company.Name,
                CompanyNIT = docnit is null ? "" : docnit.DocumentNumber,
                CompanyNRC = docnrc is null ? "" : docnrc.DocumentNumber,
                CompanyActivity = acteco is null ? "" : acteco.EconomicActivity.Name,
                TaxRate = taxRate.Conditions.FirstOrDefault(f => f.Type == "I").Value,
            };

            if (address is not null)
            {
                var email = address.Emails.FirstOrDefault(f => f.Principal == true);

                if (email is null)
                    email = address.Emails.FirstOrDefault();

                if (email is not null)
                    header.CompanyEmail = email.Email;

                var phone = address.Phones.FirstOrDefault();
                if (phone is not null)
                    header.CompanyPhone = phone.Phone;
            }

            #region Información Sucursal
            var branch = await DbContext.Branch.Include(i => i.Addresses)
                                                .ThenInclude(t => t.Emails)
                                               .Include(i => i.Addresses)
                                                .ThenInclude(t => t.Phones)
                                               .Include(i => i.Addresses)
                                                .ThenInclude(i => i.Country)
                                               .Include(i => i.Addresses)
                                                .ThenInclude(i => i.Region)
                                               .Include(i => i.Addresses)
                                                .ThenInclude(i => i.City)
                                               .AsNoTracking().FirstOrDefaultAsync(f => f.Id == model.BranchId);
            if (branch is not null)
            {
                header.BranchName = $"{branch.Code} - {branch.Name}";

                address = branch.Addresses.FirstOrDefault();
                if (address is not null)
                {
                    header.BranchAddress = $"{address.Address1}, {address.City.Name}, {address.Region.Name}, {address.Country.Name}";

                    var email = address.Emails.FirstOrDefault(f => f.Principal == true);
                    if (email is null)
                        email = address.Emails.FirstOrDefault();

                    if (email is not null)
                        header.BranchEmail = email.Email;

                    var tel = address.Phones.FirstOrDefault();
                    if (tel is not null)
                        header.BranchPhone = tel.Phone;
                }
            }
            #endregion

            #region Punto de venta
            var pointsale = await DbContext.PointSale.AsNoTracking().FirstOrDefaultAsync(f => f.Id == model.PointSaleId);
            if (pointsale is not null)
                header.PointSale = $"{pointsale.Code} - {pointsale.Name}";
            #endregion

            #region Información Socio
            if (model.Sporadic)
            {

            }
            else
            {
                var partner = await DbContext.Partner.Include(i => i.PartnerIDS)
                                                     .Include(i => i.Addresses)
                                                        .ThenInclude(t => t.Emails)
                                                     .Include(i => i.Addresses)
                                                        .ThenInclude(t => t.Phones)
                                                     .Include(i => i.Addresses)
                                                        .ThenInclude(i => i.Country)
                                                     .Include(i => i.Addresses)
                                                        .ThenInclude(i => i.Region)
                                                     .Include(i => i.Addresses)
                                                        .ThenInclude(i => i.City)
                                                     .Include(i => i.EconomicActivities)
                                                        .ThenInclude(t => t.EconomicActivity)
                                                     .AsNoTracking().FirstOrDefaultAsync(f => f.Id == model.PartnerId);

                var pdocnit = partner.PartnerIDS.FirstOrDefault(f => f.NIFNum == "1");
                var pdocnrc = partner.PartnerIDS.FirstOrDefault(f => f.NIFNum == "2");
                var pact = partner.EconomicActivities.FirstOrDefault(f => f.Principal == true);
                if (pact is null) pact = partner.EconomicActivities.FirstOrDefault();

                header.PartnerName = partner.Name;
                header.PartnerNIT = pdocnit is null ? "" : pdocnit.DocumentNumber;
                header.PartnerNRC = pdocnrc is null ? "" : pdocnrc.DocumentNumber;
                header.PartnerActivity = pact is null ? "" : pact.EconomicActivity.Name;

                if (partner.Addresses.Any())
                {
                    address = partner.Addresses.FirstOrDefault();

                    header.PartnerAddress = $"{address.Address1}, {address.City.Name}, {address.Region.Name}, {address.Country.Name}";

                    var email = address.Emails.FirstOrDefault(f => f.Principal == true);
                    if (email is null)
                        email = address.Emails.FirstOrDefault();

                    if (email is not null)
                        header.PartnerEmail = email.Email;

                    var tel = address.Phones.FirstOrDefault();
                    if (tel is not null)
                        header.PartnerPhone = tel.Phone;
                }
            }
            #endregion

            header.TotalLetras = UtilsExtension.AmountToLetters(header.TotalPagar);

            var positions = Mapper.Map<List<SaleOrderPositionDTO>>(model.Positions);

            var formName = soType.PdfFormName.IsNullOrEmpty() ? "GenericSaleOrder" : soType.PdfFormName;

            var report = UtilsExtension.ReadRDLCForm(formName, env);

            report.DataSources.Add(new ReportDataSource("SaleOrderHeaderDataSet", new List<SaleOrderPdfFormDTO> { header }));
            report.DataSources.Add(new ReportDataSource("SaleOrderPositionDataSet", positions));

            return new MemoryStream(report.Render("PDF"));
        }
    }
}
