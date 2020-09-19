using System;
using System.Threading;
using System.Threading.Tasks;
using TilesDashboard.Core.Type;
using TilesDashboard.Core.Type.Enums;
using TilesDashboard.PluginBase;
using TilesDashboard.PluginBase.Notification;

namespace TileDashboard.TestNotificationPlugins
{
    public class WeatherExceptionNotificationPlugin : NotificationPluginBase
    {
        public WeatherExceptionNotificationPlugin(IPluginConfigProvider pluginConfigProvider)
        : base(pluginConfigProvider)
        {
        }

        public override TileId TileId => new TileId("Gliwice", TileType.Weather);

        public override Task PerformNotificationAsync(object newData, CancellationToken cancellationToken)
        {
            throw new NotImplementedException("Test Notification exception");
        }
    }
}
