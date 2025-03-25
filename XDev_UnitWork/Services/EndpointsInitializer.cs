
using Microsoft.AspNetCore.Routing;
using System;
using XDev_Model;
using XDev_UnitWork.Custom;

namespace XDev_UnitWork.Services
{
    public class EndpointsInitializer : IHostedLifecycleService
    {
        private readonly IServiceProvider _serviceProvider;

        public EndpointsInitializer(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }


        public Task StartAsync(CancellationToken cancellationToken) => Task.CompletedTask;

        public async Task StartedAsync(CancellationToken cancellationToken)
        {
            using var scope = _serviceProvider.CreateScope();
            var endpointDataSource = scope.ServiceProvider.GetRequiredService<EndpointDataSource>();
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            await UtilsExtension.SynchronizeEndPoint(endpointDataSource.Endpoints, dbContext);
        }

        public Task StartingAsync(CancellationToken cancellationToken) => Task.CompletedTask;

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;

        public Task StoppedAsync(CancellationToken cancellationToken) => Task.CompletedTask;

        public Task StoppingAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
