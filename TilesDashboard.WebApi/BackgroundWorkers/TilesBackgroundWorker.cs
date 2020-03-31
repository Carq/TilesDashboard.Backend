using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace TilesDashboard.WebApi.BackgroundWorkers
{
    public class TilesBackgroundWorker : BackgroundService
    {
        private readonly ILogger<TilesBackgroundWorker> _logger;

        public TilesBackgroundWorker(ILogger<TilesBackgroundWorker> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Tiles background worker is working :)");
                await Task.Delay(10000);
            }
        }
    }
}
