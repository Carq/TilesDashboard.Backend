using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TilesDashboard.Core.Domain.Services;
using TilesDashboard.Handy.Extensions;
using TilesDashboard.PluginBase;
using TilesDashboard.PluginBase.Data;
using TilesDashboard.PluginBase.Data.MetricPlugin;

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
            var data = await plugin.GetDataAsync(cancellationToken);
            _logger.LogDebug($"Metric plugin: \"{plugin.TileName}\", Value: {data.Value}");
            if (data.Status.Is(Status.OK))
            {
                await _metricService.RecordMetricDataAsync(plugin.TileName, data.MetricType, data.Value, cancellationToken);
            }

            return data;
        }
    }
}
