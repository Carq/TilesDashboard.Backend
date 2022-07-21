using System.Collections.Generic;
using TilesDashboard.Handy.Extensions;
using TilesDashboard.PluginBase.Data;

namespace TilesDashboard.PluginSystem.DataPlugins
{
    public class DataPluginContext : IDataPluginContext
    {
        public DataPluginContext()
        {
        }

        public IList<IDataPlugin> DataPlugins { get; } = new List<IDataPlugin>();

        public void AddPlugins(IList<IDataPlugin> dataPlugins)
        {
            DataPlugins.AddRange(dataPlugins);
        }
    }
}
