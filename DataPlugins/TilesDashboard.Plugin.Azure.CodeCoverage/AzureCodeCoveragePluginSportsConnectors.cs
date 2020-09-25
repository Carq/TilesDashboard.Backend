using TilesDashboard.PluginBase;

namespace TilesDashboard.Plugin.Azure.CodeCoverage
{
    public class AzureCodeCoveragePluginSportsConnectors : AzureCodeCoveragePluginBase
    {
        public AzureCodeCoveragePluginSportsConnectors(IPluginConfigProvider configProvider)
            : base(configProvider)
        {
        }

        public override string RootConfig { get; } = "AzureCodeCoveragePluginSportsConnectors";
    }
}
