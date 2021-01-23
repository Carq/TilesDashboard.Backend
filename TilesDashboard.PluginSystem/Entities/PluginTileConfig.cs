using System;
using System.Collections.Generic;
using TilesDashboard.V2.Core.Entities;

namespace TilesDashboard.PluginSystem.Entities
{
    public class PluginTileConfig
    {
        public PluginTileConfig(StorageId storageId, string cronSchedule, IDictionary<string, string> configuration, bool disabled)
        {
            TileStorageId = storageId?.Value ?? throw new ArgumentNullException(nameof(storageId));
            CronSchedule = cronSchedule ?? throw new ArgumentNullException(nameof(cronSchedule));
            Configuration = configuration;
            Disabled = disabled;
        }

        private PluginTileConfig()
        {
        }

        public string TileStorageId { get; private set; }

        /// <summary>
        /// Schedule execution of GetDataAsync(). https://crontab.cronhub.io/
        /// </summary>
        public string CronSchedule { get; private set; }

        public IDictionary<string, string> Configuration { get; private set; }

        public bool Disabled { get; private set; }
    }
}
