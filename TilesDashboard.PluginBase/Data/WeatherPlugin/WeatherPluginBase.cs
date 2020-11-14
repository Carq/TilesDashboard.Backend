using TilesDashboard.V2.Core.Entities.Enums;

namespace TilesDashboard.PluginBase.Data.WeatherPlugin
{
    public abstract class WeatherPluginBase : DataPluginBase<WeatherData>
    {
        protected WeatherPluginBase(IPluginConfigProvider pluginConfigProvider)
            : base(pluginConfigProvider)
        {
        }

        protected WeatherPluginBase()
        {
        }

        public override TileType TileType => TileType.Weather;
    }
}
