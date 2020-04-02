using System.Collections.Generic;
using TilesDashboard.Handy.Extensions;
using TilesDashboard.PluginBase.WeatherPlugin;

namespace TilesDashboard.WebApi.PluginSystem
{
    public class LoadedPlugins
    {
        public LoadedPlugins(IList<BaseWeatherPlugin> weatherPlugins)
        {
            WeatherPlugins.AddRange(weatherPlugins);
        }

        public LoadedPlugins()
        {
        }

        public static LoadedPlugins NoPluginsLoaded => new LoadedPlugins();

        public IList<BaseWeatherPlugin> WeatherPlugins { get; } = new List<BaseWeatherPlugin>();

        public void Merge(LoadedPlugins loadedPlugins)
        {
            WeatherPlugins.AddRange(loadedPlugins.WeatherPlugins);
        }
    }
}
