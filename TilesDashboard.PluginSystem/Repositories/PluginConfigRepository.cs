using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TilesDashboard.PluginBase;
using TilesDashboard.PluginSystem.Entities;
using TilesDashboard.PluginSystem.Storage;
using TilesDashboard.V2.Core.Entities;
using TilesDashboard.V2.Core.Entities.Enums;

namespace TilesDashboard.PluginSystem.Repositories
{
    public class PluginConfigRepository : IPluginConfigRepository
    {
        private readonly IPluginSystemStorage _pluginSystemStorage;

        private IList<PluginConfiguration> _notificationPluginsCache;

        private readonly PluginTileConfig _disabledTemplateOfPluginTileConfig =
                                                new PluginTileConfig(
                                                    new StorageId("5fa824caee570237cc96b0f9"),
                                                    "0 0 19 * * 1,2,3,4,5",
                                                    new Dictionary<string, string>()
                                                    {
                                                        { "Testconfig", "urlToPage" }
                                                    },
                                                    true);

          private readonly NotificationPluginTileConfig _disabledTemplateOfNotificaiotnPluginTileConfig =
                                                new NotificationPluginTileConfig(
                                                    new StorageId("5fa824caee570237cc96b0f9"),
                                                    new Dictionary<string, string>()
                                                    {
                                                        { "Testconfig", "urlToPage" }
                                                    },
                                                    true);

        public PluginConfigRepository(IPluginSystemStorage pluginSystemStorage)
        {
            _pluginSystemStorage = pluginSystemStorage ?? throw new ArgumentNullException(nameof(pluginSystemStorage));
        }

        public async Task CreatePluginConfigurationWithTempleteEntry(string uniquePluginName, TileType tileType, PluginType pluginType,  CancellationToken cancellationToken)
        {
            var pluginConfiguration = new PluginConfiguration(uniquePluginName, tileType, pluginType);

            if (pluginType == PluginType.Data)
            {
                pluginConfiguration.PluginTileConfigs.Add(_disabledTemplateOfPluginTileConfig);
            }
            else
            {
                pluginConfiguration.NotificationPluginTileConfigs.Add(_disabledTemplateOfNotificaiotnPluginTileConfig);
            }

            await _pluginSystemStorage.PluginsConfigurations.InsertOneAsync(pluginConfiguration, null, cancellationToken);
        }

        public async Task<IList<PluginConfiguration>> GetNotificationConfigs(TileType tileType, CancellationToken cancellationToken)
        {
            return await _pluginSystemStorage.PluginsConfigurations
                .Find(x => x.Disable == false && x.PluginType == PluginType.Notification && x.TileType == tileType)
                .ToListAsync(cancellationToken);
        }

        public async Task<IList<PluginConfiguration>> GetEnabledDataPluginsConfiguration(CancellationToken cancellationToken)
        {
            if (_notificationPluginsCache == null)
            {
                _notificationPluginsCache = await _pluginSystemStorage.PluginsConfigurations.Find(x => x.Disable == false && x.PluginType == PluginType.Data).ToListAsync(cancellationToken);
            }

            return _notificationPluginsCache;
        }

        public async Task<bool> IsAnyPluginConfigurationExist(string pluginName, CancellationToken cancellationToken)
        {
            return await _pluginSystemStorage.PluginsConfigurations.Find(x => x.PluginName == pluginName).AnyAsync(cancellationToken);
        }
    }
}
