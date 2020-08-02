using System;
using System.Threading;
using System.Threading.Tasks;
using TilesDashboard.PluginBase;
using TilesDashboard.PluginBase.WeatherPlugin;

namespace TilesDashboard.TestPlugins.Weather
{
    public class TestWeatherPlugin : WeatherPluginBase
    {
        public TestWeatherPlugin(IPluginConfigProvider pluginConfigProvider) 
            : base(pluginConfigProvider)
        {
        }

        public override string TileName => "Test Plugin";

        public override string CronSchedule => "10 * * * * *";

        public override Task<WeatherData> GetDataAsync(CancellationToken cancellationToken)
        {
            var random = new Random();
            int maxTemperature = int.Parse(ConfigProvider.GetConfigEntry("TestWeatherPlugin:MaxTemp"));

            return Task.FromResult(new WeatherData(random.Next(-8, maxTemperature), random.Next(1, 100), Status.OK));
        }
    }
}
