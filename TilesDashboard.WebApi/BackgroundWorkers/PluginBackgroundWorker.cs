using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TilesDashboard.Core.Domain.Services;
using TilesDashboard.Core.Domain.ValueObjects;
using TilesDashboard.WebApi.Configuration;
using TilesDashboard.WebApi.PluginInfrastructure;

namespace TilesDashboard.WebApi.BackgroundWorkers
{
    public class PluginBackgroundWorker : BackgroundService
    {
        private readonly ILogger<PluginBackgroundWorker> _logger;

        private readonly IPluginLoader _pluginLoader;

        private readonly IWeatherServices _weatherServices;

        private readonly IPluginSystemConfig _pluginSystemConfig;

        public PluginBackgroundWorker(IWeatherServices weatherServices, IPluginLoader pluginLoader, IPluginSystemConfig config, ILogger<PluginBackgroundWorker> logger)
        {
            _weatherServices = weatherServices ?? throw new ArgumentNullException(nameof(weatherServices));
            _pluginLoader = pluginLoader ?? throw new ArgumentNullException(nameof(pluginLoader));
            _pluginSystemConfig = config ?? throw new ArgumentNullException(nameof(config));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "Allowed here")]
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Tiles background worker started.");
            var loadedPlugins = _pluginLoader.LoadPlugins(AppDomain.CurrentDomain.BaseDirectory);

            while (!stoppingToken.IsCancellationRequested)
            {
                foreach (var weatherPlugin in loadedPlugins.WeatherPlugins)
                {
                    try
                    {
                        var data = await weatherPlugin.GetDataAsync();
                        _logger.LogDebug($"Weather plugin: \"{weatherPlugin.TileName}\", Temperature: {data.Temperature}, Huminidy: {data.Huminidy}%");
                        await _weatherServices.RecordWeatherDataAsync(weatherPlugin.TileName, new Temperature(data.Temperature), data.Huminidy.HasValue ? new Percentage(data.Huminidy.Value) : null, data.DateOfChange, stoppingToken);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"Weather plugin: \"{weatherPlugin.TileName}\" threw exception. Plugin will be disabled.", ex);
                        loadedPlugins.WeatherPlugins.Remove(weatherPlugin);
                        break;
                    }
                }

                await Task.Delay(_pluginSystemConfig.DataRefreshIntervalInSeconds * 1000);
            }
        }
    }
}
