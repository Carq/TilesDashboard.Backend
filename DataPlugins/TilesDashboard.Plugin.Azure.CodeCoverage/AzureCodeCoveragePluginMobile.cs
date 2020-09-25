using TilesDashboard.PluginBase;

namespace TilesDashboard.Plugin.Azure.CodeCoverage
{
    public class AzureCodeCoveragePluginMobile : AzureCodeCoveragePluginBase
    {
        public AzureCodeCoveragePluginMobile(IPluginConfigProvider configProvider)
            : base(configProvider)
        {
        }

        public override string RootConfig { get; } = "AzureCodeCoveragePluginMobile";
    }
}
