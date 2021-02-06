using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TilesDashboard.V2.Core.Entities;
using TilesDashboard.V2.Core.Entities.Enums;

namespace TilesDashboard.PluginBase.Notification
{
    public abstract class NotificationPluginBase<TTileData> : INotificationPluginWithTileValue<TTileData>
        where TTileData : ITileValue
    {
         /// <summary>
        /// Unique Plugin Name which is used to get plugin config from storage.
        /// </summary>
        public abstract string UniquePluginName { get; }

        /// <summary>
        /// Tile type.
        /// </summary>
        public abstract TileType TileType { get; }

        public abstract Task PerformNotificationAsync(TileId tileId, TTileData newData, IReadOnlyDictionary<string, string> pluginConfiguration, IReadOnlyDictionary<string, string> tileConfiguration, CancellationToken cancellation = default);
    }
}
