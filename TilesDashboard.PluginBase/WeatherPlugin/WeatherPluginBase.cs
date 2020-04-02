using System.Threading.Tasks;

namespace TilesDashboard.PluginBase.WeatherPlugin
{
    public abstract class BaseWeatherPlugin
    {
        /// <summary>
        /// Tile Name which is used to update Tile in database and to display tile name on frontend.
        /// </summary>
        public abstract string TileName { get; }

        /// <summary>
        /// Give access to config entries. Config Provider will be injected by PluginSystem.
        /// </summary>
        public IPluginConfigProvider ConfigProvider { get; }

        public abstract Task<WeatherData> GetDataAsync();
    }
}
