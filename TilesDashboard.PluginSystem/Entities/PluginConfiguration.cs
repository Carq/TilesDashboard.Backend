using System;
using System.Collections.Generic;
using TilesDashboard.V2.Core.Entities.Enums;

namespace TilesDashboard.PluginSystem.Entities
{
    public class PluginConfiguration
    {
        public PluginConfiguration(string pluginName, TileType tileType)
        {
            PluginName = pluginName ?? throw new ArgumentNullException(nameof(pluginName));
            TileType = tileType;
            Disable = false;
            PluginTileConfigs = new List<PluginTileConfig>();
        }

        private PluginConfiguration()
        {
        }

        public string PluginName { get; private set; }

        public TileType TileType { get; private set; }

        public bool Disable { get; private set; }

        public IList<PluginTileConfig> PluginTileConfigs { get; private set; }
    }
}
