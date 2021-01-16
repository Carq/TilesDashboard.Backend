using TilesDashboard.PluginBase.Data.WeatherPlugin;
using TilesDashboard.V2.Core.Entities.Enums;

namespace TilesDashboard.PluginBase.Data.WeatherPlugin
{
    public abstract class WeatherPluginBase : DataPluginBase<WeatherData> 
    {
        public override TileType TileType => TileType.Weather;
    }
}
