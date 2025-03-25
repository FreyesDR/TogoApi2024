using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Net.Sockets;
using XDev_Model;
using XDev_Model.Entities;
using XDev_UnitWork.Custom;
using XDev_UnitWork.DTO;
using XDev_UnitWork.DTO.Company;
using XDev_UnitWork.DTO.Dte;
using XDev_UnitWork.DTO.FeSv;
using XDev_UnitWork.Interfaces;
using XDev_UnitWork.Services;
using AutoMapper;
using System.Net.Http.Headers;
using System.Net;

namespace XDev_UnitWork.Business
{
	public class FeSvBL : IFeSvBL
	{
		private readonly ApplicationDbContext dbContext;
		private readonly ICompanyBL companyBL;
		private readonly IBranchBL branchBL;
		private readonly IPartnerBL partnerBL;
		private readonly ISignerService signerService;
		private readonly IWebHostEnvironment hostEnvironment;
		private readonly IHttpContextAccessor contextAccessor;
		private readonly IHttpClientFactory httpClientFactory;
		internal EBilling ebilling;
		private EBillingCompany ebillingCom;
		private EBillingCompanyInvoice typedoc;
		private EBillingDocument ebillingDoc;
		private List<EBillingTax> ebillingTax;
		private CompanyInfoDTO company;
		private CurrentUserDTO currentUser;
		private TimeSpan timeOutHttpClient = TimeSpan.FromMinutes(3);
		private EBillingLog log;

		public FeSvBL(ApplicationDbContext dbContext,
					  ICompanyBL companyBL,
					  IBranchBL branchBL,
					  IPartnerBL partnerBL,
					  ISignerService signerService,
					  IWebHostEnvironment hostEnvironment,
					  IHttpContextAccessor contextAccessor,
					  IHttpClientFactory httpClientFactory)
		{
			this.dbContext = dbContext;
			this.companyBL = companyBL;
			this.branchBL = branchBL;
			this.partnerBL = partnerBL;
			this.signerService = signerService;
			this.hostEnvironment = hostEnvironment;
			this.contextAccessor = contextAccessor;
			this.httpClientFactory = httpClientFactory;
		}

		public async Task<FeResponseDTO> ProcessCancelInvoiceAsync(Invoice invoice, FeCancelDTO dto)
		{
			currentUser = UtilsExtension.GetCurrentUserClaim(contextAccessor);

			ebilling = await dbContext.EBilling.AsNoTracking().FirstOrDefaultAsync(f => f.Code == "MHSV");
			ebillingCom = await dbContext.EBillingCompany.AsNoTracking().FirstOrDefaultAsync(f => f.EBillingId == ebilling.Id &&
																								 f.CompanyId == invoice.CompanyId);

			if (ebillingCom is null)
				return new FeResponseDTO { Message = "Sociedad no condifurada para Facturación Electrónica" };

			company = await companyBL.GetCompanyInfoAsync(invoice.CompanyId);

			return await SendCancelAsync(invoice, dto);
		}

		public async Task<FeResponseDTO> ProcessInvoiceAsync(Invoice invoice)
		{
			currentUser = UtilsExtension.GetCurrentUserClaim(contextAccessor);

			ebilling = await dbContext.EBilling.AsNoTracking().FirstOrDefaultAsync(f => f.Code == "MHSV");
			ebillingCom = await dbContext.EBillingCompany.AsNoTracking().FirstOrDefaultAsync(f => f.EBillingId == ebilling.Id &&
																								 f.CompanyId == invoice.CompanyId);

			if (ebillingCom is null)
				return new FeResponseDTO { Message = "Sociedad no configurada para Facturación Electrónica" };

			typedoc = dbContext.EBillingCompanyInvoice.AsNoTracking().FirstOrDefault(f => f.InvoiceTypeId == invoice.InvoiceTypeId &&
																							  f.CompanyId == invoice.CompanyId &&
																							  f.EBillingId == ebilling.Id);

			if (typedoc is null)
				return new FeResponseDTO { Message = $"{invoice.InvoiceType.Name} no configurado para Facturación Electrónica" };

			ebillingDoc = dbContext.EBillingDocument.AsNoTracking().FirstOrDefault(f => f.Id == typedoc.EBillingDocumentId);

			ebillingTax = await dbContext.EBillingTax.AsNoTracking().Where(w => w.EBillingId == ebilling.Id).ToListAsync();

			company = await companyBL.GetCompanyInfoAsync(invoice.CompanyId);

			if (ebillingDoc.Code == "01")
				return await SendFacturaAsync(invoice);

			if (ebillingDoc.Code == "03")
				return await SendCreditoFiscalAsync(invoice);

			if (ebillingDoc.Code == "05")
				return await SendNotaCredito(invoice);

			if (ebillingDoc.Code == "06")
				return await SendNotaDebito(invoice);

			if (ebillingDoc.Code == "11")
				return await SendFacturaExportador(invoice);

			return new FeResponseDTO() { Message = $"Proceso para {invoice.InvoiceType.Name} no implementado" };
		}

		private async Task<FeResponseDTO> SendCancelAsync(Invoice invoice, FeCancelDTO feCancel)
		{
			var jsondto = new JsonCancelV2();

			#region Identificación            
			jsondto.identificacion.ambiente = ebillingCom.IsProd ? "01" : "00";
			jsondto.identificacion.codigoGeneracion = Guid.NewGuid().ToString().ToUpper();
			jsondto.identificacion.fecAnula = DateTime.Now.ToString("yyyy-MM-dd");
			jsondto.identificacion.horAnula = DateTime.Now.ToString("HH:mm:ss");
			#endregion

			#region Emisor 
			var branch = await branchBL.GetBranchInfoAsync(invoice.BranchId);
			jsondto.emisor.nit = company.Nif1.Replace("-", "");
			jsondto.emisor.nombre = company.Name;
			jsondto.emisor.tipoEstablecimiento = branch.TypeCode;
			jsondto.emisor.nomEstablecimiento = branch.Name;
			jsondto.emisor.telefono = branch.Phone;
			jsondto.emisor.correo = branch.Email;
			jsondto.emisor.codEstable = branch.Code;
			jsondto.emisor.codPuntoVenta = invoice.PointSaleCode;
			#endregion

			#region Documento
			jsondto.documento.tipoDte = invoice.EBillingDoc;
			jsondto.documento.codigoGeneracion = invoice.CodGeneracion;
			jsondto.documento.selloRecibido = invoice.SelloRecepcion;
			jsondto.documento.numeroControl = invoice.NumControl;
			jsondto.documento.fecEmi = invoice.Date.ToString("yyyy-MM-dd");
			jsondto.documento.montoIva = invoice.NetAmount;

			if (feCancel.CancelType == "2")
			{
				jsondto.documento.codigoGeneracionR = null;
			}
			else
			{
				if (feCancel.CodGenR.IsNullOrEmpty())
					throw new Exception("No se ha indicado el documento que reemplaza esta factura");

				jsondto.documento.codigoGeneracionR = feCancel.CodGenR.ToUpper();
			}

			if (feCancel.NumDoc.IsNotNullOrEmpty())
			{
				jsondto.documento.tipoDocumento = feCancel.TipoDoc;
				jsondto.documento.numDocumento = feCancel.NumDoc;
			}
			else
			{
				jsondto.documento.tipoDocumento = null;
				jsondto.documento.numDocumento = null;
			}

			jsondto.documento.nombre = feCancel.Nombre;
			jsondto.documento.telefono = feCancel.Phone.IsNullOrEmpty() ? null : feCancel.Phone;
			jsondto.documento.correo = feCancel.Email.IsNullOrEmpty() ? null : feCancel.Email;
			#endregion

			#region Motivo
			jsondto.motivo.tipoAnulacion = Convert.ToInt16(feCancel.CancelType);
			jsondto.motivo.motivoAnulacion = feCancel.Motivo;
			jsondto.motivo.nombreResponsable = currentUser.Name;
			jsondto.motivo.tipDocResponsable = currentUser.IDCode;
			jsondto.motivo.numDocResponsable = currentUser.IDNumber;
			jsondto.motivo.nombreSolicita = feCancel.Solicita;
			jsondto.motivo.tipDocSolicita = feCancel.SolicitaTipoDoc;
			jsondto.motivo.numDocSolicita = feCancel.SolicitaNumDoc;
			#endregion

			log = new EBillingLog
			{
				CompanyId = invoice.CompanyId,
				BranchId = invoice.BranchId,
				PointSaleId = invoice.PointSaleId,
				CancelInvoiceId = invoice.Id,
				CodGen = jsondto.identificacion.codigoGeneracion.GetGuid(),
			};

			var result = await PostCancelAsync(jsondto);

			if (result.StatusCode.Substring(0, 2) == "20")
			{
				invoice.Canceled = true;
				invoice.CanceledDate = DateTime.Now;
				invoice.CanceledUserId = currentUser.Id;
				dbContext.Update(invoice);
				await dbContext.SaveChangesAsync();
			}

			return result;
		}

		private async Task<FeResponseDTO> SendFacturaExportador(Invoice invoice)
		{
			var jsondto = new JsonFexV1();

			#region Identificación
			jsondto.identificacion.version = 1;
			jsondto.identificacion.ambiente = ebillingCom.IsProd ? "01" : "00";
			jsondto.identificacion.tipoDte = ebillingDoc.Code;
			jsondto.identificacion.tipoModelo = 1;
			jsondto.identificacion.tipoOperacion = 1;
			jsondto.identificacion.tipoContingencia = null;
			jsondto.identificacion.motivoContigencia = null;
			jsondto.identificacion.fecEmi = DateTime.Now.ToString("yyyy-MM-dd");
			jsondto.identificacion.horEmi = DateTime.Now.ToString("HH:mm:ss");
			jsondto.identificacion.tipoMoneda = invoice.CurrencyCode;
			#endregion

			#region Emisor               
			var branch = await branchBL.GetBranchInfoAsync(invoice.BranchId);
			jsondto.emisor.nit = company.Nif1.Replace("-", "");
			jsondto.emisor.nrc = company.Nif2.Replace("-", "");
			jsondto.emisor.nombre = company.Name;
			jsondto.emisor.codActividad = company.EconomicActivityCode;
			jsondto.emisor.descActividad = company.EconomicActivityName;
			jsondto.emisor.nombreComercial = company.TradeName;
			jsondto.emisor.tipoEstablecimiento = branch.TypeCode;
			jsondto.emisor.direccion.departamento = branch.RegionCode;
			jsondto.emisor.direccion.municipio = branch.CityCode;
			jsondto.emisor.direccion.complemento = branch.Address;
			jsondto.emisor.telefono = branch.Phone;
			jsondto.emisor.correo = branch.Email;
			jsondto.emisor.codEstable = branch.Code;
			jsondto.emisor.codPuntoVenta = invoice.PointSaleCode;

			jsondto.emisor.tipoItemExpor = invoice.Positions.ToList()[0].MaterialTypeCode == "B" ? 1 : 2;
			jsondto.emisor.recintoFiscal = jsondto.emisor.tipoItemExpor == 1 ? invoice.RecintoFiscalCode : null;
			jsondto.emisor.regimen = jsondto.emisor.tipoItemExpor == 1 ? invoice.RegimenExportCode : null;
			#endregion

			#region Receptor
			if (invoice.Sporadic && invoice.InvoiceSporadicPartner is not null)
			{
				if (invoice.InvoiceSporadicPartner.IDCode.IsNotNullOrEmpty())
					jsondto.receptor.tipoDocumento = invoice.InvoiceSporadicPartner.IDCode;

				if (invoice.InvoiceSporadicPartner.IDNumber.IsNotNullOrEmpty())
					jsondto.receptor.numDocumento = invoice.InvoiceSporadicPartner.IDNumber.Replace("-", "");

				jsondto.receptor.nombre = invoice.InvoiceSporadicPartner.Name;

				var pais = await dbContext.Country.AsNoTracking().FirstOrDefaultAsync(f => f.Id == invoice.InvoiceSporadicPartner.CountryId);
				if (pais is not null)
				{
					jsondto.receptor.codPais = pais.CodeMH;
					jsondto.receptor.nombrePais = pais.Name;
				}

				jsondto.receptor.complemento = invoice.InvoiceSporadicPartner.Address;

				jsondto.receptor.tipoPersona = Convert.ToInt16(invoice.InvoiceSporadicPartner.TypePerson);

				if (invoice.InvoiceSporadicPartner.EcoActivityName.IsNotNullOrEmpty())
					jsondto.receptor.descActividad = invoice.InvoiceSporadicPartner.EcoActivityName;


				jsondto.receptor.telefono = invoice.InvoiceSporadicPartner.Phone;
				jsondto.receptor.correo = invoice.InvoiceSporadicPartner.Email;

			}
			else
			{
				var partner = await partnerBL.GetInfoAsync(invoice.PartnerId ?? Guid.Empty);

				jsondto.receptor.tipoDocumento = partner.Nif1Code;
				jsondto.receptor.numDocumento = partner.Nif1.Replace("-", "");
				jsondto.receptor.nombre = partner.Name;
				jsondto.receptor.descActividad = partner.EcoActName;
				jsondto.receptor.codPais = partner.CountryAltCode;
				jsondto.receptor.nombrePais = partner.CountryName;
				jsondto.receptor.tipoPersona = Convert.ToInt16(partner.TypePerson);
				jsondto.receptor.complemento = partner.Address;
				jsondto.receptor.telefono = partner.Phone;
				jsondto.receptor.correo = partner.Email;

			}
			#endregion

			#region Cuerpo documento
			decimal sumFlete = 0, sumSeguro = 0;

			foreach (var pos in invoice.Positions)
			{
				decimal pu = 0;
				decimal netamount = pos.NetAmount;

				var taxpos = pos.Conditions.Where(x => x.Type == "I").ToList();

				var posFlt = pos.Conditions.FirstOrDefault(x => x.Code == "FLT");
				if (posFlt is not null)
				{
					netamount -= posFlt.ValueCondition;
					sumFlete += posFlt.ValueCondition;
				}


				var posSeg = pos.Conditions.FirstOrDefault(x => x.Code == "SEG");
				if (posSeg is not null)
				{
					netamount -= posSeg.ValueCondition;
					sumSeguro += posSeg.ValueCondition;
				}

				if (pos.PriceType == "GV" || pos.PriceType == "EX" || pos.PriceType == "NS")
					pu = (netamount + Math.Abs(pos.DiscountAmount)) / pos.Quantity;

				jsondto.cuerpoDocumento.Add(new FeFexV1DetailDTO
				{
					numItem = pos.Position + 1,
					cantidad = pos.Quantity,
					codigo = pos.MaterialCode,
					uniMedida = Convert.ToInt16(pos.UnitMeasureAltCode),
					descripcion = pos.MaterialName,
					precioUni = pu,
					montoDescu = Math.Abs(pos.DiscountAmount),
					ventaGravada = pos.PriceType == "GV" ? netamount : 0,
					noGravado = pos.PriceType == "NG" ? netamount : 0,
					tributos = taxpos.Select(x => x.AltCode).ToList(),
				});
			}
			#endregion

			#region Resumen            
			jsondto.resumen.totalGravada = jsondto.cuerpoDocumento.Sum(s => s.ventaGravada);
			jsondto.resumen.totalDescu = invoice.Positions.Sum(s => s.DiscountAmount);

			if (jsondto.emisor.tipoItemExpor == 1)
			{
				jsondto.resumen.seguro = sumSeguro;
				jsondto.resumen.flete = sumFlete;
			}

			jsondto.resumen.montoTotalOperacion += jsondto.resumen.totalGravada + jsondto.resumen.flete + jsondto.resumen.seguro;
			jsondto.resumen.totalNoGravado = invoice.Positions.Where(w => w.PriceType == "NG").Sum(s => s.NetAmount);

			jsondto.resumen.condicionOperacion = Convert.ToInt32(invoice.PaymentCondition.Tipo);
			jsondto.resumen.totalLetras = jsondto.resumen.totalPagar.AmountToLetters();

			var incoT = await dbContext.IncoTerms.AsNoTracking().FirstOrDefaultAsync(f => f.Id == invoice.IncoTermsId);
			if (incoT is not null)
			{
				jsondto.resumen.codIncoterms = incoT.Code;
				jsondto.resumen.descIncoterms = incoT.Name;
			}

			foreach (var payment in invoice.Payments)
			{
				jsondto.resumen.pagos.Add(new FePagosDTO
				{
					codigo = payment.MeanOfPaymentCode,
					montoPago = payment.Amount,
					referencia = payment.Reference,
					plazo = payment.Plazo == string.Empty ? null : payment.Plazo,
					periodo = payment.Periodo == 0 ? null : payment.Periodo,
				});
			}
			#endregion

			var genNum = dbContext.Database.SqlQuery<long>($"EXECUTE XSP_EBILLING_CO_NEXT_NUMBER {ebillingCom.CompanyId.ToString()},{invoice.InvoiceTypeId.ToString()}").ToList();
			if (genNum.Count == 0)
				throw new CustomTogoException("Error generando rango de número");

			invoice.NumControl = $"DTE-{ebillingDoc.Code}-{branch.Code.PadLeft(4, '0')}{invoice.PointSaleCode.PadLeft(4, '0')}-{genNum[0].ToString().PadLeft(15, '0')}";
			invoice.CodGeneracion = Guid.NewGuid().ToString().ToUpper();
			jsondto.identificacion.codigoGeneracion = invoice.CodGeneracion;
			jsondto.identificacion.numeroControl = invoice.NumControl;

			log = new EBillingLog
			{
				CompanyId = invoice.CompanyId,
				BranchId = invoice.BranchId,
				PointSaleId = invoice.PointSaleId,
				TipoDte = ebillingDoc.Code,
				CodGen = Guid.Parse(invoice.CodGeneracion),
				NumControl = invoice.NumControl,
				InvoiceId = invoice.Id,
                SaleOrderId = invoice.SaleOrderId,
                Request = JsonConvert.SerializeObject(jsondto)
            };

			var result = await PostDteAsyn(jsondto);

			if (result.StatusCode.Substring(0, 2) == "20")
			{
                if (result.StatusCode == "200")
                    result.Message = $"{invoice.InvoiceType.Name} {invoice.Number} creada correctamente";

                if (result.StatusCode == "201")
                {
                    result.Message = $"{invoice.InvoiceType.Name} {invoice.Number} creada por contingencia";
                    invoice.Contingency = true;
                    result.InvoiceId = invoice.Id;
                    await dbContext.EBillingLog.AddAsync(log);
                }


                if (result.StatusCode == "202")
                    result.Message = $"{invoice.InvoiceType.Name} {invoice.Number} creada con observaciones";

                if (result.ResponseMH is not null)
                {
                    invoice.SelloRecepcion = result.ResponseMH.selloRecibido;
                    invoice.FechaRecepcion = Convert.ToDateTime(result.ResponseMH.fhProcesamiento);
                }

                invoice.InvoiceType = null;
				invoice.PaymentCondition = null;
				invoice.EBillingDoc = ebillingDoc.Code;
                invoice.SentEmail = true;
                await dbContext.Invoice.AddAsync(invoice);
				await dbContext.SaveChangesAsync();

				await dbContext.Database.ExecuteSqlAsync($"UPDATE SaleOrder SET Invoiced = 1 WHERE Id={invoice.SaleOrderId.ToString()}");

				result.InvoiceId = invoice.Id;
			}

			return result;
		}

		private async Task<FeResponseDTO> SendNotaDebito(Invoice invoice)
		{
			var jsondto = new JsonNdV3();

			#region Identificación
			jsondto.identificacion.version = 3;
			jsondto.identificacion.ambiente = ebillingCom.IsProd ? "01" : "00";
			jsondto.identificacion.tipoDte = ebillingDoc.Code;
			jsondto.identificacion.tipoModelo = 1;
			jsondto.identificacion.tipoOperacion = 1;
			jsondto.identificacion.tipoContingencia = null;
			jsondto.identificacion.motivoContin = null;
			jsondto.identificacion.fecEmi = DateTime.Now.ToString("yyyy-MM-dd");
			jsondto.identificacion.horEmi = DateTime.Now.ToString("HH:mm:ss");
			jsondto.identificacion.tipoMoneda = invoice.CurrencyCode;
			#endregion

			#region Documento relacionado
			var invRef = await dbContext.Invoice.AsNoTracking().FirstOrDefaultAsync(f => f.Id == invoice.AssignmentId);
			if (invRef is null)
				throw new CustomTogoException($"Asignación: No existe una factura con esta referencia {invoice.Assignment}");

			jsondto.documentoRelacionado.Add(new FeRelatedDocumentDTO
			{
				tipoDocumento = invRef.EBillingDoc,
				tipoGeneracion = 2, // Siempre será 2
				numeroDocumento = invRef.CodGeneracion,
				fechaEmision = invRef.Date.ToString("yyy-MM-dd")
			});
			#endregion

			#region Emisor               
			var branch = await branchBL.GetBranchInfoAsync(invoice.BranchId);
			jsondto.emisor.nit = company.Nif1.Replace("-", "");
			jsondto.emisor.nrc = company.Nif2.Replace("-", "");
			jsondto.emisor.nombre = company.Name;
			jsondto.emisor.codActividad = company.EconomicActivityCode;
			jsondto.emisor.descActividad = company.EconomicActivityName;
			jsondto.emisor.nombreComercial = company.TradeName;
			jsondto.emisor.tipoEstablecimiento = branch.TypeCode;
			jsondto.emisor.direccion.departamento = branch.RegionCode;
			jsondto.emisor.direccion.municipio = branch.CityCode;
			jsondto.emisor.direccion.complemento = branch.Address;
			jsondto.emisor.telefono = branch.Phone;
			jsondto.emisor.correo = branch.Email;
			#endregion

			#region Receptor
			if (invoice.Sporadic && invoice.InvoiceSporadicPartner is not null)
			{
				jsondto.receptor.nit = invoice.InvoiceSporadicPartner.IDNumber.Replace("-", "");
				jsondto.receptor.nrc = invoice.InvoiceSporadicPartner.IDNumber2.Replace("-", "");
				jsondto.receptor.nombre = invoice.InvoiceSporadicPartner.Name;
				jsondto.receptor.codActividad = invoice.InvoiceSporadicPartner.EcoActivityCode;
				jsondto.receptor.descActividad = invoice.InvoiceSporadicPartner.EcoActivityName;
				jsondto.receptor.direccion.departamento = invoice.InvoiceSporadicPartner.RegionCode;
				jsondto.receptor.direccion.municipio = invoice.InvoiceSporadicPartner.CityCode;
				jsondto.receptor.direccion.complemento = invoice.InvoiceSporadicPartner.Address;
				jsondto.receptor.telefono = invoice.InvoiceSporadicPartner.Phone;
				jsondto.receptor.correo = invoice.InvoiceSporadicPartner.Email;

				jsondto.extension.nombRecibe = jsondto.receptor.nombre;
				jsondto.extension.docuRecibe = jsondto.receptor.nit;
			}
			else
			{
				var partner = await partnerBL.GetInfoAsync(invoice.PartnerId ?? Guid.Empty);

				jsondto.receptor.nit = partner.Nif1.Replace("-", "");
				jsondto.receptor.nrc = partner.Nif2.Replace("-", "");
				jsondto.receptor.nombre = partner.Name;
				jsondto.receptor.codActividad = partner.EcoActCode;
				jsondto.receptor.descActividad = partner.EcoActName;
				jsondto.receptor.nombreComercial = partner.TradeName.IsNullOrEmpty() ? null : partner.TradeName;
				jsondto.receptor.direccion.departamento = partner.RegionCode;
				jsondto.receptor.direccion.municipio = partner.CityCode;
				jsondto.receptor.direccion.complemento = partner.Address;
				jsondto.receptor.telefono = partner.Phone;
				jsondto.receptor.correo = partner.Email;

				if (string.IsNullOrEmpty(partner.ContactPersonName))
				{
					jsondto.extension.nombRecibe = jsondto.receptor.nombre;
					jsondto.extension.docuRecibe = jsondto.receptor.nit;
				}
				else
				{
					jsondto.extension.nombRecibe = partner.ContactPersonName;
					jsondto.extension.docuRecibe = partner.ContactPersonIDNumber;
				}
			}
			#endregion

			#region Cuerpo del documento
			List<InvoicePositionCondition> taxes = new List<InvoicePositionCondition>();
			foreach (var pos in invoice.Positions)
			{
				decimal pu = 0;

				if (pos.PriceType == "GV" || pos.PriceType == "EX" || pos.PriceType == "NS")
					pu = (pos.NetAmount + Math.Abs(pos.DiscountAmount)) / pos.Quantity;

				var taxpos = pos.Conditions.Where(x => x.Type == "I").ToList();

				if (taxpos.Any() && pos.PriceType == "GV")
					foreach (var t in taxpos)
					{
						t.ValueCondition = t.ValueCondition * pos.Quantity;
						var exists = taxes.FirstOrDefault(f => f.AltCode == t.AltCode);
						if (exists is null)
						{
							taxes.Add(t);
						}
						else
						{
							exists.ValueCondition += t.ValueCondition;
						}
					}

				jsondto.cuerpoDocumento.Add(new FeNdV3DetailDTO
				{
					numItem = pos.Position + 1,
					tipoItem = pos.MaterialTypeCode == "B" ? 1 : 2,
					numeroDocumento = invRef.CodGeneracion,
					cantidad = pos.Quantity,
					codigo = pos.MaterialCode,
					uniMedida = Convert.ToInt16(pos.UnitMeasureAltCode),
					descripcion = pos.MaterialName,
					precioUni = pu,
					montoDescu = Math.Abs(pos.DiscountAmount),
					ventaNoSuj = pos.PriceType == "NS" ? pos.NetAmount : 0,
					ventaExenta = pos.PriceType == "EX" ? pos.NetAmount : 0,
					ventaGravada = pos.PriceType == "GV" ? pos.NetAmount : 0,
					tributos = pos.PriceType == "GV" ? taxpos.Select(x => x.AltCode).ToList() : null,
				});
			}
			#endregion

			#region Resumen
			jsondto.resumen.totalNoSuj = invoice.Positions.Where(w => w.PriceType == "NS").Sum(s => s.NetAmount);
			jsondto.resumen.totalExenta = invoice.Positions.Where(w => w.PriceType == "EX").Sum(s => s.NetAmount);
			jsondto.resumen.totalGravada = invoice.Positions.Where(w => w.PriceType == "GV").Sum(s => s.NetAmount);
			jsondto.resumen.totalDescu = Math.Abs(invoice.Positions.Sum(s => s.DiscountAmount));

			if (taxes.Any())
			{
				foreach (var tax in taxes)
				{
					var etax = ebillingTax.FirstOrDefault(f => f.TaxCode == tax.AltCode);
					if (etax is not null)
					{
						jsondto.resumen.tributos.Add(new FeTaxesDTO
						{
							codigo = tax.AltCode,
							descripcion = etax.TaxName,
							valor = Math.Round(tax.ValueCondition, 2),
						});

						jsondto.resumen.montoTotalOperacion += Math.Round(tax.ValueCondition, 2);
					}

				}
			}
			else jsondto.resumen.tributos = null;

            jsondto.resumen.ivaRete1 = Math.Round(Math.Abs(invoice.Ret1), 2);
            jsondto.resumen.reteRenta = Math.Round(Math.Abs(invoice.Ret10), 2);

            jsondto.resumen.montoTotalOperacion += jsondto.resumen.subTotal;

			jsondto.resumen.totalLetras = jsondto.resumen.montoTotalOperacion.AmountToLetters();

			jsondto.resumen.condicionOperacion = Convert.ToInt32(invoice.PaymentCondition.Tipo);
			#endregion

			var genNum = dbContext.Database.SqlQuery<long>($"EXECUTE XSP_EBILLING_CO_NEXT_NUMBER {ebillingCom.CompanyId.ToString()},{invoice.InvoiceTypeId.ToString()}").ToList();
			if (genNum.Count == 0)
				throw new CustomTogoException("Error generando rango de número");

			invoice.NumControl = $"DTE-{ebillingDoc.Code}-{branch.Code.PadLeft(4, '0')}{invoice.PointSaleCode.PadLeft(4, '0')}-{genNum[0].ToString().PadLeft(15, '0')}";
			invoice.CodGeneracion = Guid.NewGuid().ToString().ToUpper();
			jsondto.identificacion.codigoGeneracion = invoice.CodGeneracion;
			jsondto.identificacion.numeroControl = invoice.NumControl;

			log = new EBillingLog
			{
				CompanyId = invoice.CompanyId,
				BranchId = invoice.BranchId,
				PointSaleId = invoice.PointSaleId,
				TipoDte = ebillingDoc.Code,
				CodGen = Guid.Parse(invoice.CodGeneracion),
				NumControl = invoice.NumControl,
				InvoiceId = invoice.Id,
                SaleOrderId = invoice.SaleOrderId,
                Request = JsonConvert.SerializeObject(jsondto)
            };

			var result = await PostDteAsyn(jsondto);

			if (result.StatusCode.Substring(0, 2) == "20")
			{
                if (result.StatusCode == "200")
                    result.Message = $"{invoice.InvoiceType.Name} {invoice.Number} creada correctamente";

                if (result.StatusCode == "201")
                {
                    result.Message = $"{invoice.InvoiceType.Name} {invoice.Number} creada por contingencia";
                    invoice.Contingency = true;
                    result.InvoiceId = invoice.Id;
                    await dbContext.EBillingLog.AddAsync(log);
                }


                if (result.StatusCode == "202")
                    result.Message = $"{invoice.InvoiceType.Name} {invoice.Number} creada con observaciones";

                if (result.ResponseMH is not null)
                {
                    invoice.SelloRecepcion = result.ResponseMH.selloRecibido;
                    invoice.FechaRecepcion = Convert.ToDateTime(result.ResponseMH.fhProcesamiento);
                }

                invoice.InvoiceType = null;
				invoice.PaymentCondition = null;
				invoice.EBillingDoc = ebillingDoc.Code;
                invoice.SentEmail = true;                
                await dbContext.Invoice.AddAsync(invoice);
				await dbContext.SaveChangesAsync();

				await dbContext.Database.ExecuteSqlAsync($"UPDATE SaleOrder SET Invoiced = 1 WHERE Id={invoice.SaleOrderId.ToString()}");

				result.InvoiceId = invoice.Id;
			}

			return result;
		}

		private async Task<FeResponseDTO> SendNotaCredito(Invoice invoice)
		{
			var jsondto = new JsonNcV3();

			#region Identificación
			jsondto.identificacion.version = 3;
			jsondto.identificacion.ambiente = ebillingCom.IsProd ? "01" : "00";
			jsondto.identificacion.tipoDte = ebillingDoc.Code;
			jsondto.identificacion.tipoModelo = 1;
			jsondto.identificacion.tipoOperacion = 1;
			jsondto.identificacion.tipoContingencia = null;
			jsondto.identificacion.motivoContin = null;
			jsondto.identificacion.fecEmi = DateTime.Now.ToString("yyyy-MM-dd");
			jsondto.identificacion.horEmi = DateTime.Now.ToString("HH:mm:ss");
			jsondto.identificacion.tipoMoneda = invoice.CurrencyCode;
			#endregion

			#region Documento relacionado
			var invRef = await dbContext.Invoice.AsNoTracking().FirstOrDefaultAsync(f => f.Id == invoice.AssignmentId);
			if (invRef is null)
				throw new CustomTogoException($"Asignación: No existe una factura con esta referencia {invoice.Assignment}");

			jsondto.documentoRelacionado.Add(new FeRelatedDocumentDTO
			{
				tipoDocumento = invRef.EBillingDoc,
				tipoGeneracion = 2, // Siempre será 2
				numeroDocumento = invRef.CodGeneracion,
				fechaEmision = invRef.Date.ToString("yyy-MM-dd")
			});
			#endregion

			#region Emisor               
			var branch = await branchBL.GetBranchInfoAsync(invoice.BranchId);
			jsondto.emisor.nit = company.Nif1.Replace("-", "");
			jsondto.emisor.nrc = company.Nif2.Replace("-", "");
			jsondto.emisor.nombre = company.Name;
			jsondto.emisor.codActividad = company.EconomicActivityCode;
			jsondto.emisor.descActividad = company.EconomicActivityName;
			jsondto.emisor.nombreComercial = company.TradeName;
			jsondto.emisor.tipoEstablecimiento = branch.TypeCode;
			jsondto.emisor.direccion.departamento = branch.RegionCode;
			jsondto.emisor.direccion.municipio = branch.CityCode;
			jsondto.emisor.direccion.complemento = branch.Address;
			jsondto.emisor.telefono = branch.Phone;
			jsondto.emisor.correo = branch.Email;
			#endregion

			#region Receptor
			if (invoice.Sporadic && invoice.InvoiceSporadicPartner is not null)
			{
				jsondto.receptor.nit = invoice.InvoiceSporadicPartner.IDNumber.Replace("-", "");
				jsondto.receptor.nrc = invoice.InvoiceSporadicPartner.IDNumber2.Replace("-", "");
				jsondto.receptor.nombre = invoice.InvoiceSporadicPartner.Name;
				jsondto.receptor.codActividad = invoice.InvoiceSporadicPartner.EcoActivityCode;
				jsondto.receptor.descActividad = invoice.InvoiceSporadicPartner.EcoActivityName;
				jsondto.receptor.direccion.departamento = invoice.InvoiceSporadicPartner.RegionCode;
				jsondto.receptor.direccion.municipio = invoice.InvoiceSporadicPartner.CityCode;
				jsondto.receptor.direccion.complemento = invoice.InvoiceSporadicPartner.Address;
				jsondto.receptor.telefono = invoice.InvoiceSporadicPartner.Phone;
				jsondto.receptor.correo = invoice.InvoiceSporadicPartner.Email;

				jsondto.extension.nombRecibe = jsondto.receptor.nombre;
				jsondto.extension.docuRecibe = jsondto.receptor.nit;
			}
			else
			{
				var partner = await partnerBL.GetInfoAsync(invoice.PartnerId ?? Guid.Empty);

				jsondto.receptor.nit = partner.Nif1.Replace("-", "");
				jsondto.receptor.nrc = partner.Nif2.Replace("-", "");
				jsondto.receptor.nombre = partner.Name;
				jsondto.receptor.codActividad = partner.EcoActCode;
				jsondto.receptor.descActividad = partner.EcoActName;
				jsondto.receptor.nombreComercial = partner.TradeName.IsNullOrEmpty() ? null : partner.TradeName;
				jsondto.receptor.direccion.departamento = partner.RegionCode;
				jsondto.receptor.direccion.municipio = partner.CityCode;
				jsondto.receptor.direccion.complemento = partner.Address;
				jsondto.receptor.telefono = partner.Phone;
				jsondto.receptor.correo = partner.Email;

				if (string.IsNullOrEmpty(partner.ContactPersonName))
				{
					jsondto.extension.nombRecibe = jsondto.receptor.nombre;
					jsondto.extension.docuRecibe = jsondto.receptor.nit;
				}
				else
				{
					jsondto.extension.nombRecibe = partner.ContactPersonName;
					jsondto.extension.docuRecibe = partner.ContactPersonIDNumber;
				}
			}
			#endregion

			#region Cuerpo del documento
			List<InvoicePositionCondition> taxes = new List<InvoicePositionCondition>();
			foreach (var pos in invoice.Positions)
			{
				decimal pu = 0;

				if (pos.PriceType == "GV" || pos.PriceType == "EX" || pos.PriceType == "NS")
					pu = (pos.NetAmount + Math.Abs(pos.DiscountAmount)) / pos.Quantity;

				var taxpos = pos.Conditions.Where(x => x.Type == "I").ToList();

				if (taxpos.Any() && pos.PriceType == "GV")
					foreach (var t in taxpos)
					{
						t.ValueCondition = t.ValueCondition * pos.Quantity;
						var exists = taxes.FirstOrDefault(f => f.AltCode == t.AltCode);
						if (exists is null)
						{
							taxes.Add(t);
						}
						else
						{
							exists.ValueCondition += t.ValueCondition;
						}
					}

				jsondto.cuerpoDocumento.Add(new FeNcV3DetailDTO
				{
					numItem = pos.Position + 1,
					tipoItem = pos.MaterialTypeCode == "B" ? 1 : 2,
					numeroDocumento = invRef.CodGeneracion,
					cantidad = pos.Quantity,
					codigo = pos.MaterialCode,
					uniMedida = Convert.ToInt16(pos.UnitMeasureAltCode),
					descripcion = pos.MaterialName,
					precioUni = pu,
					montoDescu = Math.Abs(pos.DiscountAmount),
					ventaNoSuj = pos.PriceType == "NS" ? pos.NetAmount : 0,
					ventaExenta = pos.PriceType == "EX" ? pos.NetAmount : 0,
					ventaGravada = pos.PriceType == "GV" ? pos.NetAmount : 0,
					tributos = pos.PriceType == "GV" ? taxpos.Select(x => x.AltCode).ToList() : null,
				});
			}
			#endregion

			#region Resumen
			jsondto.resumen.totalNoSuj = invoice.Positions.Where(w => w.PriceType == "NS").Sum(s => s.NetAmount);
			jsondto.resumen.totalExenta = invoice.Positions.Where(w => w.PriceType == "EX").Sum(s => s.NetAmount);
			jsondto.resumen.totalGravada = invoice.Positions.Where(w => w.PriceType == "GV").Sum(s => s.NetAmount);
			jsondto.resumen.totalDescu = Math.Abs(invoice.Positions.Sum(s => s.DiscountAmount));

			if (taxes.Any())
			{
				foreach (var tax in taxes)
				{
					var etax = ebillingTax.FirstOrDefault(f => f.TaxCode == tax.AltCode);
					if (etax is not null)
					{
						jsondto.resumen.tributos.Add(new FeTaxesDTO
						{
							codigo = tax.AltCode,
							descripcion = etax.TaxName,
							valor = Math.Round(tax.ValueCondition, 2),
						});

						jsondto.resumen.montoTotalOperacion += Math.Round(tax.ValueCondition, 2);
					}

				}
			}
			else jsondto.resumen.tributos = null;

			jsondto.resumen.ivaRete1 = Math.Round(Math.Abs(invoice.Ret1),2);
			jsondto.resumen.reteRenta = Math.Round(Math.Abs(invoice.Ret10),2);

			jsondto.resumen.montoTotalOperacion += jsondto.resumen.subTotal;

			jsondto.resumen.totalLetras = jsondto.resumen.montoTotalOperacion.AmountToLetters();

			jsondto.resumen.condicionOperacion = Convert.ToInt32(invoice.PaymentCondition.Tipo);
			#endregion

			var genNum = dbContext.Database.SqlQuery<long>($"EXECUTE XSP_EBILLING_CO_NEXT_NUMBER {ebillingCom.CompanyId.ToString()},{invoice.InvoiceTypeId.ToString()}").ToList();
			if (genNum.Count == 0)
				throw new CustomTogoException("Error generando rango de número");

			invoice.NumControl = $"DTE-{ebillingDoc.Code}-{branch.Code.PadLeft(4, '0')}{invoice.PointSaleCode.PadLeft(4, '0')}-{genNum[0].ToString().PadLeft(15, '0')}";
			invoice.CodGeneracion = Guid.NewGuid().ToString().ToUpper();
			jsondto.identificacion.codigoGeneracion = invoice.CodGeneracion;
			jsondto.identificacion.numeroControl = invoice.NumControl;

			log = new EBillingLog
			{
				CompanyId = invoice.CompanyId,
				BranchId = invoice.BranchId,
				PointSaleId = invoice.PointSaleId,
				TipoDte = ebillingDoc.Code,
				CodGen = Guid.Parse(invoice.CodGeneracion),
				NumControl = invoice.NumControl,
				InvoiceId = invoice.Id,
                SaleOrderId = invoice.SaleOrderId,
                Request = JsonConvert.SerializeObject(jsondto)
            };

			var result = await PostDteAsyn(jsondto);

			if (result.StatusCode.Substring(0, 2) == "20")
			{
                if (result.StatusCode == "200")
                    result.Message = $"{invoice.InvoiceType.Name}   {invoice.Number} creada correctamente";

                if (result.StatusCode == "201")
                {
                    result.Message = $"{invoice.InvoiceType.Name} {invoice.Number} creada por contingencia";
                    invoice.Contingency = true;
                    result.InvoiceId = invoice.Id;
                    await dbContext.EBillingLog.AddAsync(log);
                }


                if (result.StatusCode == "202")
                    result.Message = $"{invoice.InvoiceType.Name} {invoice.Number} creada con observaciones";

                if (result.ResponseMH is not null)
                {
                    invoice.SelloRecepcion = result.ResponseMH.selloRecibido;
                    invoice.FechaRecepcion = Convert.ToDateTime(result.ResponseMH.fhProcesamiento);
                }

                invoice.InvoiceType = null;
                invoice.PaymentCondition = null;
                invoice.EBillingDoc = ebillingDoc.Code;
                invoice.SentEmail = true;                
                await dbContext.Invoice.AddAsync(invoice);
				await dbContext.SaveChangesAsync();

				await dbContext.Database.ExecuteSqlAsync($"UPDATE SaleOrder SET Invoiced = 1 WHERE Id={invoice.SaleOrderId.ToString()}");

				result.InvoiceId = invoice.Id;
			}

			return result;
		}

		private async Task<FeResponseDTO> SendCreditoFiscalAsync(Invoice invoice)
		{
			var jsondto = new JsonFeCcfV3();

			#region Identificación
			jsondto.identificacion.version = 3;
			jsondto.identificacion.ambiente = ebillingCom.IsProd ? "01" : "00";
			jsondto.identificacion.tipoDte = ebillingDoc.Code;
			jsondto.identificacion.tipoModelo = 1;
			jsondto.identificacion.tipoOperacion = 1;
			jsondto.identificacion.tipoContingencia = null;
			jsondto.identificacion.motivoContin = null;
			jsondto.identificacion.fecEmi = DateTime.Now.ToString("yyyy-MM-dd");
			jsondto.identificacion.horEmi = DateTime.Now.ToString("HH:mm:ss");
			jsondto.identificacion.tipoMoneda = invoice.CurrencyCode;
			#endregion

			#region Emisor               
			var branch = await branchBL.GetBranchInfoAsync(invoice.BranchId);

			jsondto.emisor.nit = company.Nif1.Replace("-", "");
			jsondto.emisor.nrc = company.Nif2.Replace("-", "");
			jsondto.emisor.nombre = company.Name;
			jsondto.emisor.codActividad = company.EconomicActivityCode;
			jsondto.emisor.descActividad = company.EconomicActivityName;
			jsondto.emisor.nombreComercial = company.TradeName;
			jsondto.emisor.tipoEstablecimiento = branch.TypeCode;
			jsondto.emisor.direccion.departamento = branch.RegionCode;
			jsondto.emisor.direccion.municipio = branch.CityCode;
			jsondto.emisor.direccion.complemento = branch.Address;
			jsondto.emisor.telefono = branch.Phone;
			jsondto.emisor.correo = branch.Email;
			jsondto.emisor.codEstable = branch.Code;
			jsondto.emisor.codPuntoVenta = invoice.PointSaleCode;
			#endregion

			#region Receptor
			if (invoice.Sporadic && invoice.InvoiceSporadicPartner is not null)
			{
				jsondto.receptor.nit = invoice.InvoiceSporadicPartner.IDNumber.Replace("-", "");
				jsondto.receptor.nrc = invoice.InvoiceSporadicPartner.IDNumber2.Replace("-", "");
				jsondto.receptor.nombre = invoice.InvoiceSporadicPartner.Name;
				jsondto.receptor.codActividad = invoice.InvoiceSporadicPartner.EcoActivityCode;
				jsondto.receptor.descActividad = invoice.InvoiceSporadicPartner.EcoActivityName;
				jsondto.receptor.direccion.departamento = invoice.InvoiceSporadicPartner.RegionCode;
				jsondto.receptor.direccion.municipio = invoice.InvoiceSporadicPartner.CityCode;
				jsondto.receptor.direccion.complemento = invoice.InvoiceSporadicPartner.Address;
				jsondto.receptor.telefono = invoice.InvoiceSporadicPartner.Phone;
				jsondto.receptor.correo = invoice.InvoiceSporadicPartner.Email;

				jsondto.extension.nombRecibe = jsondto.receptor.nombre;
				jsondto.extension.docuRecibe = jsondto.receptor.nit;
			}
			else
			{
				var partner = await partnerBL.GetInfoAsync(invoice.PartnerId ?? Guid.Empty);

				jsondto.receptor.nit = partner.Nif1.Replace("-", "");
				jsondto.receptor.nrc = partner.Nif2.Replace("-", "");
				jsondto.receptor.nombre = partner.Name;
				jsondto.receptor.codActividad = partner.EcoActCode;
				jsondto.receptor.descActividad = partner.EcoActName;
				jsondto.receptor.nombreComercial = partner.TradeName.IsNullOrEmpty() ? null : partner.TradeName;
				jsondto.receptor.direccion.departamento = partner.RegionCode;
				jsondto.receptor.direccion.municipio = partner.CityCode;
				jsondto.receptor.direccion.complemento = partner.Address;
				jsondto.receptor.telefono = partner.Phone;
				jsondto.receptor.correo = partner.Email;

				if (string.IsNullOrEmpty(partner.ContactPersonName))
				{
					jsondto.extension.nombRecibe = jsondto.receptor.nombre;
					jsondto.extension.docuRecibe = jsondto.receptor.nit;
				}
				else
				{
					jsondto.extension.nombRecibe = partner.ContactPersonName;
					jsondto.extension.docuRecibe = partner.ContactPersonIDNumber;
				}
			}
			#endregion

			#region Cuerpo del documento
			List<InvoicePositionCondition> taxes = new List<InvoicePositionCondition>();
			foreach (var pos in invoice.Positions)
			{
				decimal pu = 0;

				if (pos.PriceType == "GV" || pos.PriceType == "EX" || pos.PriceType == "NS")
					pu = (pos.NetAmount + Math.Abs(pos.DiscountAmount)) / pos.Quantity;

				var taxpos = pos.Conditions.Where(x => x.Type == "I").ToList();

				if (taxpos.Any() && pos.PriceType == "GV")
					foreach (var t in taxpos)
					{
						t.ValueCondition = t.ValueCondition * pos.Quantity;
						var exists = taxes.FirstOrDefault(f => f.AltCode == t.AltCode);
						if (exists is null)
						{
							taxes.Add(t);
						}
						else
						{
							exists.ValueCondition += t.ValueCondition;
						}
					}

				jsondto.cuerpoDocumento.Add(new FeCcfV3DetailDTO
				{
					numItem = pos.Position + 1,
					tipoItem = pos.MaterialTypeCode == "B" ? 1 : 2,
					cantidad = pos.Quantity,
					codigo = pos.MaterialCode,
					uniMedida = Convert.ToInt16(pos.UnitMeasureAltCode),
					descripcion = pos.MaterialName,
					precioUni = pu,
					montoDescu = Math.Abs(pos.DiscountAmount),
					ventaNoSuj = pos.PriceType == "NS" ? pos.NetAmount : 0,
					ventaExenta = pos.PriceType == "EX" ? pos.NetAmount : 0,
					ventaGravada = pos.PriceType == "GV" ? pos.NetAmount : 0,
					psv = pu,
					noGravado = pos.PriceType == "NG" ? pos.NetAmount : 0,
					tributos = pos.PriceType == "GV" ? taxpos.Select(x => x.AltCode).ToList() : null,
				});
			}
			#endregion

			#region Resumen
			jsondto.resumen.totalNoSuj = invoice.Positions.Where(w => w.PriceType == "NS").Sum(s => s.NetAmount);
			jsondto.resumen.totalExenta = invoice.Positions.Where(w => w.PriceType == "EX").Sum(s => s.NetAmount);
			jsondto.resumen.totalGravada = invoice.Positions.Where(w => w.PriceType == "GV").Sum(s => s.NetAmount);
			jsondto.resumen.totalDescu = Math.Abs(invoice.Positions.Sum(s => s.DiscountAmount));

			if (taxes.Any())
			{
				foreach (var tax in taxes)
				{
					var etax = ebillingTax.FirstOrDefault(f => f.TaxCode == tax.AltCode);
					if (etax is not null)
					{
						jsondto.resumen.tributos.Add(new FeTaxesDTO
						{
							codigo = tax.AltCode,
							descripcion = etax.TaxName,
							valor = Math.Round(tax.ValueCondition, 2)
						});

						jsondto.resumen.montoTotalOperacion += Math.Round(tax.ValueCondition, 2);
					}

				}
			}
			else jsondto.resumen.tributos = null;

            jsondto.resumen.ivaRete1 = Math.Round(Math.Abs(invoice.Ret1), 2); ;
            jsondto.resumen.reteRenta = Math.Round(Math.Abs(invoice.Ret10), 2);
            jsondto.resumen.totalNoGravado = invoice.Positions.Where(w => w.PriceType == "NG").Sum(s => s.NetAmount);

			jsondto.resumen.condicionOperacion = Convert.ToInt32(invoice.PaymentCondition.Tipo);

			jsondto.resumen.subTotal = jsondto.resumen.subTotalVentas;

			jsondto.resumen.montoTotalOperacion += jsondto.resumen.subTotal;

			jsondto.resumen.totalLetras = jsondto.resumen.totalPagar.AmountToLetters();

			foreach (var payment in invoice.Payments)
			{
				jsondto.resumen.pagos.Add(new FePagosDTO
				{
					codigo = payment.MeanOfPaymentCode,
					montoPago = payment.Amount,
					referencia = payment.Reference,
					plazo = payment.Plazo == string.Empty ? null : payment.Plazo,
					periodo = payment.Periodo == 0 ? null : payment.Periodo,
				});
			}
			#endregion

			#region Extensión
			jsondto.extension.nombEntrega = currentUser.Name;
			jsondto.extension.docuEntrega = currentUser.IDNumber;
			#endregion

			var genNum = dbContext.Database.SqlQuery<long>($"EXECUTE XSP_EBILLING_CO_NEXT_NUMBER {ebillingCom.CompanyId.ToString()},{invoice.InvoiceTypeId.ToString()}").ToList();
			if (genNum.Count == 0)
				throw new CustomTogoException("Error generando rango de número");

			invoice.NumControl = $"DTE-{ebillingDoc.Code}-{branch.Code.PadLeft(4, '0')}{invoice.PointSaleCode.PadLeft(4, '0')}-{genNum[0].ToString().PadLeft(15, '0')}";
			invoice.CodGeneracion = Guid.NewGuid().ToString().ToUpper();
			jsondto.identificacion.codigoGeneracion = invoice.CodGeneracion;
			jsondto.identificacion.numeroControl = invoice.NumControl;

			log = new EBillingLog
			{
				CompanyId = invoice.CompanyId,
				BranchId = invoice.BranchId,
				PointSaleId = invoice.PointSaleId,
				TipoDte = ebillingDoc.Code,
				CodGen = Guid.Parse(invoice.CodGeneracion),
				NumControl = invoice.NumControl,
				InvoiceId = invoice.Id,
                SaleOrderId = invoice.SaleOrderId,
                Request = JsonConvert.SerializeObject(jsondto)
            };

			var result = await PostDteAsyn(jsondto);

			if (result.StatusCode.Substring(0, 2) == "20")
			{
                if (result.StatusCode == "200")
                    result.Message = $"{invoice.InvoiceType.Name}  {invoice.Number} creado correctamente";

                if (result.StatusCode == "201")
                {
                    result.Message = $"{invoice.InvoiceType.Name} {invoice.Number} creado por contingencia";
                    invoice.Contingency = true;
                    result.InvoiceId = invoice.Id;
                    await dbContext.EBillingLog.AddAsync(log);
                }


                if (result.StatusCode == "202")
                    result.Message = $"{invoice.InvoiceType.Name} {invoice.Number} creado con observaciones";

                if (result.ResponseMH is not null)
                {
                    invoice.SelloRecepcion = result.ResponseMH.selloRecibido;
                    invoice.FechaRecepcion = Convert.ToDateTime(result.ResponseMH.fhProcesamiento);
                }

                invoice.InvoiceType = null;
				invoice.PaymentCondition = null;
				invoice.EBillingDoc = ebillingDoc.Code;
                invoice.SentEmail = true;                
				await dbContext.Invoice.AddAsync(invoice);
				await dbContext.SaveChangesAsync();

				await dbContext.Database.ExecuteSqlAsync($"UPDATE SaleOrder SET Invoiced = 1 WHERE Id={invoice.SaleOrderId.ToString()}");

				result.InvoiceId = invoice.Id;
			}

			return result;
		}

		private async Task<FeResponseDTO> SendFacturaAsync(Invoice invoice)
		{
			var jsondto = new JsonFeFcV1();

			#region Identificación
			jsondto.identificacion.version = 1;
			jsondto.identificacion.ambiente = ebillingCom.IsProd ? "01" : "00";
			jsondto.identificacion.tipoDte = ebillingDoc.Code;
			jsondto.identificacion.tipoModelo = 1;
			jsondto.identificacion.tipoOperacion = 1;
			jsondto.identificacion.tipoContingencia = null;
			jsondto.identificacion.motivoContin = null;
			jsondto.identificacion.fecEmi = DateTime.Now.ToString("yyyy-MM-dd");
			jsondto.identificacion.horEmi = DateTime.Now.ToString("HH:mm:ss");
			jsondto.identificacion.tipoMoneda = invoice.CurrencyCode;
			#endregion

			#region Emisor               
			var branch = await branchBL.GetBranchInfoAsync(invoice.BranchId);

			jsondto.emisor.nit = company.Nif1.Replace("-", "");
			jsondto.emisor.nrc = company.Nif2.Replace("-", "");
			jsondto.emisor.nombre = company.Name;
			jsondto.emisor.codActividad = company.EconomicActivityCode;
			jsondto.emisor.descActividad = company.EconomicActivityName;
			jsondto.emisor.nombreComercial = company.TradeName;
			jsondto.emisor.tipoEstablecimiento = branch.TypeCode;
			jsondto.emisor.direccion.departamento = branch.RegionCode;
			jsondto.emisor.direccion.municipio = branch.CityCode;
			jsondto.emisor.direccion.complemento = branch.Address;
			jsondto.emisor.telefono = branch.Phone;
			jsondto.emisor.correo = branch.Email;
			jsondto.emisor.codEstable = branch.Code;
			jsondto.emisor.codPuntoVenta = invoice.PointSaleCode;
			#endregion

			#region Receptor
			if (invoice.Sporadic && invoice.InvoiceSporadicPartner is not null)
			{
				if (invoice.InvoiceSporadicPartner.IDCode.IsNotNullOrEmpty())
					jsondto.receptor.tipoDocumento = invoice.InvoiceSporadicPartner.IDCode;

				if (invoice.InvoiceSporadicPartner.IDNumber.IsNotNullOrEmpty())
					jsondto.receptor.numDocumento = invoice.InvoiceSporadicPartner.IDNumber.Replace("-", "");

				jsondto.receptor.nombre = invoice.InvoiceSporadicPartner.Name;

				if (invoice.InvoiceSporadicPartner.EcoActivityCode.IsNotNullOrEmpty())
					jsondto.receptor.codActividad = invoice.InvoiceSporadicPartner.EcoActivityCode;

				if (invoice.InvoiceSporadicPartner.EcoActivityName.IsNotNullOrEmpty())
					jsondto.receptor.descActividad = invoice.InvoiceSporadicPartner.EcoActivityName;


				if (invoice.InvoiceSporadicPartner.Address.IsNotNullOrEmpty())
				{
					jsondto.receptor.direccion.departamento = invoice.InvoiceSporadicPartner.RegionCode;
					jsondto.receptor.direccion.municipio = invoice.InvoiceSporadicPartner.CityCode;
					jsondto.receptor.direccion.complemento = invoice.InvoiceSporadicPartner.Address;
				}
				else jsondto.receptor.direccion = null;


				if (invoice.InvoiceSporadicPartner.Phone.IsNotNullOrEmpty())
					jsondto.receptor.telefono = invoice.InvoiceSporadicPartner.Phone;

				if (invoice.InvoiceSporadicPartner.Email.IsNotNullOrEmpty())
					jsondto.receptor.correo = invoice.InvoiceSporadicPartner.Email;

				jsondto.extension.nombRecibe = jsondto.receptor.nombre;
				jsondto.extension.docuRecibe = jsondto.receptor.numDocumento;
			}
			else
			{
				var partner = await partnerBL.GetInfoAsync(invoice.PartnerId ?? Guid.Empty);

				jsondto.receptor.tipoDocumento = partner.Nif1Code;
				jsondto.receptor.numDocumento = partner.Nif1.Replace("-", "");
				jsondto.receptor.nombre = partner.Name;
				jsondto.receptor.codActividad = partner.EcoActCode;
				jsondto.receptor.descActividad = partner.EcoActName;
				jsondto.receptor.direccion.departamento = partner.RegionCode;
				jsondto.receptor.direccion.municipio = partner.CityCode;
				jsondto.receptor.direccion.complemento = partner.Address;
				jsondto.receptor.telefono = partner.Phone;
				jsondto.receptor.correo = partner.Email;

				if (string.IsNullOrEmpty(partner.ContactPersonName))
				{
					jsondto.extension.nombRecibe = jsondto.receptor.nombre;
					jsondto.extension.docuRecibe = jsondto.receptor.numDocumento;
				}
				else
				{
					jsondto.extension.nombRecibe = partner.ContactPersonName;
					jsondto.extension.docuRecibe = partner.ContactPersonIDNumber;
				}
			}
			#endregion

			#region Cuerpo del documento
			foreach (var pos in invoice.Positions)
			{
				decimal pu = 0;

				if (pos.PriceType == "GV" || pos.PriceType == "EX")
					pu = ((pos.NetAmount + Math.Abs(pos.DiscountAmount) + pos.TaxAmount) / pos.Quantity);

				if (pos.PriceType == "NS")
					pu = pos.NetAmount / pos.Quantity;

				jsondto.cuerpoDocumento.Add(new FeFcDetailDTO
				{
					numItem = pos.Position + 1,
					tipoItem = pos.MaterialTypeCode == "B" ? 1 : 2,
					cantidad = pos.Quantity,
					codigo = pos.MaterialCode,
					uniMedida = Convert.ToInt16(pos.UnitMeasureAltCode),
					descripcion = pos.MaterialName,
					precioUni = pu,
					montoDescu = Math.Abs(pos.DiscountAmount),
					ventaNoSuj = pos.PriceType == "NS" ? pos.NetAmount : 0,
					ventaExenta = pos.PriceType == "EX" ? pos.NetAmount : 0,
					ventaGravada = pos.PriceType == "GV" ? (pos.NetAmount + pos.TaxAmount) : 0,
					psv = pu,
					noGravado = pos.PriceType == "NG" ? pos.NetAmount : 0,
					ivaItem = pos.TaxAmount
				});
			}
			#endregion

			#region Resumen
			jsondto.resumen.totalNoSuj = invoice.Positions.Where(w => w.PriceType == "NS").Sum(s => s.NetAmount);
			jsondto.resumen.totalExenta = invoice.Positions.Where(w => w.PriceType == "EX").Sum(s => s.NetAmount);
			jsondto.resumen.totalGravada = invoice.Positions.Where(w => w.PriceType == "GV").Sum(s => s.NetAmount + s.TaxAmount);
			jsondto.resumen.totalDescu = Math.Abs(invoice.Positions.Sum(s => s.DiscountAmount));
            jsondto.resumen.ivaRete1 = Math.Round(Math.Abs(invoice.Ret1), 2);
            jsondto.resumen.reteRenta = Math.Round(Math.Abs(invoice.Ret10), 2);
            jsondto.resumen.totalNoGravado = invoice.Positions.Where(w => w.PriceType == "NG").Sum(s => s.NetAmount);
			jsondto.resumen.totalLetras = jsondto.resumen.totalPagar.AmountToLetters();
			jsondto.resumen.totalIva = invoice.TaxAmount;
			jsondto.resumen.condicionOperacion = Convert.ToInt32(invoice.PaymentCondition.Tipo);

			foreach (var payment in invoice.Payments)
			{
				jsondto.resumen.pagos.Add(new FePagosDTO
				{
					codigo = payment.MeanOfPaymentCode,
					montoPago = payment.Amount,
					referencia = payment.Reference,
					plazo = payment.Plazo == string.Empty ? null : payment.Plazo,
					periodo = payment.Periodo == 0 ? null : payment.Periodo,
				});
			}
			#endregion

			#region Extensión
			jsondto.extension.nombEntrega = currentUser.Name;
			jsondto.extension.docuEntrega = currentUser.IDNumber;
			#endregion

			var genNum = dbContext.Database.SqlQuery<long>($"EXECUTE XSP_EBILLING_CO_NEXT_NUMBER {ebillingCom.CompanyId.ToString()},{invoice.InvoiceTypeId.ToString()}").ToList();
			if (genNum.Count == 0)
				throw new CustomTogoException("Error generando rango de número");

			invoice.NumControl = $"DTE-{ebillingDoc.Code}-{branch.Code.PadLeft(4, '0')}{invoice.PointSaleCode.PadLeft(4, '0')}-{genNum[0].ToString().PadLeft(15, '0')}";
			invoice.CodGeneracion = Guid.NewGuid().ToString().ToUpper();
			jsondto.identificacion.codigoGeneracion = invoice.CodGeneracion;
			jsondto.identificacion.numeroControl = invoice.NumControl;

			log = new EBillingLog
			{
				CompanyId = invoice.CompanyId,
				BranchId = invoice.BranchId,
				PointSaleId = invoice.PointSaleId,
				TipoDte = ebillingDoc.Code,
				CodGen = Guid.Parse(invoice.CodGeneracion),
				NumControl = invoice.NumControl,
				InvoiceId = invoice.Id,
				SaleOrderId = invoice.SaleOrderId,
				Request = JsonConvert.SerializeObject(jsondto)
			};

			var result = await PostDteAsyn(jsondto);

			if (result.StatusCode.Substring(0, 2) == "20")
			{
				if (result.StatusCode == "200") 
					result.Message = $"{invoice.InvoiceType.Name}  {invoice.Number} creada correctamente";

				if (result.StatusCode == "201")
				{
					result.Message = $"{invoice.InvoiceType.Name}  {invoice.Number} creada por contingencia";
					invoice.Contingency = true;
					result.InvoiceId = invoice.Id;
					await dbContext.EBillingLog.AddAsync(log);
				}
					

				if (result.StatusCode == "202")
					result.Message = $"{invoice.InvoiceType.Name} {invoice.Number} creada con observaciones";

				if(result.ResponseMH is not null)
				{
					invoice.SelloRecepcion = result.ResponseMH.selloRecibido;
					invoice.FechaRecepcion = Convert.ToDateTime(result.ResponseMH.fhProcesamiento);
				}
				
				invoice.InvoiceType = null;
				invoice.PaymentCondition = null;
				invoice.EBillingDoc = ebillingDoc.Code;
                invoice.SentEmail = true;                
                await dbContext.Invoice.AddAsync(invoice);
				await dbContext.SaveChangesAsync();

				await dbContext.Database.ExecuteSqlAsync($"UPDATE SaleOrder SET Invoiced = 1 WHERE Id={invoice.SaleOrderId.ToString()}");
			}

			return result;
		}

		private async Task<FeResponseDTO> PostDteAsyn(dynamic jsonDto)
		{
			try
			{				
				// Obtener token
				var token = await GetToken();

				if (token.StatusCode == HttpStatusCode.OK) {
					var pathCert = Path.Combine(ebillingCom.IsProd ? ebilling.CertPathProd : ebilling.CertPathTest, $"{ebillingCom.ApiUser}.crt");
					var key = ebillingCom.IsProd ? ebillingCom.PrivateKeyProd : ebillingCom.PrivateKeyTest;
					var strJson = JsonConvert.SerializeObject(jsonDto);
					string dteSigned = await signerService.SignDocument(pathCert, key, strJson);

					if (dteSigned.IsNullOrEmpty())
					{
						return new FeResponseDTO
						{							
							Message = "Firmador: Error intentando firmar el documento"
						};
					}

					// Enviar Documento a MH
					if (!token.Token.IsNullOrEmpty())
						return await SendDocument(jsonDto, token.Token, dteSigned);
				}
				else
				{
					// Crear documento por contingencia
					return new FeResponseDTO { StatusCode = StatusCodes.Status201Created.ToString() };
				}				
				
			}
			catch (CustomTogoException ce)
			{
				return new FeResponseDTO { Message = ce.Message };
			}

			return new FeResponseDTO();
		}

		private async Task<FeResponseDTO> PostCancelAsync(dynamic jsonDto)
		{
			try
			{
				log.Request = JsonConvert.SerializeObject(jsonDto);
				// Obtener token
				var token = await GetToken();

				if(token.StatusCode == HttpStatusCode.OK)
				{
					var pathCert = Path.Combine(ebillingCom.IsProd ? ebilling.CertPathProd : ebilling.CertPathTest, $"{company.Nif1}.crt");
					var key = ebillingCom.IsProd ? ebillingCom.PrivateKeyProd : ebillingCom.PrivateKeyTest;
					var strJson = JsonConvert.SerializeObject(jsonDto);
					string dteSigned = await signerService.SignDocument(pathCert, key, strJson);

					if (dteSigned.IsNullOrEmpty())
					{
						return new FeResponseDTO
						{							
							Message = "Firmador: Error intentando firmar el documento"
						};
					}

					// Enviar Documento a MH
					if (!token.Token.IsNullOrEmpty())
						return await SendCancelDocument(jsonDto, token.Token, dteSigned);
				}
				else
				{
					return new FeResponseDTO
					{						
						Message = "MH: Servicio no disponible"
					};
				}				
			}
			catch (CustomTogoException ce)
			{
				return new FeResponseDTO { Message = ce.Message };
			}

			return new FeResponseDTO();
		}

		private async Task<TokenInfoDTO> GetToken()
		{
			TokenInfoDTO token = new TokenInfoDTO { StatusCode = HttpStatusCode.OK };
			try
			{
				using var client = httpClientFactory.CreateClient();
				client.Timeout = timeOutHttpClient;
				client.BaseAddress = new Uri(ebillingCom.IsProd ? ebilling.UrlProd : ebilling.UrlTest);
				client.DefaultRequestHeaders.Accept.Clear();
				client.DefaultRequestHeaders.Add("User-Agent", "TogoApp");

				var requestContent = new FormUrlEncodedContent(new[]
				{
				new KeyValuePair<string, string>("user", ebillingCom.ApiUser),
				new KeyValuePair<string, string>("pwd", ebillingCom.IsProd ? ebillingCom.ApiKeyProd: ebillingCom.ApiKeyTest)//Clave Api
                });

				HttpResponseMessage response = await client.PostAsync("/seguridad/auth/", requestContent);

				var strReturn = await response.Content.ReadAsStringAsync();

				if (response.IsSuccessStatusCode)
				{
					var jObject = JsonConvert.DeserializeObject<JObject>(strReturn);
					if (jObject["status"].ToString() == "OK")
					{
						token.Token = jObject["body"]["token"].ToString().Split(' ')[1];
					}
					else
					{
						ResponseTokenDTO dtores = JsonConvert.DeserializeObject<ResponseTokenDTO>(strReturn);
						throw new CustomTogoException($"MH {dtores.body.estado}: {dtores.body.descripcionMsg}");
					}
				}
				else
				{
					ResponseTokenDTO dtores = JsonConvert.DeserializeObject<ResponseTokenDTO>(strReturn);
					throw new CustomTogoException($"MH: {dtores.body.descripcionMsg}");
				}
			}
			catch (HttpRequestException e) when (e.InnerException is SocketException { SocketErrorCode: SocketError.HostNotFound })
			{
				if (!ebillingCom.Contingency)
					throw new CustomTogoException("MH: Host no encontrado");

				token.StatusCode = HttpStatusCode.ServiceUnavailable;
			}
			catch (HttpRequestException e) when (e.InnerException is SocketException { SocketErrorCode: SocketError.ConnectionRefused })
			{
				if (!ebillingCom.Contingency)
					throw new CustomTogoException("MH: No se puede conectar con el Host");

				token.StatusCode = HttpStatusCode.ServiceUnavailable;
			}
			catch (HttpRequestException e) when (e.StatusCode.HasValue && (int)e.StatusCode.Value > 500)
			{
				throw new CustomTogoException("MH: Error desconocido");
			}
			return token;
		}

		private async Task<FeResponseDTO> SendCancelDocument(dynamic dto, string token, string dteSigned)
		{
			var responsedto = new FeResponseDTO();
			try
			{
				// Enviando a MH
				using var client = httpClientFactory.CreateClient();
				client.Timeout = timeOutHttpClient;
				client.BaseAddress = new Uri(ebillingCom.IsProd ? ebilling.UrlProd : ebilling.UrlTest);
				client.DefaultRequestHeaders.Accept.Clear();
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
				client.DefaultRequestHeaders.Add("User-Agent", "TogoApp");
				client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

				RequestCancelDte dte = new RequestCancelDte
				{
					ambiente = dto.identificacion.ambiente,
					version = dto.identificacion.version,					
					documento = dteSigned,
				};

				HttpResponseMessage response = await client.PostAsJsonAsync("/fesv/anulardte/", dte);

				var strReturn = await response.Content.ReadAsStringAsync();

				if (response.IsSuccessStatusCode || response.StatusCode == System.Net.HttpStatusCode.BadRequest)
				{
					ResponseDte dtores = JsonConvert.DeserializeObject<ResponseDte>(strReturn);

					var config = new MapperConfiguration(cfg =>
					{
						cfg.CreateMap<ResponseDte, ResponseMHDTO>();
					});
					config.AssertConfigurationIsValid();

					responsedto.ResponseMH = new Mapper(config).Map<ResponseMHDTO>(dtores);
					responsedto.Message = dtores.descripcionMsg;
					responsedto.Observaciones = dtores.observaciones;

					if (response.IsSuccessStatusCode && (dtores.codigoMsg == "001" || dtores.codigoMsg == "002"))
					{
						if (dtores.codigoMsg == "001")
							responsedto.StatusCode = StatusCodes.Status200OK.ToString();

						if (dtores.codigoMsg == "002")
							responsedto.StatusCode = StatusCodes.Status202Accepted.ToString();

						log.StatusCode = responsedto.StatusCode;
						log.SelloRecibido = responsedto.ResponseMH.selloRecibido;

						responsedto.InvoiceId = log.InvoiceId;
					}
					else
					{
						log.StatusCode = StatusCodes.Status400BadRequest.ToString();
						log.InvoiceId = Guid.Empty;
					};

					responsedto.LogId = log.Id;

					log.Response = JsonConvert.SerializeObject(responsedto);
					log.ResponseDate = Convert.ToDateTime(dtores.fhProcesamiento);
					log.ResponseStatus = dtores.estado;
					log.ResponseMessage = dtores.descripcionMsg;
					log.ResponseStatusCode = response.StatusCode.ToString();

					if (dtores.observaciones.Count > 0)
						log.Observaciones = string.Join("|", dtores.observaciones.ToArray());

					await dbContext.EBillingLog.AddAsync(log);
					dbContext.SaveChanges();
				}
				else
					throw new CustomTogoException(response.StatusCode.ToString());
			}
			catch (HttpRequestException e) when (e.InnerException is SocketException { SocketErrorCode: SocketError.HostNotFound })
			{
				throw new CustomTogoException("MH: Host no encontrado");
			}
			catch (HttpRequestException e) when (e.InnerException is SocketException { SocketErrorCode: SocketError.ConnectionRefused })
			{
				throw new CustomTogoException("MH: No se puede conectar con el Host");
			}
			catch (HttpRequestException e) when (e.StatusCode.HasValue && (int)e.StatusCode.Value > 500)
			{
				throw new CustomTogoException("MH: Error desconocido");
			}
			catch (Exception ex)
			{
				throw new CustomTogoException(ex.InnerException is null ? ex.Message : ex.InnerException.Message);
			}
			return responsedto;
		}

		private async Task<FeResponseDTO> SendDocument(dynamic dto, string token, string dteSigned)
		{
			var responsedto = new FeResponseDTO();
			try
			{
				// Enviando a MH
				using var client = httpClientFactory.CreateClient();
				client.Timeout = timeOutHttpClient;
				client.BaseAddress = new Uri(ebillingCom.IsProd ? ebilling.UrlProd : ebilling.UrlTest);
				client.DefaultRequestHeaders.Accept.Clear();
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
				client.DefaultRequestHeaders.Add("User-Agent", "TogoApp");
				client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

				RequestDte dte = new RequestDte()
				{
					ambiente = dto.identificacion.ambiente,
					version = dto.identificacion.version,
					tipoDte = dto.identificacion.tipoDte,
					documento = dteSigned,
					codigoGeneracion = dto.identificacion.codigoGeneracion
				};

				HttpResponseMessage response = await client.PostAsJsonAsync("/fesv/recepciondte/", dte);

				var strReturn = await response.Content.ReadAsStringAsync();

				if (response.IsSuccessStatusCode || response.StatusCode == System.Net.HttpStatusCode.BadRequest)
				{
					ResponseDte dtores = JsonConvert.DeserializeObject<ResponseDte>(strReturn);

					var config = new MapperConfiguration(cfg =>
					{
						cfg.CreateMap<ResponseDte, ResponseMHDTO>();
					});
					config.AssertConfigurationIsValid();

					responsedto.ResponseMH = new Mapper(config).Map<ResponseMHDTO>(dtores);
					responsedto.Message = dtores.descripcionMsg;
					responsedto.Observaciones = dtores.observaciones;

					if (response.IsSuccessStatusCode && (dtores.codigoMsg == "001" || dtores.codigoMsg == "002"))
					{
						if (dtores.codigoMsg == "001")
							responsedto.StatusCode = StatusCodes.Status200OK.ToString();

						if (dtores.codigoMsg == "002")
							responsedto.StatusCode = StatusCodes.Status202Accepted.ToString();

						log.StatusCode = responsedto.StatusCode;
						log.SelloRecibido = responsedto.ResponseMH.selloRecibido;

						responsedto.InvoiceId = log.InvoiceId;
					}
					else
					{
						log.StatusCode = StatusCodes.Status400BadRequest.ToString();
						log.InvoiceId = Guid.Empty;
					};

					responsedto.LogId = log.Id;

					log.Response = JsonConvert.SerializeObject(responsedto);
					log.ResponseDate = Convert.ToDateTime(dtores.fhProcesamiento);
					log.ResponseStatus = dtores.estado;
					log.ResponseMessage = dtores.descripcionMsg;
					log.ResponseStatusCode = response.StatusCode.ToString();

					if (dtores.observaciones.Count > 0)
						log.Observaciones = string.Join("|", dtores.observaciones.ToArray());

					await dbContext.EBillingLog.AddAsync(log);
					await dbContext.SaveChangesAsync();


				}
				else
					throw new CustomTogoException(response.StatusCode.ToString());
			}
			catch (HttpRequestException e) when (e.InnerException is SocketException { SocketErrorCode: SocketError.HostNotFound })
			{
				throw new CustomTogoException("MH: Host no encontrado");
			}
			catch (HttpRequestException e) when (e.InnerException is SocketException { SocketErrorCode: SocketError.ConnectionRefused })
			{
				throw new CustomTogoException("MH: No se puede conectar con el Host");
			}
			catch (HttpRequestException e) when (e.StatusCode.HasValue && (int)e.StatusCode.Value > 500)
			{
				throw new CustomTogoException("MH: Error desconocido");
			}
			catch (Exception ex)
			{
				throw new CustomTogoException(ex.InnerException is null ? ex.Message : ex.InnerException.Message);
			}
			return responsedto;
		}
	}

	internal class TokenInfoDTO
	{
		public string Token { get; set; }
		public HttpStatusCode StatusCode { get; set; }
	}
}
