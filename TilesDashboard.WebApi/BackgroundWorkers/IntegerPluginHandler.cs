using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TilesDashboard.Handy.Extensions;
using TilesDashboard.PluginBase;
using TilesDashboard.PluginBase.Data;
using TilesDashboard.PluginBase.Data.IntegerPlugin;
using TilesDashboard.PluginSystem.Entities;
using TilesDashboard.V2.Core.Entities;
using TilesDashboard.V2.Core.Services;

namespace TilesDashboard.WebApi.BackgroundWorkers
{
    public class IntegerPluginHandler
    {
        private readonly ILogger<IntegerPluginHandler> _logger;

        private readonly IIntegerService _integerService;

        public IntegerPluginHandler(ILogger<IntegerPluginHandler> logger, IIntegerService integerService)
        {
            _logger = logger;
            _integerService = integerService;
        }

        public async Task<PluginDataResult> HandlePlugin(IntegerPluginBase plugin, PluginTileConfig pluginConfigForTile, CancellationToken cancellationToken)
        {
            var data = await plugin.GetTileValueAsync(pluginConfigForTile.Configuration, cancellationToken);
            _logger.LogDebug($"Integer plugin: \"{plugin.UniquePluginName}\", Value: {data.Value}");
            if (data.Status.Is(Status.OK))
            {
                await _integerService.RecordValue(new StorageId(pluginConfigForTile.TileStorageId), data.Value);
            }

            return data;
        }
    }
}
