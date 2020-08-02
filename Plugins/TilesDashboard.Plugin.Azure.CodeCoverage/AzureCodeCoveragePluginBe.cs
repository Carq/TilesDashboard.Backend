using TilesDashboard.PluginBase;

namespace TilesDashboard.Plugin.Azure.CodeCoverage
{
    public class AzureCodeCoveragePluginBe : AzureCodeCoveragePluginBase
    {
        public AzureCodeCoveragePluginBe(IPluginConfigProvider configProvider)
            : base(configProvider)
        {
        }

        public override string RootConfig { get; } = "AzureCodeCoveragePluginBe";
    }
}
