using System;
using System.Diagnostics;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using TilesDashboard.Plugin.HeartBeatGeneral.Dto;
using TilesDashboard.PluginBase;
using TilesDashboard.PluginBase.Data.HeartBeatPlugin;
using TilesDashboard.PluginBase.MetricPlugin;

namespace TilesDashboard.Plugin.HeartBeatGeneral
{
    public class GarminConnectorHeartBeatGeneral : HeartBeatPluginBase
    {
        public override string TileName { get; } = "Garmin Connector";

        public override string CronSchedule => ConfigProvider.GetConfigEntry($"{RootConfig}:CronSchedule");

        private readonly string RootConfig = "GarminConnectorHeartBeat";

        public GarminConnectorHeartBeatGeneral(IPluginConfigProvider pluginConfigProvider) : base(pluginConfigProvider)
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

                var responseContent = await response.Content.ReadAsStringAsync();
                var heartbeatDto = JsonSerializer.Deserialize<HeartbeatDto>(responseContent,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });


                if (response.IsSuccessStatusCode)
                {
                    return new HeartBeatData((int)stopwatcher.ElapsedMilliseconds, heartbeatDto.Version, heartbeatDto.LastAppliedMigration, Status.OK);
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
