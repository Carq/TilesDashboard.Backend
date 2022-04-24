using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TilesDashboard.Handy.Extensions;
using TilesDashboard.PluginBase.Data;
using TilesDashboard.PluginBase.Data.DualPlugin;
using TilesDashboard.PluginSystem.Entities;
using TilesDashboard.V2.Core.Entities;
using TilesDashboard.V2.Core.Services;

namespace TilesDashboard.PluginSystem.DataPlugins.Handlers
{
    public class DualPluginHandler
    {
        private readonly ILogger<DualPluginHandler> _logger;

        private readonly IDualService _dualService;

        public DualPluginHandler(ILogger<DualPluginHandler> logger, IDualService dualService)
        {
            _logger = logger;
            _dualService = dualService;
        }

        public async Task<PluginDataResult> HandlePlugin(DualPluginBase plugin, PluginTileConfig pluginConfigForTile, CancellationToken cancellationToken)
        {
            var data = await plugin.GetTileValueAsync(pluginConfigForTile.Configuration, cancellationToken);
            _logger.LogDebug($"Dual plugin: \"{plugin.UniquePluginName}\", Value: {data}");
            if (data.Status.Is(Status.OK))
            {
                await _dualService.RecordValue(new StorageId(pluginConfigForTile.TileStorageId), data.Primary, data.Secondary, null);
            }

            return data;
        }
    }
}
