using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using TilesDashboard.Plugin.HeartBeatGeneral.Dto;
using TilesDashboard.PluginBase.Data;
using TilesDashboard.PluginBase.Data.HeartBeatPlugin;

namespace TilesDashboard.Plugin.HeartBeatGeneral
{
    public class HealthCheckHeartBeatGeneral : HeartBeatDataPlugin
    {
        public override string UniquePluginName => $"TileCorePlugins.{nameof(HealthCheckHeartBeatGeneral)}";

        public override async Task<HeartBeatData> GetTileValueAsync(IDictionary<string, string> pluginConfiguration, CancellationToken cancellation = default)
        {
            var httpClient = new HttpClient();
            var stopwatcher = new Stopwatch();
            var heartBeatAddress = pluginConfiguration["HeartBeatAddress"];

            stopwatcher.Start();
            try
            {
                var response = await httpClient.GetAsync(heartBeatAddress, cancellation);
                stopwatcher.Stop();

                var responseContent = await response.Content.ReadAsStringAsync();
                var heartbeatDto = JsonSerializer.Deserialize<HeartbeatDto>(
                    responseContent,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });


                if (response.IsSuccessStatusCode)
                {
                    return new HeartBeatData((int)stopwatcher.ElapsedMilliseconds, heartbeatDto.Version, heartbeatDto.LastAppliedMigration, Status.OK);
                }

                return HeartBeatData.Error($"Code: {response.StatusCode}");
            }
            catch (Exception ex)
            {
                return HeartBeatData.Error(ex.Message + ex.InnerException.Message);
            }
        }
    }
}
