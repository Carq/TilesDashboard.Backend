using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TilesDashboard.PluginSystem.Entities;
using TilesDashboard.V2.Core.Entities.Enums;

namespace TilesDashboard.PluginSystem.Repositories
{
    public interface IPluginConfigRepository
    {
        Task<IList<PluginConfiguration>> GetEnabledPluginsConfiguration(CancellationToken cancellationToken);

        Task<bool> IsAnyPluginConfigurationExist(string pluginName, CancellationToken cancellationToken);
        
        Task CreatePluginConfigurationWithTempleteEntry(string uniquePluginName, TileType tileType, CancellationToken cancellationToken);
    }
}
