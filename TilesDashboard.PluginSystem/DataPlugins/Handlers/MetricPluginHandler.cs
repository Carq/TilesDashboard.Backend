using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TilesDashboard.Handy.Extensions;
using TilesDashboard.PluginBase.Data;
using TilesDashboard.PluginBase.Data.MetricPlugin;
using TilesDashboard.PluginSystem.Entities;
using TilesDashboard.V2.Core.Entities;
using TilesDashboard.V2.Core.Services;

namespace TilesDashboard.PluginSystem.DataPlugins.Handlers
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

        public async Task<PluginDataResult> HandlePlugin(MetricDataPlugin plugin, PluginTileConfig pluginConfigForTile, CancellationToken cancellationToken)
        {
            var data = await plugin.GetTileValueAsync(pluginConfigForTile.Configuration, cancellationToken);
            if (data.Status.Is(Status.OK))
            {
                _logger.LogDebug($"Metric plugin: \"{plugin.UniquePluginName}\", Value: {data.Value}, Value Type: {data.MetricType}");
                await _metricService.RecordValue(new StorageId(pluginConfigForTile.TileStorageId), data.MetricType, data.Value);
            }

            return data;
        }
    }
}
