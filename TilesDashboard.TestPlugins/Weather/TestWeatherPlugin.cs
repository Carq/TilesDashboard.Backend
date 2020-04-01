using System;
using System.Threading.Tasks;
using TilesDashboard.PluginBase;
using TilesDashboard.PluginBase.WeatherPluginBase;

namespace TilesDashboard.TestPlugins.Weather
{
    public class TestWeatherPlugin : IWeatherPlugin
    {
        public string TileName => "Test Weather";

        public Task<WeatherData> GetDataAsync()
        {
            var random = new Random();
            return Task.FromResult(new WeatherData(random.Next(-8, 30), random.Next(1, 100), DateTimeOffset.Now));
        }
    }
}
