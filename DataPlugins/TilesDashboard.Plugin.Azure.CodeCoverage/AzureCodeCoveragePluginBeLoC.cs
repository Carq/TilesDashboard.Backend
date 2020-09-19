using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TilesDashboard.PluginBase;
using TilesDashboard.PluginBase.Data.IntegerPlugin;

namespace TilesDashboard.Plugin.Azure.CodeCoverage
{
    public class AzureCodeCoveragePluginBeLoC : IntegerPluginBase
    {
        public AzureCodeCoveragePluginBeLoC(IPluginConfigProvider configProvider)
            : base(configProvider)
        {
        }

        public string RootConfig { get; } = "AzureCodeCoveragePluginBeLoC";

        public override string TileName => ConfigProvider.GetConfigEntry($"{RootConfig}:TileName");

        public override string CronSchedule => ConfigProvider.GetConfigEntry($"{RootConfig}:CronSchedule");

        public override async Task<IntegerData> GetDataAsync(CancellationToken cancellationToken)
        {
            try
            {
                var azureDevOpsHelper = new AzureDevOpsHelper(ConfigProvider, RootConfig);
                var codeCoverageDetails = await azureDevOpsHelper.GetCodeCoverageResultForLastGreenBuildAsync(cancellationToken);
                if (codeCoverageDetails == null)
                {
                    return IntegerData.NoUpdate();
                }

                var linesCoverageData = codeCoverageDetails.CoverageData.First().CoverageStats.Single(x => x.Label == "Lines");
                return new IntegerData(linesCoverageData.Total, Status.OK);
            }
            catch (Exception ex)
            {
                return IntegerData.Error(ex.Message);
            }
        }
    }
}
