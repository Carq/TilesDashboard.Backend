using System.Collections.Generic;
using TilesDashboard.Handy.Extensions;
using TilesDashboard.PluginBase.WeatherPlugin;

namespace TilesDashboard.WebApi.PluginSystem
{
    public class Plugins
    {
        public Plugins(IList<BaseWeatherPlugin> weatherPlugins)
        {
            WeatherPlugins.AddRange(weatherPlugins);
        }

        public Plugins()
        {
        }

        public static Plugins NoPluginsLoaded => new Plugins();

        public IList<BaseWeatherPlugin> WeatherPlugins { get; } = new List<BaseWeatherPlugin>();

        public void Merge(Plugins loadedPlugins)
        {
            WeatherPlugins.AddRange(loadedPlugins.WeatherPlugins);
        }
    }
}
