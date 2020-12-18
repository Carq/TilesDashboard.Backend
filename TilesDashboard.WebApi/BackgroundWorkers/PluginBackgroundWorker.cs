using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NCrontab;
using TilesDashboard.Handy.Tools;
using TilesDashboard.PluginBase;
using TilesDashboard.PluginBase.Data;
using TilesDashboard.PluginBase.V2;
using TilesDashboard.PluginSystem;
using TilesDashboard.V2.Core.Entities.Enums;
using TilesDashboard.WebApi.PluginSystem;
using TilesDashboard.WebApi.PluginSystem.Extensions;
using IDataPlugin = TilesDashboard.PluginBase.V2.IDataPlugin;

namespace TilesDashboard.WebApi.BackgroundWorkers
{
    public class PluginBackgroundWorker : BackgroundService
    {
        private readonly ILogger<PluginBackgroundWorker> _logger;

        private readonly IPluginConfigRepository _pluginConfigRepository;

        private readonly IDataPluginLoader _pluginLoader;

        private readonly IDateTimeProvider _dateTimeProvider;

        private readonly MetricPluginHandler _metricPluginHandler;

        private readonly WeatherPluginHandler _weatherPluginHandler;

        private readonly IntegerPluginHandler _integerPluginHandler;

        private readonly HeartBeatPluginHandler _heartBeatPluginHandler;

        public PluginBackgroundWorker(
            IDataPluginLoader pluginLoader,
            ILogger<PluginBackgroundWorker> logger,
            IDateTimeProvider dateTimeProvider,
            IPluginConfigRepository pluginConfigRepository,
            MetricPluginHandler metricPluginHandler,
            WeatherPluginHandler weatherPluginHandler,
            IntegerPluginHandler integerPluginHandler,
            HeartBeatPluginHandler heartBeatPluginHandler)
        {
            _pluginLoader = pluginLoader ?? throw new ArgumentNullException(nameof(pluginLoader));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _dateTimeProvider = dateTimeProvider ?? throw new ArgumentNullException(nameof(dateTimeProvider));
            _pluginConfigRepository = pluginConfigRepository ?? throw new ArgumentNullException(nameof(pluginConfigRepository));
            _metricPluginHandler = metricPluginHandler ?? throw new ArgumentNullException(nameof(metricPluginHandler));
            _weatherPluginHandler = weatherPluginHandler ?? throw new ArgumentNullException(nameof(weatherPluginHandler));
            _integerPluginHandler = integerPluginHandler ?? throw new ArgumentNullException(nameof(integerPluginHandler));
            _heartBeatPluginHandler = heartBeatPluginHandler ?? throw new ArgumentNullException(nameof(heartBeatPluginHandler));
        }

        [SuppressMessage("Microsoft.Naming", "CA1725", Justification = "Allowed here")]
        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Tiles background worker started.");
            var loadedPlugins = await _pluginLoader.LoadDataPluginsAsync(AppDomain.CurrentDomain.BaseDirectory);
            var pluginsConfigurationPerTile = await _pluginConfigRepository.GetAllPluginsConfiguration(cancellationToken);

            foreach (var plugin in loadedPlugins)
            {
                SchedulePlugin(plugin, pluginsConfigurationPerTile.Where(x => x.PluginName == plugin.UniquePluginName).ToList(), cancellationToken);
            }

            while (!cancellationToken.IsCancellationRequested)
            {
                await Task.Delay(TimeSpan.FromSeconds(2), cancellationToken);
            }
        }

        private void SchedulePlugin(IDataPlugin plugin, IList<PluginConfigForTile> pluginConfigurationForTiles, CancellationToken cancellationToken)
        {
            foreach (var pluginConfigurationForTile in pluginConfigurationForTiles)
            {
                SchedulePlugin(plugin, pluginConfigurationForTile, cancellationToken);
            }
        }

        private void SchedulePlugin(IDataPlugin plugin, PluginConfigForTile pluginConfigurationForTile, CancellationToken cancellationToken)
        {
            var schedule = CrontabSchedule.Parse(pluginConfigurationForTile.CronSchedule, new CrontabSchedule.ParseOptions { IncludingSeconds = true });
            DateTimeOffset nextOccurrence = schedule.GetNextOccurrence(_dateTimeProvider.Now.DateTime);
            _logger.LogDebug($"Plugin: \"{plugin.UniquePluginName}\" for Tile Id {pluginConfigurationForTile.TileStorageId} with Type {plugin.TileType} - Next schedule {nextOccurrence}");
            Observable.Timer(nextOccurrence)
                  .Select(x => HandlePlugin(plugin, pluginConfigurationForTile, cancellationToken))
                  .Switch()
                  .Subscribe(
                    plugin => SchedulePlugin(plugin, pluginConfigurationForTile, cancellationToken),
                    exception => _logger.LogError($"Error occurs during plugin processing. Plugin will be disabled. Error: {exception.Message}. Inner Exception: {exception.InnerException?.Message}", exception));
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "Allowed here")]
        private IObservable<IDataPlugin> HandlePlugin(IDataPlugin plugin, PluginConfigForTile pluginConfigurationForTile, CancellationToken cancellationToken)
        {
            return Observable.Create<IDataPlugin>(
                async observer =>
                {
                    try
                    {
                        _logger.LogDebug($"Execute GetTileValueAsync method for plugin: \"{plugin.UniquePluginName}\" - \"{plugin.TileType}\"...");

                        Result result = null;

                        switch (plugin.TileType)
                        {
                            case TileType.Weather:
                                var weatherPlugin = (WeatherPluginBase)plugin;
                                result = await _weatherPluginHandler.HandlePlugin(weatherPlugin, pluginConfigurationForTile, cancellationToken);
                                break;
                            default:
                                throw new NotSupportedException($"Plugin type {plugin.TileType} is not yet supported");
                        }

                        if (result.Status.IsError())
                        {
                            observer.OnError(new InvalidOperationException($"Plugin {plugin.UniquePluginName} for tile with Id: \"{pluginConfigurationForTile.TileStorageId}\" return Status \"{result.Status}\" with message: \"{result.ErrorMessage}\""));
                            return;
                        }

                        if (result.Status == Status.NoUpdate)
                        {
                            _logger.LogDebug($"No data to update for plugin \"{plugin.UniquePluginName}\" and Tile with Id \"{pluginConfigurationForTile.TileStorageId}\".");
                        }

                        observer.OnNext(plugin);
                    }
                    catch (Exception exception)
                    {
                        observer.OnError(exception);
                    }
                });
        }
    }
}
