namespace TilesDashboard.WebApi.PluginInfrastructure
{
    public interface IPluginLoader
    {
        public LoadedPlugins LoadPlugins(string rootPath);
    }
}
