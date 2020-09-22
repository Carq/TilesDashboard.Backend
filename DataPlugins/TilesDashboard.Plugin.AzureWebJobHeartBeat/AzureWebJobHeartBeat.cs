using System;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using TilesDashboard.Plugin.AzureWebJobHeartBeat.Dto;
using TilesDashboard.PluginBase;
using TilesDashboard.PluginBase.Data.HeartBeatPlugin;
using TilesDashboard.PluginBase.MetricPlugin;

namespace TilesDashboard.Plugin.AzureWebJobHeartBeat
{
    public class AzureWebJobHeartBeat : HeartBeatPluginBase
    {
        private readonly string RootConfig = "HeartBeatTelemetryJob";

        public AzureWebJobHeartBeat(IPluginConfigProvider pluginConfigProvider) : base(pluginConfigProvider)
        {
        }

        public override string TileName { get; } = "Telemetry Job";

        public override string CronSchedule => ConfigProvider.GetConfigEntry($"{RootConfig}:CronSchedule");

        public override async Task<HeartBeatData> GetDataAsync(CancellationToken cancellation = default)
        {
            var httpClient = new HttpClient();
            var stopWatch = new Stopwatch();

            stopWatch.Start();
            try
            {
                using var request = CreateHttpRequest();
                using var response = await httpClient.SendAsync(request, cancellation);

                stopWatch.Stop();
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var webJobResponse = JsonSerializer.Deserialize<AzureWebJobStatusResponseDto>(responseContent,
                        new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        });

                    if (webJobResponse.Status == "Running")
                    {
                        return new HeartBeatData((int)stopWatch.ElapsedMilliseconds, Status.OK);
                    }
                }

                return new HeartBeatData(0, Status.OK);
            }
            catch (Exception ex)
            {
                return HeartBeatData.Error(ex.Message + ex.InnerException?.Message);
            }
        }

        private HttpRequestMessage CreateHttpRequest()
        {
            var username = ConfigProvider.GetConfigEntry($"{RootConfig}:UserName");
            var password = ConfigProvider.GetConfigEntry($"{RootConfig}:Password");
            var request = new HttpRequestMessage(HttpMethod.Get, ConfigProvider.GetConfigEntry($"{RootConfig}:HeartBeatAddress"));

            request.Headers.Add("Authorization", $"Basic {Convert.ToBase64String(Encoding.ASCII.GetBytes(username + ":" + password))}");

            return request;
        }
    }
}
