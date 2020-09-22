using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TilesDashboard.Core.Domain.Services;
using TilesDashboard.Handy.Extensions;
using TilesDashboard.PluginBase;
using TilesDashboard.PluginBase.Data;
using TilesDashboard.PluginBase.MetricPlugin;

namespace TilesDashboard.WebApi.BackgroundWorkers
{
    public class HeartBeatPluginHandler
    {
        private readonly ILogger<HeartBeatPluginHandler> _logger;

        private readonly IHeartBeatService _heartBeatService;

        public HeartBeatPluginHandler(ILogger<HeartBeatPluginHandler> logger, IHeartBeatService heartBeatService)
        {
            _logger = logger;
            _heartBeatService = heartBeatService;
        }

        public async Task<Result> HandlePlugin(HeartBeatPluginBase plugin, CancellationToken cancellationToken)
        {
            var data = await plugin.GetDataAsync();
            _logger.LogDebug($"Heartbeat plugin: \"{plugin}\"");
            if (data.Status.Is(Status.OK))
            {
                await _heartBeatService.RecordDataAsync(plugin.TileName, data.Value, data.AppVersion, data.AdditionalInfo, cancellationToken);
            }

            return data;
        }
    }
}
