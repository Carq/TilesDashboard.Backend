using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TilesDashboard.PluginBase;
using TilesDashboard.PluginBase.Data.MetricPlugin;
using TilesDashboard.V2.Core.Entities.Enums;

namespace TilesDashboard.Plugin.Azure.CodeCoverage
{
    public abstract class AzureCodeCoveragePluginBase : MetricPluginBase
    {
        public AzureCodeCoveragePluginBase(IPluginConfigProvider configProvider)
            : base(configProvider)
        {
        }

        public abstract string RootConfig { get; }

        public override string TileName => ConfigProvider.GetConfigEntry($"{RootConfig}:TileName");

        public override string CronSchedule => ConfigProvider.GetConfigEntry($"{RootConfig}:CronSchedule");

        public override Task InitializeAsync()
        {
            return Task.CompletedTask;
        }

        public override async Task<MetricData> GetDataAsync(CancellationToken cancellationToken)
        {
            try
            {
                var azureDevOpsHelper = new AzureDevOpsHelper(ConfigProvider, RootConfig);
                var codeCoverageDetails = await azureDevOpsHelper.GetCodeCoverageResultForLastGreenBuildAsync(cancellationToken);
                if (codeCoverageDetails == null)
                {
                    return MetricData.NoUpdate();
                }

                var linesCoverageData = codeCoverageDetails.CoverageData.First().CoverageStats.Single(x => x.Label == "Lines");
                var percentageCoverage = Math.Round(100m * linesCoverageData.Covered / linesCoverageData.Total, 1);

                return new MetricData(percentageCoverage, MetricType.Percentage, Status.OK);
            }
            catch (Exception ex)
            {
                return MetricData.Error(ex.Message);
            }
        }
    }
}
