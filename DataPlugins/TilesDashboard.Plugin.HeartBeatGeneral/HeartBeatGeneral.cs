using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using TilesDashboard.PluginBase;
using TilesDashboard.PluginBase.Data.HeartBeatPlugin;
using TilesDashboard.PluginBase.MetricPlugin;

namespace TilesDashboard.Plugin.HeartBeatGeneral
{
    public class HeartBeatGeneral : HeartBeatPluginBase
    {
        public override string TileName { get; } = "API";

        public override string CronSchedule => ConfigProvider.GetConfigEntry($"{RootConfig}:CronSchedule");

        private readonly string RootConfig = "Api";

        public HeartBeatGeneral(IPluginConfigProvider pluginConfigProvider) : base(pluginConfigProvider)
        {
        }

        public override async Task<HeartBeatData> GetDataAsync(CancellationToken cancellationToken = default)
        {
            var httpClient = new HttpClient();
            var stopwatcher = new Stopwatch();

            stopwatcher.Start();
            try
            {
                var response = await httpClient.GetAsync($"{ConfigProvider.GetConfigEntry($"{RootConfig}:HeartBeatAddress")}", cancellationToken);
                stopwatcher.Stop();
                if (response.IsSuccessStatusCode)
                {
                    return new HeartBeatData((int)stopwatcher.ElapsedMilliseconds, null, null, Status.OK);
                }

                return HeartBeatData.NoResponse();
            }
            catch (Exception ex)
            {
                return HeartBeatData.Error(ex.Message + ex.InnerException.Message);
            }
        }
    }
}
