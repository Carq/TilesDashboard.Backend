using System;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NCrontab;
using TilesDashboard.Core.Type;
using TilesDashboard.Handy.Tools;
using TilesDashboard.PluginBase;
using TilesDashboard.PluginBase.MetricPlugin;
using TilesDashboard.PluginBase.WeatherPlugin;
using TilesDashboard.WebApi.PluginSystem;
using TilesDashboard.WebApi.PluginSystem.Extensions;

namespace TilesDashboard.WebApi.BackgroundWorkers
{
    public class PluginBackgroundWorker : BackgroundService
    {
        private readonly ILogger<PluginBackgroundWorker> _logger;

        private readonly IPluginLoader _pluginLoader;

        private readonly IDateTimeOffsetProvider _dateTimeProvider;

        private readonly MetricPluginHandler _metricPluginHandler;

        private readonly WeatherPluginHandler _weatherPluginHandler;

        private readonly IntegerPluginHandler _integerPluginHandler;

        public PluginBackgroundWorker(
            IPluginLoader pluginLoader,
            ILogger<PluginBackgroundWorker> logger,
            IDateTimeOffsetProvider dateTimeProvider,
            MetricPluginHandler metricPluginHandler,
            WeatherPluginHandler weatherPluginHandler,
            IntegerPluginHandler integerPluginHandler)
        {
            _pluginLoader = pluginLoader ?? throw new ArgumentNullException(nameof(pluginLoader));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _dateTimeProvider = dateTimeProvider ?? throw new ArgumentNullException(nameof(dateTimeProvider));
            _metricPluginHandler = metricPluginHandler;
            _weatherPluginHandler = weatherPluginHandler;
            _integerPluginHandler = integerPluginHandler;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Tiles background worker started.");
            var loadedPlugins = await _pluginLoader.LoadPluginsAsync(AppDomain.CurrentDomain.BaseDirectory);
            foreach (var plugin in loadedPlugins)
            {
                SchedulePlugin(plugin, stoppingToken);
            }

            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(TimeSpan.FromSeconds(2));
            }
        }

        private void SchedulePlugin(IPlugin plugin, CancellationToken cancellationToken)
        {
            var schedule = CrontabSchedule.Parse(plugin.CronSchedule, new CrontabSchedule.ParseOptions { IncludingSeconds = true });
            DateTimeOffset nextOccurrence = schedule.GetNextOccurrence(_dateTimeProvider.Now.DateTime);
            _logger.LogDebug($"{plugin.TileType} plugin: \"{plugin.TileName}\" - Next schedule {nextOccurrence}");
            Observable.Timer(nextOccurrence)
                      .Select(x => HandlePlugin(plugin, cancellationToken))
                      .Switch()
                      .Subscribe(
                        plugin => SchedulePlugin(plugin, cancellationToken),
                        exception => _logger.LogError($"Error occurs during plugin processing. Plugin will be disabled. Error: {exception.Message}", exception));
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "Allowed here")]
        private IObservable<IPlugin> HandlePlugin(IPlugin plugin, CancellationToken cancellationToken)
        {
            return Observable.Create<IPlugin>(
                async observer =>
                {
                    try
                    {
                        _logger.LogDebug($"Execute GetDataAsync() for plugin: \"{plugin.TileName}\" - \"{plugin.GetType()}\"...");

                        Result result = null;

                        switch (plugin.TileType)
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
                            default:
                                throw new NotSupportedException($"Plugin type {plugin.TileType} is not yet supported");
                        }

                        if (result.Status.IsError())
                        {
                            observer.OnError(new InvalidOperationException($"{plugin.TileType} plugin: \"{plugin.TileName}\" return Status \"{result.Status}\" with message: \"{result.ErrorMessage}\""));
                            return;
                        }

                        if (result.Status == Status.NoUpdate)
                        {
                            _logger.LogDebug($"No data to update for plugin: \"{plugin.TileName}\" - \"{plugin.GetType()}\".");
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
