using Microsoft.Extensions.Configuration;

namespace TilesDashboard.WebApi.Configuration
{
    public class PluginSystemConfig : BaseSettings, IPluginSystemConfig
    {
        public PluginSystemConfig(IConfiguration configuration)
            : base(configuration)
        {
        }

        public string BaseRoot { get; } = "PluginSystem:";

        public int DataRefreshIntervalInSeconds => GetValue<int>(BaseRoot + nameof(DataRefreshIntervalInSeconds));
    }
}
