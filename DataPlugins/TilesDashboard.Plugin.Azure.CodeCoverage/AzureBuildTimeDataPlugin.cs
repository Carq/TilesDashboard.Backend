using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TilesDashboard.Plugin.Azure.CodeCoverage;
using TilesDashboard.PluginBase;
using TilesDashboard.PluginBase.Data.MetricPlugin;
using TilesDashboard.V2.Core.Entities.Metric;

namespace TilesDashboard.Plugin.Azure
{
    public class AzureBuildTimeDataPlugin : MetricDataPlugin
    {
        public override string UniquePluginName => $"TileCorePlugins.{nameof(AzureBuildTimeDataPlugin)}";

        public override async Task<MetricData> GetTileValueAsync(IDictionary<string, string> pluginConfiguration, CancellationToken cancellation = default)
        {
            try
            {
                var azureDevOpsHelper = new AzureDevOpsHelper(
                                                pluginConfiguration["Organization"],
                                                pluginConfiguration["Project"],
                                                pluginConfiguration["BuildDefinition"],
                                                pluginConfiguration["PersonalAccessToken"]);

                var nowDate = DateTime.Now.Date;
                var fromBeginingOfMonth = new DateTime(nowDate.Year, nowDate.Month, 1);
                var builds = await azureDevOpsHelper.GetGreenBuildsAsync(fromBeginingOfMonth, nowDate, cancellation);
                if (builds == null || builds.Count == 0)
                {
                    return MetricData.NoUpdate();
                }

                var avgTimeBuildInSeconds = (decimal)builds.Value.Average(x => (x.FinishTime - x.StartTime).TotalSeconds);
                return new MetricData(avgTimeBuildInSeconds, MetricType.Time, Status.OK);
            }
            catch (Exception ex)
            {
                return MetricData.Error(ex.Message);
            }
        }
    }
}
