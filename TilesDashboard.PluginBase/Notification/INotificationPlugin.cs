namespace TilesDashboard.PluginBase.Notification
{
    public interface INotificationPlugin : IPlugin
    {
        PluginType IPlugin.PluginType => PluginType.Notification;
    }
}
