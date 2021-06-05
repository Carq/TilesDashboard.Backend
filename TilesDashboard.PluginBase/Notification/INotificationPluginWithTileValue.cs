using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TilesDashboard.V2.Core.Entities;

namespace TilesDashboard.PluginBase.Notification
{
    public interface INotificationPluginWithTileValue<TTileData> : INotificationPlugin
        where TTileData : ITileValue
    {
        PluginType IPlugin.PluginType => PluginType.Notification;

        Task PerformNotificationAsync(TileId tileId, TTileData newData, IReadOnlyDictionary<string, string> pluginConfiguration, IReadOnlyDictionary<string, string> tileConfiguration, CancellationToken cancellation = default);
    }
}
