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
using XDev_UnitWork.DTO.FeSv;

namespace XDev_UnitWork.Business
{
    public class SaleOrderBL : GenericBL<ISaleOrderRep>, ISaleOrderBL
    {
        private readonly IInvoiceBL invoiceBL;
        private readonly ILogger<SaleOrderBL> logger;
        private readonly IAddressBL addressBL;
        private readonly ICompanyBL companyBL;
        private readonly IBranchBL branchBL;
        private readonly IPartnerBL partnerBL;
        private readonly IWebHostEnvironment env;

        public SaleOrderBL(ApplicationDbContext dbContext,
                           IHttpContextAccessor contextAccessor,
                           IMapper mapper,
                           IInvoiceBL invoiceBL,
                           ILogger<SaleOrderBL> logger,
                           IAddressBL addressBL,
                           ICompanyBL companyBL,
                           IBranchBL branchBL,
                           IPartnerBL partnerBL,
                           IWebHostEnvironment env) : base(dbContext, contextAccessor, mapper)
        {
            this.invoiceBL = invoiceBL;
            this.logger = logger;
            this.addressBL = addressBL;
            this.companyBL = companyBL;
            this.branchBL = branchBL;
            this.partnerBL = partnerBL;
            this.env = env;
        }

        public async Task<SaleOrderSporadicPartner> SporadicValidateInfo(SaleOrderSporadicPartner model)
        {
            // Documento de Identificación
            if (model.IDNumber.IsNullOrEmpty())
            {
                model.IDTypeId = Guid.Empty;
                model.IDCode = string.Empty;
            }
            else
            {
                var idt = await DbContext.IDType.AsNoTracking().FirstOrDefaultAsync(f => f.Id == model.IDTypeId);
                if (idt is not null)
                {
                    model.IDCode = idt.AltCode;
                }
            }

            // Documento de Identificación 2
            if (model.IDNumber2.IsNullOrEmpty())
            {
                model.IDTypeId2 = Guid.Empty;
                model.IDCode2 = string.Empty;
            }
            else
            {
                var idt = await DbContext.IDType.AsNoTracking().FirstOrDefaultAsync(f => f.Id == model.IDTypeId2);
                if (idt is not null)
                {
                    model.IDCode2 = idt.AltCode;
                }
            }

            // Dirección
            if (model.Address.IsNullOrEmpty())
            {
                model.CountryId = Guid.Empty;
                model.CountryCode = string.Empty;
                model.CountryName = string.Empty;
                model.RegionCode = string.Empty;
                model.RegionName = string.Empty;
                model.CityCode = string.Empty;
                model.CityName = string.Empty;
            }
            else
            {
                var country = await DbContext.Country.Include(i => i.Regions)
                                                     .ThenInclude(t => t.Cities)
                                                     .AsNoTracking().FirstOrDefaultAsync(f => f.Id == model.CountryId);

                model.CountryCode = country.Code;
                model.CountryName = country.Name;

                var region = country.Regions.FirstOrDefault(f => f.Id == model.RegionId);
                model.RegionCode = region.Code;
                model.RegionName = region.Name;

                var city = region.Cities.FirstOrDefault(f => f.Id == model.CityId);
                model.CityCode = city.Code;
                model.CityName = city.Name;
            }

            // Actividad Económica
            if (model.EcoActivityId == Guid.Empty)
            {
                model.EcoActivityCode = string.Empty;
                model.EcoActivityName = string.Empty;
            }
            else
            {
                var act = await DbContext.EconomicActivities.AsNoTracking().FirstOrDefaultAsync(f => f.Id == model.EcoActivityId);
                model.EcoActivityCode = act.Code;
                model.EcoActivityName = act.Name;
            }

            return model;
        }


        public Task<bool> AnyAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AnyAsync(string code)
        {
            throw new NotImplementedException();
        }

        public Task CreateAsync(SaleOrderDTO dto)
        {
            throw new NotImplementedException();
        }

        public async Task<string> CreateSOAsync(SaleOrderDTO dto)
        {
            var sotype = await DbContext.SaleOrderType.FirstOrDefaultAsync(f => f.Id == dto.SaleOrderTypeId);

            var model = Mapper.Map<SaleOrder>(dto);
            model.SaleOrderType = null;
            model.PaymentCondition = null;

            if (model.Sporadic)
            {
                model.SaleOrderSporadicPartner = await SporadicValidateInfo(model.SaleOrderSporadicPartner);
            }
            else
            {
                model.SaleOrderSporadicPartner = null;
            }

            if (sotype.AssignmentRequired)
            {
                var inv = await invoiceBL.GetByAssignment(model.Assignment);
                if (inv is not null)
                    model.AssignmentId = inv.Id;
            }

            var result = DbContext.Database.SqlQuery<long>($"EXECUTE XSP_GEN_NEXT_NUMBER {sotype.RangeId.ToString()}").ToList();
            if (result.Count == 0)
                throw new CustomTogoException("Error generando rango de número, validar configuración");

            model.Number = result[0].ToString();

            await Repository.CreateAsync(model);

            try
            {

                foreach (var pos in dto.Positions)
                {
                    if (pos.MaterialTypeCode == "B")
                        await DbContext.Database.ExecuteSqlAsync($"EXECUTE XSP_MATERIAL_WH_SALE_ORDER {pos.MaterialId.ToString()}, {dto.BranchId.ToString()}, {pos.WareHouseId.ToString()}, {sotype.Inventory}, {pos.Quantity}");
                }
            }
            catch (Exception ex)
            {
                logger.LogError($"Creación SO: {(ex.InnerException is null ? ex.Message : ex.InnerException.Message)}");
            }

            return $"Pedido de Venta {model.Number} creado correctamente";
        }

        public async Task DeleteAsync(Guid id)
        {
            var model = await DbContext.SaleOrder.Include(i => i.Positions).Include(i => i.SaleOrderType).FirstOrDefaultAsync(f => f.Id == id);
            if (model is not null)
            {
                if (model.Invoiced)
                    throw new CustomTogoException("Pedido de venta ya facturado, no se puede eliminar");

                var mIds = model.Positions.Select(p => p.MaterialId).ToList<Guid>();
                var entity = model;
                //var sot = DbContext.SaleOrderType.AsNoTracking().FirstOrDefault(f => f.Id == model.SaleOrderTypeId);
                var sot = model.SaleOrderType;

                DbContext.SaleOrder.Remove(model);
                await DbContext.SaveChangesAsync();

                foreach (var pos in entity.Positions)
                {
                    if (pos.MaterialTypeCode == "B")
                        await DbContext.Database.ExecuteSqlAsync($"EXECUTE XSP_MATERIAL_WH_SALE_ORDER {pos.MaterialId.ToString()}, {model.BranchId.ToString()}, {pos.WareHouseId.ToString()}, {sot.Inventory}, {pos.Quantity * -1}");

                }


            }
        }

        public async Task<SaleOrderDTO> GetByIdAsync(params object[] ids)
        {
            var model = await Repository.GetFirstorDefaultAsync(f => f.Id == Guid.Parse(ids[0].ToString()),
                                                                include: s => s.Include(i => i.SaleOrderSporadicPartner)
                                                                               .Include(i => i.Payments));
            if (model is null)
                return new SaleOrderDTO();

            model.Positions = DbContext.SaleOrderPosition.Include(i => i.Conditions)
                                                         .Where(w => w.SaleOrderId == model.Id)
                                                         .AsNoTracking().OrderBy(o => o.Position).ToHashSet();

            foreach (var pos in model.Positions)
            {
                pos.Conditions = pos.Conditions.OrderBy(o => o.Position).ToHashSet();
            }

            if (model.PartnerId is null)
                model.Sporadic = true;

            var dto = Mapper.Map<SaleOrderDTO>(model);

            if (!dto.Sporadic) dto.SaleOrderSporadicPartner = new SaleOrderSporadicPartnerDTO();

            if (model.PartnerId != Guid.Empty)
            {
                var partner = await DbContext.Partner.FirstOrDefaultAsync(f => f.Id == model.PartnerId);
                if (partner is not null)
                {
                    dto.PartnerCode = partner.Code;
                    dto.PartnerName = partner.Name;
                }
            }

            var pay = await DbContext.PaymentCondition.AsNoTracking().FirstOrDefaultAsync(f => f.Id == dto.PaymentConditionId);
            if (pay is not null)
                dto.PaymentConditionType = pay.Tipo;

            return dto;
        }

        public Task<List<SaleOrderDTO>> GetListAsync(PaginationDTO pagination)
        {
            throw new NotImplementedException();
        }

        public async Task<List<SaleOrderListDTO>> GetSOListAsync(PaginationDTO pagination)
        {
            if (pagination.SortField.IsNullOrEmpty())
            {
                pagination.SortField = "CreatedAt";
                pagination.SortOrder = OrderDirection.descending;
            }

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
                             Total = so.NetAmount + so.TaxAmount + so.Per1 - Math.Abs(so.Ret1) - Math.Abs(so.Ret10),
                             CurrencyCode = cur.Code,
                             CompanyCode = co.Code,
                             RefDocument = so.RefDocument,
                             Delivered = so.Delivered,
                             Invoiced = so.Invoiced,
                             CreatedAt = so.CreatedAt,
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
                                                                include: s => s.Include(i => i.SaleOrderSporadicPartner)
                                                                               .Include(i => i.Payments)
                                                                               .Include(i => i.SaleOrderType));

            if (model is not null)
            {
                if (model.Invoiced)
                    throw new CustomTogoException("Pedido de venta ya fue facturado");

                try
                {
                    model.Positions = DbContext.SaleOrderPosition.Include(i => i.Conditions).AsNoTracking().Where(w => w.SaleOrderId == model.Id).ToHashSet();
                    model.RefDate = dto.RefDate;
                    model.RefDocument = dto.RefDocument;
                    model.Sporadic = dto.Sporadic;
                    model.SaleOrderSporadicPartner = Mapper.Map<SaleOrderSporadicPartner>(dto.SaleOrderSporadicPartner);
                    if (model.Sporadic)
                        model.SaleOrderSporadicPartner = await SporadicValidateInfo(model.SaleOrderSporadicPartner);

                    if (!model.Sporadic)
                        model.SaleOrderSporadicPartner = null;

                    //model.Date = dto.Date;
                    //model.SaleOrderTypeId = dto.SaleOrderTypeId;
                    //model.CompanyId = dto.CompanyId;
                    //model.BranchId = dto.BranchId;
                    model.PaymentConditionId = dto.PaymentConditionId;
                    model.PointSaleId = dto.PointSaleId;
                    model.PointSaleCode = dto.PointSaleCode;
                    model.CurrencyId = dto.CurrencyId;
                    model.CurrencyCode = dto.CurrencyCode;
                    model.PartnerId = dto.Sporadic ? null : dto.PartnerId;
                    model.TaxAmount = dto.TaxAmount;
                    model.NetAmount = dto.NetAmount;
                    model.Per1 = dto.Per1;
                    model.Ret1 = dto.Ret1;
                    model.Ret10 = dto.Ret10;
                    model.RecintoFiscalId = dto.RecintoFiscalId;
                    model.RecintoFiscalCode = dto.RecintoFiscalCode;
                    model.RegimenExportId = dto.RegimenExportId;
                    model.RegimenExportCode = dto.RegimenExportCode;
                    model.IncoTermsId = dto.IncoTermsId;
                    model.IncoTerms = dto.IncoTerms;

                    if (dto.Assignment != model.Assignment)
                    {
                        if (model.SaleOrderType.AssignmentRequired)
                        {
                            var inv = await invoiceBL.GetByAssignment(dto.Assignment);
                            if (inv is not null)
                                model.AssignmentId = inv.Id;
                        }
                    }
                    model.Assignment = dto.Assignment;

                    ////var sot = await DbContext.SaleOrderType.AsNoTracking().FirstOrDefaultAsync(f => f.Id == model.SaleOrderTypeId);
                    var sot = model.SaleOrderType;
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

                            if (pos.MaterialTypeCode == "B")
                                await DbContext.Database.ExecuteSqlAsync($"EXECUTE XSP_MATERIAL_WH_SALE_ORDER {pos.MaterialId.ToString()}, {dto.BranchId.ToString()}, {pos.WareHouseId.ToString()}, {sot.Inventory}, {pos.Quantity}");
                        }
                        else
                        {
                            if (wh is not null && pos.MaterialTypeCode == "B")
                            {
                                //Validar si es el mismo almacén
                                if (modelpos.WareHouseId != pos.WareHouseId)
                                {
                                    // Liberar inventario almacén anterior
                                    var whOld = wareHouse.FirstOrDefault(f => f.WareHouseId == modelpos.WareHouseId && f.MaterialId == modelpos.MaterialId);
                                    if (whOld is not null)
                                        await DbContext.Database.ExecuteSqlAsync($"EXECUTE XSP_MATERIAL_WH_SALE_ORDER {pos.MaterialId.ToString()}, {dto.BranchId.ToString()}, {whOld.WareHouseId.ToString()}, {sot.Inventory}, {pos.Quantity * -1}");

                                    // Bloquear inventario almacén actual
                                    await DbContext.Database.ExecuteSqlAsync($"EXECUTE XSP_MATERIAL_WH_SALE_ORDER {pos.MaterialId.ToString()}, {dto.BranchId.ToString()}, {pos.WareHouseId.ToString()}, {sot.Inventory}, {pos.Quantity}");
                                }
                                else
                                {
                                    // Validar cantidad
                                    if (modelpos.Quantity > pos.Quantity)
                                        await DbContext.Database.ExecuteSqlAsync($"EXECUTE XSP_MATERIAL_WH_SALE_ORDER {pos.MaterialId.ToString()}, {dto.BranchId.ToString()}, {pos.WareHouseId.ToString()}, {sot.Inventory}, {modelpos.Quantity - pos.Quantity}");

                                    if (modelpos.Quantity < pos.Quantity)
                                        await DbContext.Database.ExecuteSqlAsync($"EXECUTE XSP_MATERIAL_WH_SALE_ORDER {pos.MaterialId.ToString()}, {dto.BranchId.ToString()}, {pos.WareHouseId.ToString()}, {sot.Inventory}, {pos.Quantity - modelpos.Quantity}");
                                }
                            }

                            //model.Positions.Remove(modelpos);
                            modelpos.Quantity = pos.Quantity;
                            modelpos.NetPrice = pos.NetPrice;
                            modelpos.GrossPrice = pos.GrossPrice;
                            modelpos.Position = pos.Position;
                            modelpos.PriceType = pos.PriceType;
                            modelpos.NetAmount = pos.NetAmount;
                            modelpos.TaxAmount = pos.TaxAmount;
                            modelpos.DiscountAmount = pos.DiscountAmount;                            
                            modelpos.MaterialCode = pos.MaterialCode;
                            modelpos.MaterialTypeCode = pos.MaterialTypeCode;
                            modelpos.MaterialId = pos.MaterialId;
                            modelpos.MaterialName = pos.MaterialName;
                            modelpos.UnitMeasureAltCode = pos.UnitMeasureAltCode;
                            modelpos.UnitMeasureCode = pos.UnitMeasureCode;
                            modelpos.UnitMeasureId = pos.UnitMeasureId;
                            modelpos.WareHouseId = pos.WareHouseId;
                            modelpos.Conditions = Mapper.Map<HashSet<SaleOrderPositionCondition>>(pos.Conditions);

                            await DbContext.Database.ExecuteSqlAsync($"delete from SaleOrderPositionCondition where SaleOrderPositionId = {modelpos.Id}");
                            //model.Positions.Add(modelpos);
                        }
                    }

                    #region Medios de pago
                    foreach (var payment in model.Payments)
                    {
                        model.Payments.Remove(payment);
                    }

                    foreach (var payment in dto.Payments)
                    {
                        model.Payments.Add(Mapper.Map<SaleOrderPayment>(payment));
                    }
                    #endregion



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

                        foreach (var del in delPos)
                        {
                            var wh = wareHouse.FirstOrDefault(f => f.MaterialId == del.MaterialId && f.WareHouseId == del.WareHouseId);
                            if (wh is not null && del.MaterialTypeCode == "B")
                                await DbContext.Database.ExecuteSqlAsync($"EXECUTE XSP_MATERIAL_WH_SALE_ORDER {wh.MaterialId.ToString()}, {dto.BranchId.ToString()}, {wh.WareHouseId.ToString()}, {sot.Inventory}, {del.Quantity * -1}");
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

            var soType = await DbContext.SaleOrderType.AsNoTracking().FirstOrDefaultAsync(f => f.Id == model.SaleOrderTypeId);
            var company = await companyBL.GetCompanyInfoAsync(model.CompanyId);

            var totGravado = model.Positions.Where(w => w.PriceType == "GV").Sum(s => s.NetAmount);
            var totExento = model.Positions.Where(w => w.PriceType == "EX").Sum(s => s.NetAmount);
            var totNoGravado = model.Positions.Where(w => w.PriceType == "NG").Sum(s => s.NetAmount);
            var totNoSujeto = model.Positions.Where(w => w.PriceType == "NS").Sum(s => s.NetAmount);

            var imagePath = $"{env.WebRootPath}\\image\\LogoAvaLink.png";
            var image = File.ReadAllBytes(imagePath);

            var taxRate = model.Positions.Where(w => w.Conditions.Any(a => a.Type == "I")).FirstOrDefault();

            var header = new SaleOrderPdfFormDTO
            {
                CurrencyCode = model.CurrencyCode,
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
                CompanyNIT = company.Nif1,
                CompanyNRC = company.Nif2,
                CompanyActivity = company.EconomicActivityName,
                CompanyEmail = company.Email,
                CompanyPhone = company.Phone,
                TaxRate = taxRate.Conditions.FirstOrDefault(f => f.Type == "I").Value,

            };

            #region Información Sucursal
            var branch = await branchBL.GetBranchInfoAsync(model.BranchId);
            if (branch is not null)
            {
                header.BranchName = $"{branch.Code} - {branch.Name}";
                header.BranchAddress = $"{branch.Address}, {branch.CityName}, {branch.RegionName}, {branch.CountryName}";
                header.BranchEmail = branch.Email;
                header.BranchPhone = branch.Phone;

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
                var partner = await partnerBL.GetInfoAsync((Guid)model.PartnerId);

                header.PartnerName = partner.Name;
                header.PartnerNIT = partner.Nif1;
                header.PartnerNRC = partner.Nif2;
                header.PartnerActivity = partner.EcoActName;
                header.PartnerAddress = $"{partner.Address}, {partner.CityName}, {partner.RegionName}, {partner.CountryName}";
                header.PartnerEmail = partner.Email;
                header.PartnerPhone = partner.Phone;
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

        public async Task<FeResponseDTO> CreateInvoiceAsync(string soid)
        {
            //var model = await Repository.GetFirstorDefaultAsync(f => f.Id == soid.GetGuid(),
            //                                                    include: s => s.Include(i => i.SaleOrderSporadicPartner)
            //                                                                   .Include(i => i.Positions.OrderBy(o => o.Position))
            //                                                                   .ThenInclude(t => t.Conditions)
            //                                                                   .Include(i => i.SaleOrderType)
            //                                                                   .Include(i => i.Payments));

            var saleorder = DbContext.SaleOrder.AsNoTracking();
            var sporadic = DbContext.SaleOrderSporadicPartner.AsNoTracking();
            var position = DbContext.SaleOrderPosition.AsNoTracking().Include(x => x.Conditions)
                                                      .Where(w => w.SaleOrderId == soid.GetGuid())
                                                      .OrderBy(o => o.Position);
            var ordertype = DbContext.SaleOrderType.AsNoTracking();
            var payment = DbContext.SaleOrderPayment.AsNoTracking();

            var query = await (from so in saleorder
                               join sp in sporadic on so.Id equals sp.SaleOrderId into spo
                               from spor in spo.DefaultIfEmpty()
                               join ot in ordertype on so.SaleOrderTypeId equals ot.Id
                               join py in payment on so.Id equals py.SaleOrderId into payments                               
                               where so.Id == soid.GetGuid()
                               select new
                               {
                                   so,
                                   spor,
                                   ot,
                                   payments                                   
                               }).FirstOrDefaultAsync();

            var cf = new MapperConfiguration(cfg => cfg.CreateMap<SaleOrder, SaleOrder>());
            cf.AssertConfigurationIsValid();

            var model = new Mapper(cf).Map<SaleOrder>(query.so);

            model.Positions = position.ToHashSet();
            model.SaleOrderType = query.ot;
            model.Payments = query.payments.ToHashSet();
            model.SaleOrderSporadicPartner = query.spor;

            if (model is null)
                throw new Exception("Error en pedido de venta");



            if (model.Invoiced)
                throw new Exception($"El pedido de venta {model.Number} ya fue facturado.");

            var result = await invoiceBL.CreateInvoiceAsync(model);

            // Colocar estatus facturado al pedido

            return result;
        }


    }
}
