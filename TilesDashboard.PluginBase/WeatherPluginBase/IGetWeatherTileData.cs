using TilesDashboard.PluginBase.WeatherPluginBase;

namespace TilesDashboard.PluginBase
{
    interface IGetWeatherTileData
    {
        public string Name { get; }

        WeatherData GetData();
    }
}
