using System;
using System.Collections.Generic;
using TilesDashboard.Handy.Extensions;
using TilesDashboard.PluginBase;
using TilesDashboard.V2.Core.Entities.Enums;
using TilesDashboard.V2.Core.Entities.Exceptions;

namespace TilesDashboard.PluginSystem.Entities
{
    public class PluginConfiguration
    {
        public PluginConfiguration(string pluginName, TileType tileType, PluginType pluginType)
        {
            PluginName = pluginName ?? throw new ArgumentNullException(nameof(pluginName));
            TileType = tileType;
            Disable = false;
            PluginType = pluginType.IsUndefined() ? throw new ValidationException("PluginType cannot be Undefined") : pluginType;

            if (pluginType == PluginType.Data)
            {
                PluginTileConfigs = new List<PluginTileConfig>();
                NotificationPluginTileConfigs = null;
            }
            else
            {
                PluginTileConfigs = null;
                NotificationPluginTileConfigs = new List<NotificationPluginTileConfig>();
            }
        }

        private PluginConfiguration()
        {
        }

        public string PluginName { get; private set; }

        public TileType TileType { get; private set; }

        public bool Disable { get; private set; }

        public PluginType PluginType { get; private set; }

        public IList<PluginTileConfig> PluginTileConfigs { get; private set; }

        public IList<NotificationPluginTileConfig> NotificationPluginTileConfigs { get; private set; }
    }
}
