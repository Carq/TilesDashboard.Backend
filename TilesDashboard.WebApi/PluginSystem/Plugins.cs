using System.Collections.Generic;
using TilesDashboard.Handy.Extensions;
using TilesDashboard.PluginBase.MetricPlugin;
using TilesDashboard.PluginBase.WeatherPlugin;

namespace TilesDashboard.WebApi.PluginSystem
{
    public class Plugins
    {
        public Plugins(IList<WeatherPluginBase> weatherPlugins, IList<MetricPluginBase> metricPlugins)
        {
            WeatherPlugins.AddRange(weatherPlugins);
            MetricPlugins.AddRange(metricPlugins);
        }

        public Plugins()
        {
        }

        public static Plugins NoPluginsLoaded => new Plugins();

        public IList<WeatherPluginBase> WeatherPlugins { get; } = new List<WeatherPluginBase>();

        public IList<MetricPluginBase> MetricPlugins { get; } = new List<MetricPluginBase>();

        public void Merge(Plugins loadedPlugins)
        {
            WeatherPlugins.AddRange(loadedPlugins.WeatherPlugins);
            MetricPlugins.AddRange(loadedPlugins.MetricPlugins);
        }
    }
}
