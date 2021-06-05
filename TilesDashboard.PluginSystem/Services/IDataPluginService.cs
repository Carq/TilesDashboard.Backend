using System.Threading;
using System.Threading.Tasks;
using TilesDashboard.PluginBase.Data;
using TilesDashboard.PluginSystem.Entities;

namespace TilesDashboard.PluginSystem.Services
{
    public interface IDataPluginService
    {
        Task ExecuteDataPluginForTile(string pluginName, string tileStorageId, CancellationToken cancellationToken);

        Task<PluginDataResult> HandlePlugin(IDataPlugin plugin, PluginTileConfig pluginConfigurationForTile,
            CancellationToken cancellationToken);
    }
}