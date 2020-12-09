using System;
using System.Diagnostics.CodeAnalysis;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NCrontab;
using TilesDashboard.Handy.Tools;
using TilesDashboard.PluginBase;
using TilesDashboard.PluginBase.Data;
using TilesDashboard.PluginBase.Data.IntegerPlugin;
using TilesDashboard.PluginBase.Data.MetricPlugin;
using TilesDashboard.PluginBase.Data.WeatherPlugin;
using TilesDashboard.PluginBase.MetricPlugin;
using TilesDashboard.V2.Core.Entities.Enums;
using TilesDashboard.WebApi.PluginSystem;
using TilesDashboard.WebApi.PluginSystem.Extensions;

namespace TilesDashboard.WebApi.BackgroundWorkers
{
    public class PluginBackgroundWorker : BackgroundService
    {
        private readonly ILogger<PluginBackgroundWorker> _logger;

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
            MetricPluginHandler metricPluginHandler,
            WeatherPluginHandler weatherPluginHandler,
            IntegerPluginHandler integerPluginHandler,
            HeartBeatPluginHandler heartBeatPluginHandler)
        {
            _pluginLoader = pluginLoader ?? throw new ArgumentNullException(nameof(pluginLoader));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _dateTimeProvider = dateTimeProvider ?? throw new ArgumentNullException(nameof(dateTimeProvider));
            _metricPluginHandler = metricPluginHandler ?? throw new ArgumentNullException(nameof(metricPluginHandler));
            _weatherPluginHandler = weatherPluginHandler ?? throw new ArgumentNullException(nameof(weatherPluginHandler));
            _integerPluginHandler = integerPluginHandler ?? throw new ArgumentNullException(nameof(integerPluginHandler));
            _heartBeatPluginHandler = heartBeatPluginHandler ?? throw new ArgumentNullException(nameof(heartBeatPluginHandler));
        }

        [SuppressMessage("Microsoft.Naming", "CA1725", Justification = "Allowed here")]
        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Tiles background worker started.");
            var loadedPlugins = await _pluginLoader.LoadDataProviderPluginsAsync(AppDomain.CurrentDomain.BaseDirectory);
            foreach (var plugin in loadedPlugins)
            {
                SchedulePlugin(plugin, cancellationToken);
            }

            while (!cancellationToken.IsCancellationRequested)
            {
                await Task.Delay(TimeSpan.FromSeconds(2), cancellationToken);
            }
        }

        private void SchedulePlugin(IDataPlugin plugin, CancellationToken cancellationToken)
        {
            var schedule = CrontabSchedule.Parse(plugin.CronSchedule, new CrontabSchedule.ParseOptions { IncludingSeconds = true });
            DateTimeOffset nextOccurrence = schedule.GetNextOccurrence(_dateTimeProvider.Now.DateTime);
            _logger.LogDebug($"{plugin.TileId.Type} plugin: \"{plugin.TileId.Name}\" - Next schedule {nextOccurrence}");
            Observable.Timer(nextOccurrence)
                      .Select(x => HandlePlugin(plugin, cancellationToken))
                      .Switch()
                      .Subscribe(
                        plugin => SchedulePlugin(plugin, cancellationToken),
                        exception => _logger.LogError($"Error occurs during plugin processing. Plugin will be disabled. Error: {exception.Message}. Inner Exception: {exception.InnerException?.Message}", exception));
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "Allowed here")]
        private IObservable<IDataPlugin> HandlePlugin(IDataPlugin plugin, CancellationToken cancellationToken)
        {
            return Observable.Create<IDataPlugin>(
                async observer =>
                {
                    try
                    {
                        _logger.LogDebug($"Execute GetDataAsync() for plugin: \"{plugin.TileId.Name}\" - \"{plugin.GetType()}\"...");

                        Result result = null;

                        switch (plugin.TileId.Type)
                        {
                            case TileType.Metric:
                                var metricPlugin = (MetricPluginBase)plugin;
                                result = await _metricPluginHandler.HandlePlugin(metricPlugin, cancellationToken);
                                break;
                            case TileType.Weather:
                                var weatherPlugin = (WeatherPluginBase)plugin;
                                result = await _weatherPluginHandler.HandlePlugin(weatherPlugin, cancellationToken);
                                break;
                            case TileType.Integer:
                                var integerPlugin = (IntegerPluginBase)plugin;
                                result = await _integerPluginHandler.HandlePlugin(integerPlugin, cancellationToken);
                                break;
                            case TileType.HeartBeat:
                                var heartBeatPlugin = (HeartBeatPluginBase)plugin;
                                result = await _heartBeatPluginHandler.HandlePlugin(heartBeatPlugin, cancellationToken);
                                break;
                            default:
                                throw new NotSupportedException($"Plugin type {plugin.TileId.Type} is not yet supported");
                        }

                        if (result.Status.IsError())
                        {
                            observer.OnError(new InvalidOperationException($"{plugin.TileId.Type} plugin: \"{plugin.TileId.Name}\" return Status \"{result.Status}\" with message: \"{result.ErrorMessage}\""));
                            return;
                        }

                        if (result.Status == Status.NoUpdate)
                        {
                            _logger.LogDebug($"No data to update for plugin: \"{plugin.TileId.Name}\" - \"{plugin.GetType()}\".");
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
