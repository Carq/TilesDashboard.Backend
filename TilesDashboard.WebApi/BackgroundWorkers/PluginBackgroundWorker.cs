using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TilesDashboard.Core.Domain.Services;
using TilesDashboard.Core.Domain.ValueObjects;
using TilesDashboard.Handy.Extensions;
using TilesDashboard.PluginBase;
using TilesDashboard.WebApi.Configuration;
using TilesDashboard.WebApi.PluginSystem;
using TilesDashboard.WebApi.PluginSystem.Extensions;

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
            var loadedPlugins = await _pluginLoader.LoadPluginsAsync(AppDomain.CurrentDomain.BaseDirectory);

            while (!stoppingToken.IsCancellationRequested)
            {
                foreach (var weatherPlugin in loadedPlugins.WeatherPlugins)
                {
                    try
                    {
                        var data = await weatherPlugin.GetDataAsync();
                        _logger.LogDebug($"Weather plugin: \"{weatherPlugin.TileName}\", Temperature: {data.Temperature}, Huminidy: {data.Huminidy}%");
                        if (data.Status.Is(Status.OK))
                        {
                            await _weatherServices.RecordWeatherDataAsync(weatherPlugin.TileName, new Temperature(data.Temperature), data.Huminidy.HasValue ? new Percentage(data.Huminidy.Value) : null, data.DateOfChange, stoppingToken);
                        }

                        if (data.Status.IsError())
                        {
                            _logger.LogError($"Weather plugin: \"{weatherPlugin.TileName}\" return Status \"{data.Status}\" with message: \"{data.ErrorMessage}\". Plugin will be disabled");
                            loadedPlugins.WeatherPlugins.Remove(weatherPlugin);
                            break;
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"Weather plugin: \"{weatherPlugin.TileName}\" threw exception. Plugin will be disabled. Error: {ex.Message}", ex);
                        loadedPlugins.WeatherPlugins.Remove(weatherPlugin);
                        break;
                    }
                }

                await Task.Delay(_pluginSystemConfig.DataRefreshIntervalInSeconds * 1000);
            }
        }
    }
}
