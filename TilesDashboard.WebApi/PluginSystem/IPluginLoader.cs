namespace TilesDashboard.WebApi.PluginSystem
{
    public interface IPluginLoader
    {
        public LoadedPlugins LoadPlugins(string rootPath);
    }
}
