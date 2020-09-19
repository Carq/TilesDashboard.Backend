using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TilesDashboard.Core.Domain.Services;
using TilesDashboard.Handy.Extensions;
using TilesDashboard.PluginBase;
using TilesDashboard.PluginBase.Data;
using TilesDashboard.PluginBase.Data.IntegerPlugin;

namespace TilesDashboard.WebApi.BackgroundWorkers
{
    public class IntegerPluginHandler
    {
        private readonly ILogger<IntegerPluginHandler> _logger;

        private readonly IIntegerTileService _integerTileService;

        public IntegerPluginHandler(ILogger<IntegerPluginHandler> logger, IIntegerTileService integerTileService)
        {
            _logger = logger;
            _integerTileService = integerTileService;
        }

        public async Task<Result> HandlePlugin(IntegerPluginBase integerPlugin, CancellationToken stoppingToken)
        {
            var data = await integerPlugin.GetDataAsync();
            _logger.LogDebug($"Integer plugin: \"{integerPlugin.TileName}\", Value: {data.Value}");
            if (data.Status.Is(Status.OK))
            {
                await _integerTileService.RecordIntegerDataAsync(integerPlugin.TileName, data.Value, stoppingToken);
            }

            return data;
        }
    }
}
