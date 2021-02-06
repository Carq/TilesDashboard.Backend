using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TilesDashboard.PluginBase;
using TilesDashboard.PluginSystem.Entities;
using TilesDashboard.V2.Core.Entities.Enums;

namespace TilesDashboard.PluginSystem.Repositories
{
    public interface IPluginConfigRepository
    {
        Task<IList<PluginConfiguration>> GetEnabledDataPluginsConfiguration(CancellationToken cancellationToken);

        Task<bool> IsAnyPluginConfigurationExist(string pluginName, CancellationToken cancellationToken);
        
        Task CreatePluginConfigurationWithTempleteEntry(string uniquePluginName, TileType tileType, PluginType pluginType, CancellationToken cancellationToken);

        Task<IList<PluginConfiguration>> GetNotificationConfigs(TileType type, CancellationToken cancellationToken);
    }
}
