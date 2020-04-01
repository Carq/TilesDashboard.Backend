using TilesDashboard.PluginBase.WeatherPluginBase;

namespace TilesDashboard.PluginBase
{
    public interface IWeatherPlugin
    {
        public string Name { get; }

        WeatherData GetData();
    }
}
