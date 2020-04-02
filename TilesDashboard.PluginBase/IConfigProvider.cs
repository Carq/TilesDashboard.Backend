namespace TilesDashboard.PluginBase
{
    public interface IPluginConfigProvider
    {
        /// <summary>
        /// Get entry from configuration file. Configuration entries have to be in section "PluginsConfigs".
        /// </summary>
        /// <param name="configEntry">Path to config entry inside of "PluginsConfigs" section.</param>
        public string GetConfigEntry(string configEntry);
    }
}
