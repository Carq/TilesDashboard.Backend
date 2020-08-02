using TilesDashboard.PluginBase;

namespace TilesDashboard.Plugin.Azure.CodeCoverage
{
    public class AzureCodeCoveragePluginAnalytics : AzureCodeCoveragePluginBase
    {
        public AzureCodeCoveragePluginAnalytics(IPluginConfigProvider configProvider)
            : base(configProvider)
        {
        }

        public override string RootConfig { get; } = "AzureCodeCoveragePluginAnalytics";
    }
}
