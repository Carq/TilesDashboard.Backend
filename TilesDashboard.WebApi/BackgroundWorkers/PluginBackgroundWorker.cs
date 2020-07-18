using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TilesDashboard.WebApi.PluginSystem;

namespace TilesDashboard.WebApi.BackgroundWorkers
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "Allowed here")]
    public class PluginBackgroundWorker : BackgroundService
    {
        private readonly ILogger<PluginBackgroundWorker> _logger;

        private readonly IPluginLoader _pluginLoader;

        private readonly WeatherPluginHandler _weatherPluginHandler;

        private readonly MetricPluginHandler _metricPluginHandler;

        public PluginBackgroundWorker(IPluginLoader pluginLoader, ILogger<PluginBackgroundWorker> logger, WeatherPluginHandler weatherPluginHandler, MetricPluginHandler metricPluginHandler)
        {
            _pluginLoader = pluginLoader ?? throw new ArgumentNullException(nameof(pluginLoader));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _weatherPluginHandler = weatherPluginHandler;
            _metricPluginHandler = metricPluginHandler;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogDebug("Tiles background worker started.");
            var loadedPlugins = await _pluginLoader.LoadPluginsAsync(AppDomain.CurrentDomain.BaseDirectory);

            _weatherPluginHandler.Handle(loadedPlugins.WeatherPlugins, stoppingToken);
            _metricPluginHandler.Handle(loadedPlugins.MetricPlugins, stoppingToken);
        }
    }
}
