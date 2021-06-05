using System.Collections.Generic;
using TilesDashboard.PluginBase.Data;

namespace TilesDashboard.PluginSystem.DataPlugins
{
    public interface IDataPluginContext
    {
        IList<IDataPlugin> DataPlugins { get; }

        void AddPlugins(IList<IDataPlugin> dataPlugins);
    }
}