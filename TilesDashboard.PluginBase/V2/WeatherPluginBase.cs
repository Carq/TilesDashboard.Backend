using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TilesDashboard.PluginBase.Data.WeatherPlugin;
using TilesDashboard.V2.Core.Entities.Enums;

namespace TilesDashboard.PluginBase.V2
{
    public abstract class WeatherPluginBase : IDataPlugin
    {
        public abstract string UniquePluginName { get; }

        public TileType TileType => TileType.Weather;

        public abstract Task<WeatherData> GetTileValueAsync(IDictionary<string, string> pluginConfiguration, CancellationToken cancellation = default);
    }
}
