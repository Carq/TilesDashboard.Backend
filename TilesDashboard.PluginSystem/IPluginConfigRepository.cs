using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace TilesDashboard.PluginSystem
{
    public interface IPluginConfigRepository
    {
        Task<IList<PluginConfigForTile>> GetAllPluginsConfiguration(CancellationToken cancellationToken);
    }
}
