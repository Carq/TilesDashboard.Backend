using System;
using TilesDashboard.PluginBase;
using TilesDashboard.PluginBase.WeatherPluginBase;

namespace TilesDashboard.DefaultPlugins.Weather
{
    public class BasicWeatherPlugin : IWeatherPlugin
    {
        public string Name => "Random Weather";

        public WeatherData GetData()
        {
            var random = new Random();
            return new WeatherData(random.Next(-8, 30), random.Next(1, 100));
        }
    }
}
