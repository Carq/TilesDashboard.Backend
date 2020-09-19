using System.Collections.Generic;
using System.Threading.Tasks;
using TilesDashboard.PluginBase.Data;

namespace TilesDashboard.WebApi.PluginSystem
{
    public interface IPluginLoader
    {
        public Task<IList<IDataPlugin>> LoadDataProviderPluginsAsync(string rootPath);
    }
}
