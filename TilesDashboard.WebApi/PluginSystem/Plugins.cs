using System.Collections;
using System.Collections.Generic;
using TilesDashboard.Handy.Extensions;
using TilesDashboard.PluginBase;

namespace TilesDashboard.WebApi.PluginSystem
{
    public class Plugins : IEnumerable<IPlugin>
    {
        public Plugins(IList<IPlugin> plugins)
        {
            LoadedPlugins.AddRange(plugins);
        }

        public Plugins()
        {
        }

        public static Plugins NoPluginsLoaded => new Plugins();

        public IList<IPlugin> LoadedPlugins { get; } = new List<IPlugin>();

        public void Add(IPlugin plugin)
        {
            LoadedPlugins.Add(plugin);
        }

        public void Merge(Plugins loadedPlugins)
        {
            LoadedPlugins.AddRange(loadedPlugins.LoadedPlugins);
        }

        public IEnumerator<IPlugin> GetEnumerator() => LoadedPlugins.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
