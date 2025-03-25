
using XDev_UnitWork.Interfaces;

namespace XDev_UnitWork.Services
{
	public class ContingencyService : BackgroundService
	{
		private readonly ILogger<ContingencyService> logger;
		private readonly IFeSvContingencyBL feSv;

		public ContingencyService(ILogger<ContingencyService> logger, IFeSvContingencyBL feSv)
        {
			this.logger = logger;
			this.feSv = feSv;
		}

		public override async Task StopAsync(CancellationToken stoppingToken)
		{
			logger.LogInformation("Contingencia: Proceso detenido.");

			await base.StopAsync(stoppingToken);
		}

		protected override async Task ExecuteAsync(CancellationToken stoppingToken)
		{
			while (!stoppingToken.IsCancellationRequested)
			{
				logger.LogInformation("Contingencia: Proceso iniciado.");
				await feSv.ProcessContingency();
				await Task.Delay(180000, stoppingToken);
			}
		}
	}
}
