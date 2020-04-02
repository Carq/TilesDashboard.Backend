using Microsoft.Extensions.Configuration;
using TilesDashboard.PluginBase;
using TilesDashboard.WebApi.Configuration;

namespace TilesDashboard.WebApi.PluginSystem
{
    public class PluginConfigProvider : BaseSettings, IPluginConfigProvider
    {
        public PluginConfigProvider(IConfiguration configuration) : base(configuration)
        {
        }

        public string BaseRoot { get; } = "PluginsConfigs:";

        public string GetConfigEntry(string configEntry) => GetValue<string>(BaseRoot + configEntry);
    }
}
