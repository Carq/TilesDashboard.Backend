using System;
using System.Collections.Generic;
using TilesDashboard.V2.Core.Entities;

namespace TilesDashboard.PluginSystem
{
    public class PluginConfigForTile
    {
        public PluginConfigForTile(string pluginName, StorageId tileStorageId, string cronSchedule, IDictionary<string, string> configuration)
        {
            PluginName = pluginName ?? throw new ArgumentNullException(nameof(pluginName));
            TileStorageId = tileStorageId ?? throw new ArgumentNullException(nameof(tileStorageId));
            CronSchedule = cronSchedule ?? throw new ArgumentNullException(nameof(cronSchedule));
            Configuration = configuration;
        }

        public string PluginName { get; }

        public StorageId TileStorageId { get; }

        public string CronSchedule { get; }

        public IDictionary<string, string> Configuration { get; }
    }
}
