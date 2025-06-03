using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Reporting.NETCore;
using XDev_Model;
using XDev_Model.Entities;
using XDev_Model.Interfaces;
using XDev_UnitWork.Custom;
using XDev_UnitWork.DTO;
using XDev_UnitWork.DTO.FeSv;
using XDev_UnitWork.DTO.Invoice;
using XDev_UnitWork.Interfaces;
using QRCoder;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace XDev_UnitWork.Business
{
    public class InvoiceBL : GenericBL<IInvoiceRep>, IInvoiceBL
    {
        private readonly IFeSvBL feSvBL;
        private readonly ICompanyBL companyBL;
        private readonly IBranchBL branchBL;
        private readonly IPartnerBL partnerBL;
        private readonly IWebHostEnvironment env;
        private readonly ILogger<InvoiceBL> logger;
        private readonly UserManager<ApplicationUser> userManager;

        public InvoiceBL(ApplicationDbContext dbContext,
                        IHttpContextAccessor contextAccessor,
                        IMapper mapper,
                        IFeSvBL feSvBL,
                        ICompanyBL companyBL,
                        IBranchBL branchBL,
                        IPartnerBL partnerBL,
                        IWebHostEnvironment env,
                        ILogger<InvoiceBL> logger,
                        UserManager<ApplicationUser> userManager
                        ) : base(dbContext, contextAccessor, mapper)
        {
            this.feSvBL = feSvBL;
            this.companyBL = companyBL;
            this.branchBL = branchBL;
            this.partnerBL = partnerBL;
            this.env = env;
            this.logger = logger;
            this.userManager = userManager;
        }

        public async Task<bool> AnyByCodeGen(string codegen)
        {
            return await DbContext.Invoice.AsNoTracking().AnyAsync(f => f.CodGeneracion == codegen);
        }

        public async Task<bool> AnyByNumber(string invnum)
        {
            return await DbContext.Invoice.AsNoTracking().AnyAsync(f => f.Number == invnum);
        }

        public async Task<Invoice> GetByAssignment(string assignment)
        {
            var inv = await DbContext.Invoice.AsNoTracking().FirstOrDefaultAsync(f => f.Number == assignment);
            if (inv is null)
                inv = await DbContext.Invoice.AsNoTracking().FirstOrDefaultAsync(f => f.CodGeneracion == assignment);

            return inv;
        }

        public async Task<FeCancelDTO> GetInvoiceCancelAsync(string id)
        {
            var model = await DbContext.Invoice.Include(i => i.InvoiceSporadicPartner)
                                               .AsNoTracking().FirstOrDefaultAsync(f => f.Id == id.GetGuid());
            if (model is null)
                throw new CustomTogoException("Factura no existe");

            if (model.Canceled)
                throw new CustomTogoException($"Factura {model.Number} ya está anulada");

            var dto = new FeCancelDTO();
            dto.InvoiceId = model.Id;

            if (model.Sporadic)
            {
                dto.Nombre = model.InvoiceSporadicPartner.Name;
                dto.IDTypeId = model.InvoiceSporadicPartner.IDTypeId;
                dto.TipoDoc = model.InvoiceSporadicPartner.IDCode;
                dto.NumDoc = model.InvoiceSporadicPartner.IDNumber;
                dto.Phone = model.InvoiceSporadicPartner.Phone;
                dto.Email = model.InvoiceSporadicPartner.Email;
            }
            else
            {
                var partner = await partnerBL.GetInfoAsync((Guid)model.PartnerId);
                if (partner is not null)
                {
                    dto.Nombre = partner.Name;
                    dto.IDTypeId = partner.Nif1Id;
                    dto.TipoDoc = partner.Nif1Code;
                    dto.NumDoc = partner.Nif1;
                    dto.Phone = partner.Phone;
                    dto.Email = partner.Email;
                }
            }

            return dto;
        }

        public async Task<FeResponseDTO> CancelInvoice(FeCancelDTO dto)
        {
            var model = await DbContext.Invoice.AsNoTracking().FirstOrDefaultAsync(f => f.Id == dto.InvoiceId);

            if (model is null)
                throw new Exception("Factura no existe");

            if (model.Canceled) throw new Exception("Factura ya está Anulada");

            var soType = await (from so in DbContext.SaleOrder.AsNoTracking()
                                join st in DbContext.SaleOrderType.AsNoTracking() on so.SaleOrderTypeId equals st.Id
                                where so.Id == model.SaleOrderId
                                select new
                                {
                                    st.Inventory
                                }).FirstOrDefaultAsync();

            // Validar Facturación Electrónica
            var configList = await (from ebco in DbContext.EBillingCompany.AsNoTracking()
                                    join eb in DbContext.EBilling.AsNoTracking() on ebco.EBillingId equals eb.Id
                                    where ebco.CompanyId == model.CompanyId
                                    select new
                                    {
                                        eb.Code,
                                        ebco.CompanyId,
                                        ebco.EBillingId,
                                        ebco.Active
                                    }).ToListAsync();

            var config = configList.FirstOrDefault();
            if (config is not null && config.Active)
            {
                if (config.Code == "MHSV")
                {
                    var res = await feSvBL.ProcessCancelInvoiceAsync(model, dto);
                    try
                    {
                        if (res.StatusCode.Substring(0, 2) == "20" && soType is not null)
                            await DbContext.Database.ExecuteSqlAsync($"EXECUTE XSP_MATERIAL_WH_INVOICE {model.Id.ToString()}, {soType.Inventory}");
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex.InnerException is null ? ex.Message : ex.InnerException.Message);
                    }

                    return res;
                }

            }
            else
            {
                // No configurada Facturación Electrónica, guardar documento                               
                model.Canceled = true;
                model.CanceledDate = DateTime.Now;
                model.CanceledUserId = UtilsExtension.GetCurrentUserId(ContextAccessor); ;
                DbContext.Invoice.Update(model);
                await DbContext.SaveChangesAsync();

                try
                {
                    if (soType is not null)
                        await DbContext.Database.ExecuteSqlAsync($"EXECUTE XSP_MATERIAL_WH_INVOICE {model.Id.ToString()}, {soType.Inventory}");
                }
                catch (Exception ex)
                {
                    logger.LogError(ex.InnerException is null ? ex.Message : ex.InnerException.Message);
                }

                return new FeResponseDTO { StatusCode = StatusCodes.Status201Created.ToString(), Message = $"Factura #{model.Number} actualizada correctamente" };
            }

            return new FeResponseDTO { StatusCode = StatusCodes.Status201Created.ToString() };
        }

        public async Task<InvoiceDTO> GetByIdAsync(Guid id)
        {
            var query = await (from inv in DbContext.Invoice.AsNoTracking()
                               join invt in DbContext.InvoiceType.AsNoTracking() on inv.InvoiceTypeId equals invt.Id
                               join co in DbContext.Company.AsNoTracking() on inv.CompanyId equals co.Id
                               join br in DbContext.Branch.AsNoTracking() on inv.BranchId equals br.Id
                               join ps in DbContext.PointSale.AsNoTracking() on inv.PointSaleId equals ps.Id
                               join cur in DbContext.Currency.AsNoTracking() on inv.CurrencyId equals cur.Id
                               join pay in DbContext.PaymentCondition.AsNoTracking() on inv.PaymentConditionId equals pay.Id
                               where inv.Id == id
                               select new InvoiceDTO
                               {
                                   Id = inv.Id,
                                   Number = inv.Number,
                                   Date = inv.Date,
                                   InvoiceType = invt.Name,
                                   Company = co.Name,
                                   Branch = br.Name,
                                   PointSale = ps.Name,
                                   Currency = cur.Name,
                                   CurrencyCode = cur.Code,
                                   Assignment = inv.Assignment,
                                   HasAssignment = inv.AssignmentId == Guid.Empty ? false : true,
                                   Sporadic = inv.Sporadic,
                                   PartnerId = inv.PartnerId,
                                   NetAmount = inv.NetAmount,
                                   TaxAmount = inv.TaxAmount,
                                   Per1 = inv.Per1,
                                   Ret1 = inv.Ret1,
                                   Ret10 = inv.Ret10,
                                   PaymentCondition = pay.Name,
                                   Export = invt.Export,
                                   Contingency = inv.Contingency,
                               }).ToListAsync();

            var model = query.FirstOrDefault();

            if (model is null)
                throw new CustomTogoException("Factura no existe");

            var dto = Mapper.Map<InvoiceDTO>(model);

            if (model.Sporadic)
            {
                var invSporadic = await DbContext.InvoiceSporadicPartner.AsNoTracking().FirstOrDefaultAsync(f => f.InvoiceId == id);
                if (invSporadic is not null)
                {
                    dto.InvoiceSporadicPartner = Mapper.Map<InvoiceSporadicPartnerDTO>(invSporadic);
                }
            }
            else
            {
                if (model.PartnerId is not null)
                {
                    var partner = await partnerBL.GetInfoAsync((Guid)model.PartnerId);
                    if (partner is not null)
                    {
                        dto.PartnerCode = partner.Code;
                        dto.PartnerName = partner.Name;
                        dto.PartnerPhone = partner.Phone;
                        dto.PartnerEmail = partner.Email;
                        dto.PartnerAddress = partner.Address;
                        dto.PartnerEcoAct = partner.EcoActName;
                        dto.PartnerNit = partner.Nif1;
                        dto.PartnerNrc = partner.Nif2;
                    }
                }
            }

            var positions = await DbContext.InvoicePosition.AsNoTracking().Where(w => w.InvoiceId == id).ToListAsync();
            if (positions.Any())
            {
                dto.Positions = Mapper.Map<List<InvoicePositionDTO>>(positions.OrderBy(o => o.Position));

                dto.NoSujeto = dto.Positions.Where(w => w.PriceType == "NS").Sum(s => s.NetAmount);
                dto.NoGravado = dto.Positions.Where(w => w.PriceType == "NG").Sum(s => s.NetAmount);
                dto.Exento = dto.Positions.Where(w => w.PriceType == "EX").Sum(s => s.NetAmount);
                dto.Gravado = dto.Positions.Where(w => w.PriceType == "GV").Sum(s => s.NetAmount);
            }

            var payments = await (from invpay in DbContext.InvoicePayment.AsNoTracking()
                                  join mp in DbContext.MeanOfPayment.AsNoTracking() on invpay.MeanOfPaymentId equals mp.Id into means
                                  from meanspay in means.DefaultIfEmpty()
                                  where invpay.InvoiceId == id
                                  select new InvoicePaymentDTO
                                  {
                                      MeanOfPayment = meanspay.Name,
                                      Amount = invpay.Amount,
                                      Reference = invpay.Reference,
                                      Tipo = invpay.Tipo,
                                      Plazo = invpay.Plazo,
                                      Periodo = invpay.Periodo,
                                  }).ToListAsync();

            if (payments.Any())
            {

                dto.Payments = Mapper.Map<List<InvoicePaymentDTO>>(payments.OrderBy(o => o.Position));
            }

            return dto;
        }

        public async Task<List<InvoiceListDTO>> GetPaginationAsync(PaginationDTO pagination)
        {
            if (pagination.SortField.IsNullOrEmpty())
            {
                pagination.SortField = "CreatedAt";
                pagination.SortOrder = OrderDirection.descending;
            }

            var query = (from inv in DbContext.Invoice.AsNoTracking()
                         join typ in DbContext.InvoiceType.AsNoTracking() on inv.InvoiceTypeId equals typ.Id
                         join co in DbContext.Company.AsNoTracking() on inv.CompanyId equals co.Id
                         join br in DbContext.Branch.AsNoTracking() on inv.BranchId equals br.Id
                         join pa in DbContext.Partner.AsNoTracking() on inv.PartnerId equals pa.Id into partners
                         from partner in partners.DefaultIfEmpty()

                         select new InvoiceListDTO
                         {
                             Id = inv.Id,
                             Number = inv.Number,
                             Date = inv.Date,
                             InvoiceType = typ.Name,
                             CompanyCode = co.Code,
                             BranchCode = br.Code,
                             Sporadic = inv.Sporadic,
                             CurrencyCode = inv.CurrencyCode,
                             PartnerCode = partner.Code,
                             Total = inv.NetAmount + inv.TaxAmount + inv.Per1 - Math.Abs(inv.Ret1) - Math.Abs(inv.Ret10),
                             Canceled = inv.Canceled,
                             CreatedAt = inv.CreatedAt,
                             Contingency = inv.Contingency,
                         });

            query = query.CreateFilterAndOrder(pagination);
            return await query.CreatePaging<InvoiceListDTO, InvoiceListDTO>(pagination, ContextAccessor.HttpContext);
        }

        public async Task<FeResponseDTO> CreateInvoiceAsync(SaleOrder dto)
        {
            //var sotype = await DbContext.SaleOrderType.AsNoTracking().FirstOrDefaultAsync(f => f.Id == dto.SaleOrderTypeId);
            //if (sotype is null)
            //    throw new Exception("Error en tipo pedido de venta");


            var invtype = await DbContext.InvoiceType.AsNoTracking().FirstOrDefaultAsync(f => f.Id == dto.SaleOrderType.InvoiceTypeId);
            if (invtype is null)
                throw new Exception("Error en tipo de factura");


            var model = Mapper.Map<Invoice>(dto);

            if (model.InvoiceSporadicPartner is null)
                model.InvoiceSporadicPartner = Mapper.Map<InvoiceSporadicPartner>(dto.SaleOrderSporadicPartner);

            model.Id = Guid.NewGuid();
            model.InvoiceTypeId = invtype.Id;
            model.InvoiceType = invtype;
            model.PaymentCondition = await DbContext.PaymentCondition.AsNoTracking().FirstOrDefaultAsync(f => f.Id == dto.PaymentConditionId);

            model.Date = DateTime.Now;

            foreach (var pos in model.Positions)
            {
                pos.InvoiceId = model.Id;
            }

            foreach (var pay in model.Payments)
            {
                pay.InvoiceId = model.Id;
            }

            var result = DbContext.Database.SqlQuery<long>($"EXECUTE XSP_GEN_NEXT_NUMBER {invtype.RangeId.ToString()}").ToList();
            if (result.Count == 0)
                throw new CustomTogoException("Error generando rango de número, validar configuración");

            model.Number = result[0].ToString();

            // Validar Facturación Electrónica
            var configList = await (from ebco in DbContext.EBillingCompany.AsNoTracking()
                                    join eb in DbContext.EBilling.AsNoTracking() on ebco.EBillingId equals eb.Id
                                    where ebco.CompanyId == model.CompanyId
                                    select new
                                    {
                                        eb.Code,
                                        ebco.CompanyId,
                                        ebco.EBillingId,
                                        ebco.Active
                                    }).ToListAsync();

            var config = configList.FirstOrDefault();
            if (config is not null && config.Active)
            {
                if (config.Code == "MHSV")
                {
                    var res = await feSvBL.ProcessInvoiceAsync(model);

                    try
                    {
                        if (res.StatusCode.Substring(0, 2) == "20")
                            await DbContext.Database.ExecuteSqlAsync($"EXECUTE XSP_MATERIAL_WH_INVOICE {model.Id.ToString()}, {dto.SaleOrderType.Inventory}");
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex.InnerException is null ? ex.Message : ex.InnerException.Message);
                    }

                    return res;
                }
            }
            else
            {
                // No configurada Facturación Electrónica, guardar documento                
                model.InvoiceType = null;
                model.PaymentCondition = null;
                await DbContext.Invoice.AddAsync(model);
                dto.Invoiced = true;
                DbContext.SaleOrder.Update(dto);
                await DbContext.SaveChangesAsync();

                try
                {
                    await DbContext.Database.ExecuteSqlAsync($"EXECUTE XSP_MATERIAL_WH_INVOICE {model.Id.ToString()}, {dto.SaleOrderType.Inventory}");
                }
                catch (Exception ex)
                {
                    logger.LogError(ex.InnerException is null ? ex.Message : ex.InnerException.Message);
                }


                return new FeResponseDTO { StatusCode = StatusCodes.Status201Created.ToString(), Message = $"Factura #{model.Number} creada correctamente" };
            }

            return new FeResponseDTO { StatusCode = StatusCodes.Status201Created.ToString() };
        }

        public async Task<MemoryStream> CreateFormPDF(Guid id)
        {
            var model = await DbContext.Invoice.Include(i => i.InvoiceType)
                                               .Include(i => i.PaymentCondition)
                                               .Include(i => i.InvoiceSporadicPartner)
                                               .Include(i => i.Positions)
                                               .ThenInclude(t => t.Conditions)
                                               .AsNoTracking().FirstOrDefaultAsync(f => f.Id == id);

            if (model is null)
                throw new CustomTogoException("Factura no existe");

            if (model.InvoiceType.FormName.IsNullOrEmpty())
                throw new CustomTogoException($"No se ha indicado un nombre de formulario para {model.InvoiceType.Name}");

            var totGravado = model.Positions.Where(w => w.PriceType == "GV").Sum(s => s.NetAmount);
            var totExento = model.Positions.Where(w => w.PriceType == "EX").Sum(s => s.NetAmount);
            var totNoGravado = model.Positions.Where(w => w.PriceType == "NG").Sum(s => s.NetAmount);
            var totNoSujeto = model.Positions.Where(w => w.PriceType == "NS").Sum(s => s.NetAmount);

            //var imagePath = $"{env.WebRootPath}\\image\\LogoAvaLink.png";
            //var image = File.ReadAllBytes(imagePath);

            var taxRate = model.Positions.Where(w => w.Conditions.Any(a => a.Type == "I")).FirstOrDefault();
            
            var dto = new InvoiceFormDTO
            {
                CodGen = model.CodGeneracion,
                NumControl = model.NumControl,
                FechaRecepcion = model.FechaRecepcion,
                SelloRecepcion = model.SelloRecepcion,
                CurrencyCode = model.CurrencyCode,
                SOType = model.InvoiceType.Name,
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
                TaxRate = $"{Math.Round(taxRate.Conditions.FirstOrDefault(f => f.Type == "I").Value, 2)}%",
                PartnerPayCondition = model.PaymentCondition.Name,
            };

            var user = await userManager.FindByIdAsync(model.CreatedBy);
            if (user is not null)
            {
                dto.PersonSender = user.Name;
                dto.PersonSenderNumDocNum = user.IDNumber;
            }

            // Datos de la sociedad
            var company = await companyBL.GetCompanyInfoAsync(model.CompanyId);
            dto.CompanyActivity = company.EconomicActivityName;
            dto.CompanyName = company.Name;
            dto.CompanyNIT = company.Nif1;
            dto.CompanyNRC = company.Nif2;
            dto.CompanyPhone = company.Phone;
            dto.CompanyEmail = company.Email;

            var imagePath = company.UrlLogo;

            if (imagePath is not null)
            {
                var imageName = imagePath.Split("/");
                var logoName = imageName[imageName.Length - 1];

                var pathFile = Path.Combine(env.WebRootPath, "image", logoName);

                if (File.Exists(pathFile))
                {
                    dto.CompanyLogo = File.ReadAllBytes(pathFile);                    
                }
            }

            // Datos Sucursal
            var branch = await branchBL.GetBranchInfoAsync(model.BranchId);
            dto.BranchName = branch.Name;
            dto.BranchAddress = branch.Address;
            dto.BranchCityName = branch.CityName;
            dto.BranchRegionName = branch.RegionName;
            dto.BranchEmail = branch.Email;
            dto.BranchPhone = branch.Phone;

            // Punto de venta
            var pointSale = await DbContext.PointSale.AsNoTracking().FirstOrDefaultAsync(f => f.Id == model.PointSaleId);
            if (pointSale is not null)
                dto.PointSale = $"{pointSale.Code} - {pointSale.Name}";

            // Datos Socio
            if (model.Sporadic)
            {
                dto.PartnerName = model.InvoiceSporadicPartner.Name;
                dto.PartnerAddress = model.InvoiceSporadicPartner.Address;
                dto.PartnerCityName = model.InvoiceSporadicPartner.CityName;
                dto.PartnerRegionName = model.InvoiceSporadicPartner.RegionName;
                dto.PartnerActivity = model.InvoiceSporadicPartner.EcoActivityName;
                dto.PartnerEmail = model.InvoiceSporadicPartner.Email;
                dto.PartnerPhone = model.InvoiceSporadicPartner.Phone;
                dto.PartnerNIT = model.InvoiceSporadicPartner.IDNumber;
                dto.PartnerNRC = model.InvoiceSporadicPartner.IDNumber2;
                dto.PersonReceiver = dto.PartnerName;
                dto.PersonReceiverNumDoc = dto.PartnerNIT;
            }
            else
            {
                var partner = await partnerBL.GetInfoAsync(model.PartnerId ?? Guid.Empty);
                dto.PartnerName = partner.Name;
                dto.PartnerAddress = partner.Address;
                dto.PartnerCityName = partner.CityName;
                dto.PartnerRegionName = partner.RegionName;
                dto.PartnerActivity = partner.EcoActName;
                dto.PartnerEmail = partner.Email;
                dto.PartnerPhone = partner.Phone;
                dto.PartnerNIT = partner.Nif1;
                dto.PartnerNRC = partner.Nif2;
                dto.PersonReceiver = partner.ContactPersonName;
                dto.PersonReceiverNumDoc = partner.ContactPersonIDNumber;
            }

            List<InvoiceFormPositionDTO> posDto = new List<InvoiceFormPositionDTO>();

            decimal sumFlete = 0, sumSeguro = 0;
            foreach (var pos in model.Positions)
            {
                var posFlt = pos.Conditions.FirstOrDefault(x => x.Code == "FLT");
                if (posFlt is not null)
                    sumFlete += posFlt.ValueCondition;

                var posSeg = pos.Conditions.FirstOrDefault(x => x.Code == "SEG");
                if(posSeg is not null)
                    sumSeguro += posSeg.ValueCondition;

                posDto.Add(new InvoiceFormPositionDTO
                {
                    MaterialCode = pos.MaterialCode,
                    MaterialName = pos.MaterialName,
                    MaterialTypeCode = pos.MaterialTypeCode,
                    UnitMeasureCode = pos.UnitMeasureCode,
                    NetAmount = pos.NetAmount,
                    TaxAmount = pos.TaxAmount,
                    DiscountAmount = pos.DiscountAmount,
                    Quantity = pos.Quantity,
                    NoGravado = pos.PriceType == "NG" ? pos.TaxAmount : 0,
                    NoSujeto = pos.PriceType == "NS" ? pos.TaxAmount : 0,
                    Exento = pos.PriceType == "EX" ? pos.TaxAmount : 0,
                    Gravado = pos.PriceType == "GV" ? pos.NetAmount : 0,
                    UnitPrice = (pos.NetAmount + pos.DiscountAmount) / pos.Quantity,
                });
            }

            dto.Flete = sumFlete;
            dto.Seguro = sumSeguro;

            dto.TotalLetras = dto.TotalPagar.AmountToLetters();

            if (model.CurrencyCode == "USD")
                dto.TotalLetras = $"{dto.TotalLetras} DÓLARES";

            var ebillingCo = await DbContext.EBillingCompany.FirstOrDefaultAsync(f => f.CompanyId == model.CompanyId);
            if (ebillingCo is not null)
            {
                var url = $"https://admin.factura.gob.sv/consultaPublica?ambiente={(ebillingCo.IsProd ? "01" : "00")}&codGen={model.CodGeneracion}&fechaEmi={model.Date.ToString("yyy-MM-dd")}";

                using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
                using (QRCodeData qrCodeData = qrGenerator.CreateQrCode(url, QRCodeGenerator.ECCLevel.Q))
                using (PngByteQRCode qrCode = new PngByteQRCode(qrCodeData))
                {
                    dto.QrCode = qrCode.GetGraphic(20);
                }
            }

            var report = UtilsExtension.ReadRDLCForm(model.InvoiceType.FormName, env);

            report.DataSources.Add(new ReportDataSource("InvoiceDataSet", new List<InvoiceFormDTO> { dto }));
            report.DataSources.Add(new ReportDataSource("InvoicePositionDataSet", posDto));

            /// Documentos relacionados
            if (model.AssignmentId != Guid.Empty)
            {
                var docRel = await DbContext.Invoice.Include(i => i.InvoiceType)
                                                    .AsNoTracking().FirstOrDefaultAsync(f => f.Id == model.AssignmentId);
                if (docRel is not null)
                {
                    report.DataSources.Add(new ReportDataSource("InvoiceDocumentsDataSet", new List<InvoiceFormDocumentsDTO> {
                        new InvoiceFormDocumentsDTO {
                            DocumentType = docRel.InvoiceType.Name,
                            DocumentNumber = docRel.CodGeneracion,
                            DocumentDate = docRel.Date,
                        }
                    }));
                }
            }

            return new MemoryStream(report.Render("PDF"));
        }
    }
}
