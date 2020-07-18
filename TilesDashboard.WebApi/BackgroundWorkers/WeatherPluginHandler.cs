using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Threading;
using Microsoft.Extensions.Logging;
using NCrontab;
using TilesDashboard.Core.Domain.Services;
using TilesDashboard.Core.Domain.ValueObjects;
using TilesDashboard.Handy.Extensions;
using TilesDashboard.Handy.Tools;
using TilesDashboard.PluginBase;
using TilesDashboard.PluginBase.WeatherPlugin;
using TilesDashboard.WebApi.PluginSystem.Extensions;

namespace TilesDashboard.WebApi.BackgroundWorkers
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "Allowed here")]
    public class WeatherPluginHandler
    {
        private readonly ILogger<WeatherPluginHandler> _logger;

        private readonly IWeatherServices _weatherServices;

        private readonly IDateTimeOffsetProvider _dateTimeProvider;

        public WeatherPluginHandler(ILogger<WeatherPluginHandler> logger, IWeatherServices weatherServices, IDateTimeOffsetProvider dateTimeProvider)
        {
            _logger = logger;
            _weatherServices = weatherServices;
            _dateTimeProvider = dateTimeProvider;
        }

        public void Handle(IList<WeatherPluginBase> weatherPlugins, CancellationToken cancellationToken)
        {
            foreach (var weatherPlugin in weatherPlugins)
            {
                ScheduleWeatherPlugin(weatherPlugin, cancellationToken);
            }
        }

        private void ScheduleWeatherPlugin(WeatherPluginBase weatherPlugin, CancellationToken cancellationToken)
        {
            var schedule = CrontabSchedule.Parse(weatherPlugin.CronSchedule, new CrontabSchedule.ParseOptions { IncludingSeconds = true });
            DateTimeOffset nextOccurrence = schedule.GetNextOccurrence(_dateTimeProvider.Now.DateTime);
            _logger.LogDebug($"Weather plugin: \"{weatherPlugin.TileName}\" - Next schedule {nextOccurrence}");
            Observable.Timer(nextOccurrence)
                      .Select(x => HandleWeatherPlugin(weatherPlugin, cancellationToken))
                      .Switch()
                      .Subscribe(
                        plugin => ScheduleWeatherPlugin(plugin, cancellationToken),
                        exception => _logger.LogError($"Weather plugin: \"{weatherPlugin.TileName}\" threw exception. Plugin will be disabled. Error: {exception.Message}", exception));
        }

        private IObservable<WeatherPluginBase> HandleWeatherPlugin(WeatherPluginBase weatherPlugin, CancellationToken stoppingToken)
        {
            return Observable.Create<WeatherPluginBase>(
                async observer =>
                {
                    try
                    {
                        _logger.LogDebug($"Geting data from plugin: \"{weatherPlugin.TileName}\" - \"{weatherPlugin.GetType()}\"...");
                        var data = await weatherPlugin.GetDataAsync();
                        _logger.LogDebug($"Weather plugin: \"{weatherPlugin.TileName}\", Temperature: {data.Temperature}, Huminidy: {data.Huminidy}%");
                        if (data.Status.Is(Status.OK))
                        {
                            await _weatherServices.RecordWeatherDataAsync(weatherPlugin.TileName, new Temperature(data.Temperature), data.Huminidy.HasValue ? new Percentage(data.Huminidy.Value) : null, data.DateOfChange, stoppingToken);
                        }

                        if (data.Status.IsError())
                        {
                            _logger.LogError($"Weather plugin: \"{weatherPlugin.TileName}\" return Status \"{data.Status}\" with message: \"{data.ErrorMessage}\". Plugin will be disabled");
                        }

                        observer.OnNext(weatherPlugin);
                    }
                    catch (Exception exception)
                    {
                        observer.OnError(exception);
                    }
                });
        }
    }
}
