using System;
using System.Threading.Tasks;
using TilesDashboard.PluginBase.WeatherPlugin;

namespace TilesDashboard.TestPlugins.Weather
{
    public class TestExceptionWeatherPlugin : BaseWeatherPlugin
    {
        public override string TileName => "Test Exception Weather";

        public override Task<WeatherData> GetDataAsync()
        {
            throw new NotImplementedException("Test exception");
        }
    }
}
