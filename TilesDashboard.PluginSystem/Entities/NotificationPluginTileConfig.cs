using System;
using System.Collections.Generic;
using TilesDashboard.V2.Core.Entities;

namespace TilesDashboard.PluginSystem.Entities
{
    public class NotificationPluginTileConfig
    {
        public NotificationPluginTileConfig(StorageId storageId, IDictionary<string, string> configuration, bool disabled)
        {
            TileStorageId = storageId?.Value ?? throw new ArgumentNullException(nameof(storageId));
            Configuration = configuration;
            Disabled = disabled;
        }

        private NotificationPluginTileConfig()
        {
        }

        public string TileStorageId { get; private set; }

        public IDictionary<string, string> Configuration { get; private set; }

        public bool Disabled { get; private set; }
    }
}
