namespace TilesDashboard.PluginBase.Data
{
    public interface IDataPlugin : IPlugin
    {
        PluginType IPlugin.PluginType => PluginType.Data;
    }
}
