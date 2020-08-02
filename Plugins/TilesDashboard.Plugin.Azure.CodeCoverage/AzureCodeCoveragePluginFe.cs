using TilesDashboard.PluginBase;

namespace TilesDashboard.Plugin.Azure.CodeCoverage
{
    public class AzureCodeCoveragePluginFe : AzureCodeCoveragePluginBase
    {
        public AzureCodeCoveragePluginFe(IPluginConfigProvider configProvider)
            : base(configProvider)
        {
        }

        public override string RootConfig { get; } = "AzureCodeCoveragePluginFe";
    }
}
