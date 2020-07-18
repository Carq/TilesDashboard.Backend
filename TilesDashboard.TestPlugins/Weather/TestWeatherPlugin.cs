using System;
using System.Threading.Tasks;
using TilesDashboard.PluginBase;
using TilesDashboard.PluginBase.WeatherPlugin;

namespace TilesDashboard.TestPlugins.Weather
{
    public class TestWeatherPlugin : BaseWeatherPlugin
    {
        public TestWeatherPlugin(IPluginConfigProvider pluginConfigProvider) 
            : base(pluginConfigProvider)
        {
        }

        public override string TileName => "Test Weather";

        public override Task<WeatherData> GetDataAsync()
        {
            var random = new Random();
            int maxTemperature = int.Parse(ConfigProvider.GetConfigEntry("TestWeatherPlugin:MaxTemp"));

            return Task.FromResult(new WeatherData(random.Next(-8, maxTemperature), random.Next(1, 100), Status.OK, DateTimeOffset.Now));
        }
    }
}
