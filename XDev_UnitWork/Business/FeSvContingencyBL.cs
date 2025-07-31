using AutoMapper;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Sockets;
using System.Text;
using System.Xml.Serialization;
using XDev_Model;
using XDev_Model.Entities;
using XDev_UnitWork.Custom;
using XDev_UnitWork.DTO;
using XDev_UnitWork.DTO.Company;
using XDev_UnitWork.DTO.Dte;
using XDev_UnitWork.DTO.FeSv;
using XDev_UnitWork.Interfaces;
using XDev_UnitWork.Services;

namespace XDev_UnitWork.Business
{
	public class FeSvContingencyBL : IFeSvContingencyBL
	{
		private readonly ILogger<FeSvContingencyBL> logger;
		private readonly ApplicationDbContext dbContext;
		private readonly IHttpClientFactory httpClientFactory;

		internal EBilling ebilling;
		private EBillingCompany ebillingCom;
		private TimeSpan timeOutHttpClient = TimeSpan.FromMinutes(3);
		private EBillingLog log;
		private ISignerService signerService;
        private readonly IDataProtector _protectorCert;
        private readonly IDataProtector _protectorCred;

        public FeSvContingencyBL(ILogger<FeSvContingencyBL> logger, IServiceScopeFactory scopeFactory, IDataProtectionProvider dataProtectionProvider)
		{
			this.logger = logger;
			var scope = scopeFactory.CreateScope();
			dbContext = scope.ServiceProvider.GetService<ApplicationDbContext>();
			httpClientFactory = scope.ServiceProvider.GetService<IHttpClientFactory>();
			signerService = new SignerService();
            _protectorCert = dataProtectionProvider.CreateProtector("cert-protector");
            _protectorCred = dataProtectionProvider.CreateProtector("cred-protector");
        }

		public async Task ProcessContingency()
		{
			var list = await (from inv in dbContext.Invoice.AsNoTracking()
							  where inv.Contingency == true && inv.SelloRecepcion == null
							  select new
							  {
								  inv.Id,
								  inv.CompanyId
							  }).ToListAsync();

			if (list.Any())
			{
				logger.LogInformation($"Contingencia: Procesando {list.Count} contingencias");

				// La contingencia debe ser por sociedad
				var companies = list.Select(s => s.CompanyId).Distinct().ToList(); //list.DistinctBy(k => k.CompanyId).ToList();

				foreach (var co in companies)
				{


					ebilling = await dbContext.EBilling.AsNoTracking().FirstOrDefaultAsync(f => f.Code == "MHSV");
					ebillingCom = await dbContext.EBillingCompany.AsNoTracking().FirstOrDefaultAsync(f => f.EBillingId == ebilling.Id &&
																										 f.CompanyId == co);

					if (ebillingCom is null)
					{
						logger.LogWarning("Sociedad no configurada para Facturación Electrónica");
					}
					else
					{
						var invoices = list.Where(w => w.CompanyId == co).ToList();

						if (invoices.Any())
						{
							foreach (var invoice in invoices)
							{
								await SendContingencyAsync(invoice.Id);
							}
						}


					}
				}
			}
			else logger.LogInformation("Contingencia: No existen contingencias que procesar");
		}

		private async Task SendContingencyAsync(Guid invoiceid)
		{
			try
			{
				dynamic dte = null;
				log = await dbContext.EBillingLog.AsNoTracking().FirstOrDefaultAsync(f => f.InvoiceId == invoiceid);

				if (log is not null)
				{
					if (log.TipoDte == "01")
						dte = JsonConvert.DeserializeObject<JsonFeFcV1>(log.Request);

					if (log.TipoDte == "03")
                        dte = JsonConvert.DeserializeObject<JsonFeCcfV3>(log.Request);                    

                    if (log.TipoDte == "05")
                        dte = JsonConvert.DeserializeObject<JsonNcV3>(log.Request);

                    if (log.TipoDte == "06")
                        dte = JsonConvert.DeserializeObject<JsonNdV3>(log.Request);

                    if (log.TipoDte == "11")
                        dte = JsonConvert.DeserializeObject<JsonFexV1>(log.Request);

                    if (dte is not null)
					{
						logger.LogInformation($"Procesando Factura {invoiceid.ToString()}");
						var jsondto = new JsonContingencyV3();

						#region Identificación                
						jsondto.identificacion.ambiente = ebillingCom.IsProd ? "01" : "00";
						jsondto.identificacion.codigoGeneracion = Guid.NewGuid().ToString().ToUpper();
						jsondto.identificacion.fTransmision = DateTime.Now.ToString("yyyy-MM-dd");
						jsondto.identificacion.hTransmision = DateTime.Now.ToString("HH:mm:ss");
						#endregion

						#region Emisor
						jsondto.emisor.nit = dte.emisor.nit;
						jsondto.emisor.nombre = dte.emisor.nombre;
						jsondto.emisor.nombreResponsable = "Administrador de Sistema"; //dte.extension.nombEntrega;
						jsondto.emisor.tipoDocResponsable = "37"; // cambiar después
						jsondto.emisor.numeroDocResponsable = "1"; //dte.extension.docuEntrega;
						jsondto.emisor.tipoEstablecimiento = dte.emisor.tipoEstablecimiento;
						//jsondto.emisor.codPuntoVenta = dte.emisor.codPuntoVenta; // Se comento porque la NC y ND no tienen este campo
						jsondto.emisor.telefono = dte.emisor.telefono;
						jsondto.emisor.correo = dte.emisor.correo;
						#endregion

						#region Detalle
						jsondto.detalleDTE.Add(new JsonDetailContingencyV3
						{
							codigoGeneracion = dte.identificacion.codigoGeneracion,
							tipoDoc = dte.identificacion.tipoDte,
						});
						#endregion

						#region Motivo
						jsondto.motivo.fInicio = log.DateTime.ToString("yyyy-MM-dd");
						jsondto.motivo.fFin = DateTime.Now.ToString("yyyy-MM-dd");
						jsondto.motivo.hInicio = log.DateTime.ToString("HH:mm:ss");
						jsondto.motivo.hFin = DateTime.Now.ToString("HH:mm:ss");
						jsondto.motivo.tipoContingencia = 3;
						#endregion

						// Firmar json
						//var pathCert = Path.Combine(ebillingCom.IsProd ? ebilling.CertPathProd : ebilling.CertPathTest, $"{ebillingCom.ApiUser}.crt");
						//var key = ebillingCom.IsProd ? ebillingCom.PrivateKeyProd : ebillingCom.PrivateKeyTest;
						var strJson = JsonConvert.SerializeObject(jsondto);
                        var certificado = await ReadCertificado();
                        var key = _protectorCred.Unprotect(ebillingCom.IsProd ? ebillingCom.PrivateKeyProd : ebillingCom.PrivateKeyTest);
                        string dteSigned = await signerService.SignDocument(certificado, key, strJson);

						// Obtener token
						var token = await GetToken();

						if (token.StatusCode == HttpStatusCode.OK)
						{
							// Enviando a MH
							using var client = httpClientFactory.CreateClient();
							client.Timeout = timeOutHttpClient;
							client.BaseAddress = new Uri(ebillingCom.IsProd ? ebilling.UrlProd : ebilling.UrlTest);
							client.DefaultRequestHeaders.Accept.Clear();
							client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
							client.DefaultRequestHeaders.Add("User-Agent", "TogoApp");
							client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Token);

							var dteS = new
							{
								nit = ebillingCom.ApiUser,
								documento = dteSigned
							};

							HttpResponseMessage response = await client.PostAsJsonAsync("/fesv/contingencia/", dteS);

							var strReturn = await response.Content.ReadAsStringAsync();

							if (response.IsSuccessStatusCode)
							{
								var res = JsonConvert.DeserializeObject<ResponseContingenciaDte>(strReturn);

								logger.LogInformation(res.estado);
								if (res.estado == "RECIBIDO")
								{
									// Enviar DTE
									logger.LogInformation("Contingencia: Enviada correctamente");

									// Actualizar DTE
									dte.identificacion.tipoModelo = 2;
									dte.identificacion.tipoOperacion = 2;
									dte.identificacion.tipoContingencia = 3;
									dte.identificacion.motivoContin = "Falla en el suministro de Internet";

									// Firmar DTE
									strJson = JsonConvert.SerializeObject(dte);
									dteSigned = await signerService.SignDocument(certificado, key, strJson);
									
									// Enviando a MH									
									RequestDte req = new RequestDte()
									{
										ambiente = dte.identificacion.ambiente,
										version = dte.identificacion.version,
										tipoDte = dte.identificacion.tipoDte,
										documento = dteSigned,
										codigoGeneracion = dte.identificacion.codigoGeneracion
									};

									response = await client.PostAsJsonAsync("/fesv/recepciondte/", req);

									strReturn = await response.Content.ReadAsStringAsync();

									if (response.IsSuccessStatusCode || response.StatusCode == System.Net.HttpStatusCode.BadRequest)
									{
										ResponseDte dtores = JsonConvert.DeserializeObject<ResponseDte>(strReturn);

										//var config = new MapperConfiguration(cfg =>
										//{
										//	cfg.CreateMap<ResponseDte, ResponseMHDTO>();
										//});
										//config.AssertConfigurationIsValid();										

										if (response.IsSuccessStatusCode && (dtores.codigoMsg == "001" || dtores.codigoMsg == "002"))
										{
											logger.LogInformation("Contingencia: DTE Envíado correctamente");
											if (dtores.codigoMsg == "001")
												log.StatusCode = StatusCodes.Status200OK.ToString();

											if (dtores.codigoMsg == "002")
												log.StatusCode = StatusCodes.Status202Accepted.ToString();
											
											log.SelloRecibido = dtores.selloRecibido;
											log.Request = JsonConvert.SerializeObject(dte);
											log.Response = JsonConvert.SerializeObject(dtores);
											log.ResponseDate = Convert.ToDateTime(dtores.fhProcesamiento);
											log.ResponseStatus = dtores.estado;
											log.ResponseMessage = dtores.descripcionMsg;
											log.ResponseStatusCode = response.StatusCode.ToString();

											if (dtores.observaciones.Count > 0)
												log.Observaciones = string.Join("|", dtores.observaciones.ToArray());

											dbContext.EBillingLog.Update(log);
											await dbContext.SaveChangesAsync();

											await dbContext.Database.ExecuteSqlAsync($"UPDATE Invoice SET SelloRecepcion = {log.SelloRecibido} WHERE Id={invoiceid.ToString()}");
										}																				
									}
								}
								else
								{
                                    foreach (var obs in res.observaciones)
                                    {
                                        logger.LogInformation($"Contingencia: {obs}");
                                    }
                                }
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				logger.LogError($"Contingencia: {(ex.InnerException is null ? ex.Message : ex.InnerException.Message)}");
			}
		}

		private async Task<TokenInfoDTO> GetToken()
		{
			TokenInfoDTO token = new TokenInfoDTO { StatusCode = HttpStatusCode.BadRequest };
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
				new KeyValuePair<string, string>("pwd", ebillingCom.IsProd ? _protectorCred.Unprotect(ebillingCom.ApiKeyProd): _protectorCred.Unprotect(ebillingCom.ApiKeyTest))//Clave Api
                });

				HttpResponseMessage response = await client.PostAsync("/seguridad/auth/", requestContent);

				var strReturn = await response.Content.ReadAsStringAsync();

				if (response.IsSuccessStatusCode)
				{
					var jObject = JsonConvert.DeserializeObject<JObject>(strReturn);
					if (jObject["status"].ToString() == "OK")
					{
						token.Token = jObject["body"]["token"].ToString().Split(' ')[1];
						token.StatusCode = HttpStatusCode.OK;
					}
					else
					{
						ResponseTokenDTO dtores = JsonConvert.DeserializeObject<ResponseTokenDTO>(strReturn);
						logger.LogError($"MH {dtores.body.estado}: {dtores.body.descripcionMsg}");
					}
				}
				else
				{
					ResponseTokenDTO dtores = JsonConvert.DeserializeObject<ResponseTokenDTO>(strReturn);
					logger.LogError($"MH: {dtores.body.descripcionMsg}");
				}
			}
			catch (HttpRequestException e) when (e.InnerException is SocketException { SocketErrorCode: SocketError.HostNotFound })
			{
				logger.LogError("MH: Host no encontrado");
			}
			catch (HttpRequestException e) when (e.InnerException is SocketException { SocketErrorCode: SocketError.ConnectionRefused })
			{
				logger.LogError("MH: No se puede conectar con el Host");
			}
			catch (HttpRequestException e) when (e.StatusCode.HasValue && (int)e.StatusCode.Value > 500)
			{
				logger.LogError("MH: Error desconocido");
			}
			return token;
		}

        private async Task<CertificadoMH> ReadCertificado()
        {
            return await Task.Run(() =>
            {
                var certificado = ebillingCom.IsProd ? ebillingCom.CertPrd : ebillingCom.CertTest;
                var xml = _protectorCert.Unprotect(Encoding.UTF8.GetString(certificado));
                var serializer = new XmlSerializer(typeof(CertificadoMH));

                using var reader = new StringReader(xml);
                return (CertificadoMH)serializer.Deserialize(reader);
            });
        }
    }

	internal class ResponseContingenciaDte
	{
		public string estado { get; set; }
		public string fechaHora { get; set; }
		public string selloRecibido { get; set; }
		public string mensaje { get; set; }
		public List<string> observaciones { get; set; }
	}
}
