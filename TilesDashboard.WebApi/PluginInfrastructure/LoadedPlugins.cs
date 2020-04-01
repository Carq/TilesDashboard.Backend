using System.Collections.Generic;
using TilesDashboard.Handy.Extensions;
using TilesDashboard.PluginBase;

namespace TilesDashboard.WebApi.PluginInfrastructure
{
    public class LoadedPlugins
    {
        public LoadedPlugins(IList<IWeatherPlugin> weatherPlugins)
        {
            WeatherPlugins.AddRange(weatherPlugins);
        }

        public LoadedPlugins()
        {
        }

        public static LoadedPlugins NoPluginsLoaded => new LoadedPlugins();

        public IList<IWeatherPlugin> WeatherPlugins { get; } = new List<IWeatherPlugin>();

        public void Merge(LoadedPlugins loadedPlugins)
        {
            WeatherPlugins.AddRange(loadedPlugins.WeatherPlugins);
        }
    }
}
