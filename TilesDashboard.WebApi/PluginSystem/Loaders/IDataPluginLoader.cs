using System.Collections.Generic;
using System.Threading.Tasks;
using TilesDashboard.PluginBase.Data;

namespace TilesDashboard.WebApi.PluginSystem
{
    public interface IDataPluginLoader
    {
        Task<IList<IDataPlugin>> LoadDataPluginsAsync(string rootPath);
    }
}
