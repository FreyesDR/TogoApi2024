using FluentEmail.Core;
using FluentEmail.Smtp;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Razor.Templating.Core;
using SendGrid;
using System.Net;
using System.Net.Mail;
using XDev_AvaLinkAIO;
using XDev_Model;
using XDev_Model.Entities;
using XDev_RazorTemplate.Views;
using XDev_UnitWork.Custom;
using XDev_UnitWork.DTO.ElectronicBilling;
using XDev_UnitWork.Interfaces;

namespace XDev_UnitWork.Business
{
    public class InvoiceSendEmailBL : IInvoiceSendEmailBL
    {
        private readonly ILogger<FeSvContingencyBL> logger;
        private readonly IWebHostEnvironment env;
        private readonly IEmailSenderService emailSender;
        private ApplicationDbContext dbContext;
        private IInvoiceBL invoiceBL;
        private EBillingCompany ebillingCompany;
        private EBilling ebilling;

        public InvoiceSendEmailBL(IServiceScopeFactory scopeFactory,
                                  ILogger<FeSvContingencyBL> logger,
                                  IWebHostEnvironment env,
                                  IEmailSenderService emailSender)
        {
            var scope = scopeFactory.CreateScope();
            dbContext = scope.ServiceProvider.GetService<ApplicationDbContext>();
            invoiceBL = scope.ServiceProvider.GetService<IInvoiceBL>();
            this.logger = logger;
            this.env = env;
            this.emailSender = emailSender;
        }

        public async Task SendEmailAllInvoicesAsync()
        {
            try
            {
                var invoices = await dbContext.Invoice.Include(i => i.InvoiceType)
                                                      .AsNoTracking()
                                                      .Where(f => f.SentEmail == true && f.Canceled == false)
                                                      .Select(s => new
                                                      {
                                                          s.Id,
                                                          s.CompanyId,
                                                          s.Number,
                                                          s.PartnerId,
                                                          s.Sporadic,
                                                          s.CodGeneracion,
                                                          s.NumControl,
                                                          s.SelloRecepcion,
                                                          s.InvoiceType,
                                                          s.Date
                                                      }).ToListAsync();

                if (invoices.Any())
                {
                    var companies = invoices.Select(s => s.CompanyId).Distinct().ToList();

                    foreach (var co in companies)
                    {
                        ebillingCompany = await dbContext.EBillingCompany.AsNoTracking().FirstOrDefaultAsync(f => f.CompanyId == co && f.Active == true);
                        if (ebillingCompany is not null)
                        {
                            ebilling = await dbContext.EBilling.AsNoTracking().FirstOrDefaultAsync(f => f.Id == ebillingCompany.EBillingId);

                            var invList = invoices.Where(w => w.CompanyId == co);

                            foreach (var inv in invList)
                            {
                                if (ebilling.Code == "MHSV")
                                    await SendEmailFeSv(inv);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogCritical($"Invoice Email: {(ex.InnerException is null ? ex.Message : ex.InnerException.Message)}");
            }

        }

        private async Task SendEmailFeSv(dynamic invoice)
        {
            var invid = (Guid)invoice.Id;
            var log = await dbContext.EBillingLog.AsNoTracking().FirstOrDefaultAsync(f => f.InvoiceId == invid);
            if (log is not null)
            {
                var company = await dbContext.Company.AsNoTracking().FirstOrDefaultAsync(f => f.Id == log.CompanyId);
                var pdf = await invoiceBL.CreateFormPDF(invid);

                var to = string.Empty;
                var subject = "Facturación Electrónica";

                EmailDte dte = new EmailDte
                {
                    CodGen = invoice.CodGeneracion,
                    NumControl = invoice.NumControl,
                    SelloRecepcion = invoice.SelloRecepcion,
                    InvoiceType = invoice.InvoiceType.Name,
                    Sociedad = company.TradeName.IsNotNullOrEmpty() ? company.TradeName : company.Name,
                    UrlBtn = $"https://admin.factura.gob.sv/consultaPublica?ambiente={(log.IsProd ? "01" : "00")}&codGen={invoice.CodGeneracion}&fechaEmi={invoice.Date.ToString("yyy-MM-dd")}"
                };

                var imagePath = company.UrlLogo;                                                

                if (imagePath is not null)
                {
                    var imageName = imagePath.Split("/");
                    dte.UrlLogo = imageName[imageName.Length - 1];

                    var pathFile = Path.Combine(env.WebRootPath, "image", dte.UrlLogo);

                    if (File.Exists(pathFile))
                    {
                        dte.Logo = File.ReadAllBytes(pathFile);
                        //var base64 = Convert.ToBase64String(image);
                        //var mimetype = UtilsExtension.GetImageMimeType(image);

                        //dte.UrlLogo = String.Format("data:{0};base64,{1}", mimetype, base64);
                    }
                }

                if (invoice.Sporadic)
                {
                    var sporadic = await dbContext.InvoiceSporadicPartner.AsNoTracking().FirstOrDefaultAsync(f => f.InvoiceId == invid);
                    if (sporadic is not null)
                    {
                        dte.Socio = sporadic.Name;
                        to = sporadic.Email;
                    }
                }
                else
                {
                    Guid paid = (Guid)invoice.PartnerId;
                    var partner = await dbContext.Partner.AsNoTracking().FirstOrDefaultAsync(f => f.Id == paid);
                    if (partner is not null)
                    {
                        dte.Socio = partner.Name;
                        to = partner.ContactPersonEmail;
                    }
                }

                if (to.IsNotNullOrEmpty())
                {
                    var body = await RazorTemplateEngine.RenderAsync("~/Views/EmailDte.cshtml", dte);
                    var json = log.Request.ToStream();

                    List<FluentEmail.Core.Models.Address> ccemails = new List<FluentEmail.Core.Models.Address>();

                    if (ebillingCompany.CcEmail1.IsNotNullOrEmpty() || ebillingCompany.CcEmail2.IsNotNullOrEmpty())
                    {
                        if (ebillingCompany.CcEmail1.IsNotNullOrEmpty())
                            ccemails.Add(new FluentEmail.Core.Models.Address { EmailAddress = ebillingCompany.CcEmail1 });

                        if (ebillingCompany.CcEmail2.IsNotNullOrEmpty())
                            ccemails.Add(new FluentEmail.Core.Models.Address { EmailAddress = ebillingCompany.CcEmail2 });
                    }

                    // Archivos adjuntos
                    var attachList = new List<FluentEmail.Core.Models.Attachment>
                        {
                            new FluentEmail.Core.Models.Attachment {
                                 Data = pdf,
                                 Filename = $"{invoice.CodGeneracion}.pdf",
                                 ContentType = "application/pdf"
                            },
                            new FluentEmail.Core.Models.Attachment {
                                 Data = json,
                                 Filename = $"{invoice.CodGeneracion}.json",
                                 ContentType = "application/json"
                            }
                        };

                    // Adjuntar Logo de la sociedad
                    if (!dte.Logo.IsNullOrEmpty() && dte.Logo.Length > 0)
                    {
                        var mimetype = UtilsExtension.GetImageMimeType(dte.Logo);
                        attachList.Add(new FluentEmail.Core.Models.Attachment
                        {                            
                            Filename = dte.UrlLogo, // Nombre del archivo
                            ContentType = mimetype, // Tipo MIME (ajustar según formato)
                            Data = new MemoryStream(dte.Logo), // Convertir byte[] a Stream
                            IsInline = true, // false para adjunto, true para incrustado
                            ContentId = dte.UrlLogo,
                        });
                    }

                    if (ebillingCompany.SmtpService.IsNullOrEmpty())
                    {
                        // Envío de correo por configuración global
                        var result = await emailSender.SendEmailAsync(to, subject, body, attachList, ccemails);

                        if (result.Successful)
                            await dbContext.Database.ExecuteSqlAsync($"UPDATE Invoice SET SentEmail = {false} WHERE Id={invid.ToString()}");

                        if (!result.Successful)
                            foreach (var msg in result.ErrorMessages)
                            {
                                logger.LogCritical(msg);
                            }
                    }

                    if (ebillingCompany.SmtpService == "1")
                    {
                        // Envío de correo por configuración de Sociedad
                        var sender = new SmtpSender(() => new SmtpClient(ebillingCompany.SmtpHost, ebillingCompany.SmtpPort)
                        {
                            UseDefaultCredentials = false,
                            Credentials = new NetworkCredential()
                            {
                                UserName = ebillingCompany.SmtpUserName,
                                Password = AIO.Decrypt(ebillingCompany.SmtpUserPassword)
                            },
                            EnableSsl = ebillingCompany.SmtpEnableSsl
                        });

                        Email.DefaultSender = sender;
                        var result = await Email.From(ebillingCompany.SmtpUserName, ebillingCompany.FromName)
                                                .To(to)
                                                .CC(ccemails)
                                                .Subject(subject)
                                                .Body(body, true)
                                                .Attach(attachList)
                                                .SendAsync();

                        if (result.Successful)
                            await dbContext.Database.ExecuteSqlAsync($"UPDATE Invoice SET SentEmail = {false} WHERE Id={invid.ToString()}");

                        if (!result.Successful)
                            foreach (var msg in result.ErrorMessages)
                            {
                                logger.LogCritical(msg);
                            }
                    }
                }
            }
        }
    }
}
