using System;
using System.Threading.Tasks;
using TilesDashboard.PluginBase;
using TilesDashboard.PluginBase.WeatherPluginBase;

namespace TilesDashboard.TestPlugins.Weather
{
    public class TestExceptionWeatherPlugin : IWeatherPlugin
    {
        public string TileName => "Test Exception Weather";

        public Task<WeatherData> GetDataAsync()
        {
            throw new NotImplementedException("Test exception");
        }
    }
}
