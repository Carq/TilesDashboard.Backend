using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TilesDashboard.Handy.Extensions;
using TilesDashboard.PluginBase;
using TilesDashboard.PluginBase.Data;
using TilesDashboard.PluginBase.V2;
using TilesDashboard.PluginSystem.Entities;
using TilesDashboard.V2.Core.Entities;
using TilesDashboard.V2.Core.Services;

namespace TilesDashboard.WebApi.BackgroundWorkers
{
    public class WeatherPluginHandler
    {
        private readonly ILogger<WeatherPluginHandler> _logger;

        private readonly IWeatherService _weatherService;

        public WeatherPluginHandler(ILogger<WeatherPluginHandler> logger, IWeatherService weatherService)
        {
            _logger = logger;
            _weatherService = weatherService;
        }

        public async Task<Result> HandlePlugin(WeatherPluginBase weatherPlugin, PluginTileConfig pluginConfigForTile, CancellationToken cancellationToken)
        {
            var data = await weatherPlugin.GetTileValueAsync(pluginConfigForTile.Configuration, cancellationToken);
            _logger.LogDebug($"Weather plugin: \"{weatherPlugin.UniquePluginName}\", Temperature: {data.Temperature}, Huminidy: {data.Huminidy}%");
            if (data.Status.Is(Status.OK))
            {
                await _weatherService.RecordValue(new StorageId(pluginConfigForTile.TileStorageId), data.Temperature, data.Huminidy ?? 0);
            }

            return data;
        }
    }
}
