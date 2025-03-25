using FluentEmail.Core;
using FluentEmail.Core.Models;
using Microsoft.AspNetCore.Identity.UI.Services;
using XDev_UnitWork.Interfaces;

namespace XDev_UnitWork.Services
{
    public class EmailSenderService : IEmailSenderService
    {
        private readonly IFluentEmail fluentEmail;
        private readonly ILogger<EmailSenderService> logger;

        public EmailSenderService(IFluentEmail fluentEmail, ILogger<EmailSenderService> logger)
        {
            this.fluentEmail = fluentEmail;
            this.logger = logger;
        }
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var response = await fluentEmail.To(email)
                             .Subject(subject)                                     
                             .Body(htmlMessage, true)
                             .SendAsync();

            if (!response.Successful)
            {
                foreach (var error in response.ErrorMessages)
                {
                    logger.LogCritical(error);
                }
            }
        }

        public async Task<SendResponse> SendEmailAsync(string email, string subject, string htmlMessage, List<Attachment> attachments, List<Address> listcc = null)
        {
            var response = await fluentEmail.To(email)
                              .CC(listcc)
                              .Subject(subject)
                              .Body(htmlMessage, true)
                              .Attach(attachments)
                              .SendAsync();            

            return response;
        }
    }
}
