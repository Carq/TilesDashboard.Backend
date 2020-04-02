using System.Threading.Tasks;

namespace TilesDashboard.WebApi.PluginSystem
{
    public interface IPluginLoader
    {
        public Task<Plugins> LoadPluginsAsync(string rootPath);
    }
}
