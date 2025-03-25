using FluentEmail.Core.Models;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace XDev_UnitWork.Interfaces
{
    public interface IEmailSenderService : IEmailSender
    {
        Task<SendResponse> SendEmailAsync(string email, string subject, string htmlMessage, List<Attachment> attachments, List<Address> listcc = null);
    }
}
