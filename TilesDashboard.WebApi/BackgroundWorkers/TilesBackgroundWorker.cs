using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TilesDashboard.WebApi.PluginInfrastructure;

namespace TilesDashboard.WebApi.BackgroundWorkers
{
    public class TilesBackgroundWorker : BackgroundService
    {
        private readonly ILogger<TilesBackgroundWorker> _logger;

        private readonly IPluginLoader _pluginLoader;

        public TilesBackgroundWorker(ILogger<TilesBackgroundWorker> logger, IPluginLoader pluginLoader)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _pluginLoader = pluginLoader ?? throw new ArgumentNullException(nameof(pluginLoader));
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Tiles background worker started.");
            var loadedPlugins = _pluginLoader.LoadPlugins(AppDomain.CurrentDomain.BaseDirectory);

            while (!stoppingToken.IsCancellationRequested)
            {
                foreach (var weatherPlugin in loadedPlugins.WeatherPlugins)
                {
                    var data = weatherPlugin.GetData();
                    _logger.LogInformation($"Weather plugin: {weatherPlugin.Name}, temp: {data.Temperature}, hum: {data.Huminidy}%");
                }

                await Task.Delay(10000);
            }
        }
    }
}
