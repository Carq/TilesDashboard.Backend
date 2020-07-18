using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Threading;
using Microsoft.Extensions.Logging;
using NCrontab;
using TilesDashboard.Core.Domain.Services;
using TilesDashboard.Handy.Extensions;
using TilesDashboard.Handy.Tools;
using TilesDashboard.PluginBase;
using TilesDashboard.PluginBase.MetricPlugin;
using TilesDashboard.WebApi.PluginSystem.Extensions;

namespace TilesDashboard.WebApi.BackgroundWorkers
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "Allowed here")]
    public class MetricPluginHandler
    {
        private readonly ILogger<MetricPluginHandler> _logger;

        private readonly IMetricService _metricService;

        private readonly IDateTimeOffsetProvider _dateTimeProvider;

        public MetricPluginHandler(ILogger<MetricPluginHandler> logger, IMetricService metricService, IDateTimeOffsetProvider dateTimeProvider)
        {
            _logger = logger;
            _metricService = metricService;
            _dateTimeProvider = dateTimeProvider;
        }

        public void Handle(IList<MetricPluginBase> metricPlugins, CancellationToken cancellationToken)
        {
            foreach (var matricPlugin in metricPlugins)
            {
                SchedulePlugin(matricPlugin, cancellationToken);
            }
        }

        private void SchedulePlugin(MetricPluginBase plugin, CancellationToken cancellationToken)
        {
            var schedule = CrontabSchedule.Parse(plugin.CronSchedule, new CrontabSchedule.ParseOptions { IncludingSeconds = true });
            DateTimeOffset nextOccurrence = schedule.GetNextOccurrence(_dateTimeProvider.Now.DateTime);
            _logger.LogDebug($"Metric plugin: \"{plugin.TileName}\" - Next schedule {nextOccurrence}");
            Observable.Timer(nextOccurrence)
                      .Select(x => HandlePlugin(plugin, cancellationToken))
                      .Switch()
                      .Subscribe(
                        plugin => SchedulePlugin(plugin, cancellationToken),
                        exception => _logger.LogError($"Metric plugin: \"{plugin.TileName}\" threw exception. Plugin will be disabled. Error: {exception.Message}", exception));
        }

        private IObservable<MetricPluginBase> HandlePlugin(MetricPluginBase plugin, CancellationToken stoppingToken)
        {
            return Observable.Create<MetricPluginBase>(
                async observer =>
                {
                    try
                    {
                        _logger.LogDebug($"Geting data from plugin: \"{plugin.TileName}\" - \"{plugin.GetType()}\"...");
                        var data = await plugin.GetDataAsync();
                        _logger.LogDebug($"Metric plugin: \"{plugin.TileName}\", Code Coverage: {data.PercentageCodeCoverage}%");
                        if (data.Status.Is(Status.OK))
                        {
                            await _metricService.RecordMetricDataAsync(plugin.TileName, data.MetricType.Convert<Core.Domain.Enums.MetricType>(), data.PercentageCodeCoverage, stoppingToken);
                        }

                        if (data.Status.IsError())
                        {
                            _logger.LogError($"Metric plugin: \"{plugin.TileName}\" return Status \"{data.Status}\" with message: \"{data.ErrorMessage}\". Plugin will be disabled");
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
