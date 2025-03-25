using XDev_UnitWork.Interfaces;

namespace XDev_UnitWork.Services
{
    public class InvoiceSendEmailService: BackgroundService
    {
        private readonly ILogger<ContingencyService> logger;
        private readonly IInvoiceSendEmailBL sendEmailBL;

        public InvoiceSendEmailService(ILogger<ContingencyService> logger, IInvoiceSendEmailBL sendEmailBL)
        {
            this.logger = logger;
            this.sendEmailBL = sendEmailBL;
        }

        public override async Task StopAsync(CancellationToken stoppingToken)
        {
            logger.LogInformation("Send Email: Proceso detenido.");

            await base.StopAsync(stoppingToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                logger.LogInformation("Send Email: Proceso iniciado.");
                await sendEmailBL.SendEmailAllInvoicesAsync();
                await Task.Delay(180000, stoppingToken);
            }
        }
    }
}
