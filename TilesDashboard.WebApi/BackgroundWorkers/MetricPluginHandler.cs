using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TilesDashboard.Core.Domain.Services;
using TilesDashboard.Handy.Extensions;
using TilesDashboard.PluginBase;
using TilesDashboard.PluginBase.MetricPlugin;

namespace TilesDashboard.WebApi.BackgroundWorkers
{
    public class MetricPluginHandler
    {
        private readonly ILogger<MetricPluginHandler> _logger;

        private readonly IMetricService _metricService;

        public MetricPluginHandler(ILogger<MetricPluginHandler> logger, IMetricService metricService)
        {
            _logger = logger;
            _metricService = metricService;
        }

        public async Task<Result> HandlePlugin(MetricPluginBase plugin, CancellationToken cancellationToken)
        {
            var data = await plugin.GetDataAsync();
            _logger.LogDebug($"Metric plugin: \"{plugin.TileName}\", Code Coverage: {data.PercentageCodeCoverage}%");
            if (data.Status.Is(Status.OK))
            {
                await _metricService.RecordMetricDataAsync(plugin.TileName, data.MetricType, data.PercentageCodeCoverage, cancellationToken);
            }

            return data;
        }
    }
}
