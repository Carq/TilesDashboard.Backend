using System;
using System.Threading;
using System.Threading.Tasks;
using TilesDashboard.PluginBase.WeatherPlugin;

namespace TilesDashboard.TestPlugins.Weather
{
    public class TestExceptionWeatherPlugin : WeatherPluginBase
    {
        public override string TileName => "Test Exception Weather";

        public override string CronSchedule => "30 * * * * *";

        public override Task<WeatherData> GetDataAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException("Test exception");
        }
    }
}
