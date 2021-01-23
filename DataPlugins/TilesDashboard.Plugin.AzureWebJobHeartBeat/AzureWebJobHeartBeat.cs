using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using TilesDashboard.Plugin.AzureWebJobHeartBeat.Dto;
using TilesDashboard.PluginBase;
using TilesDashboard.PluginBase.Data.HeartBeatPlugin;

namespace TilesDashboard.Plugin.AzureWebJobHeartBeat
{
    public class AzureWebJobHeartBeat : HeartBeatPluginBase
    {
        public override string UniquePluginName => $"TileCorePlugins.{nameof(AzureWebJobHeartBeat)}";

        public override async Task<HeartBeatData> GetTileValueAsync(IDictionary<string, string> pluginConfiguration, CancellationToken cancellation = default)
        {
            var httpClient = new HttpClient();
            var stopWatch = new Stopwatch();
            var username = pluginConfiguration["UserName"];
            var password = pluginConfiguration["Password"];
            var heartBeatAddress = pluginConfiguration["HeartBeatAddress"];

            stopWatch.Start();
            try
            {
                using var request = CreateHttpRequest(username, password, heartBeatAddress);
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
                        return new HeartBeatData((int)stopWatch.ElapsedMilliseconds, "?", null, Status.OK);
                    }
                }

                return HeartBeatData.NoResponse(); ;
            }
            catch (Exception ex)
            {
                return HeartBeatData.Error(ex.Message + ex.InnerException?.Message);
            }
        }

        private HttpRequestMessage CreateHttpRequest(string username, string password, string heartbeatAddress)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, heartbeatAddress);

            request.Headers.Add("Authorization", $"Basic {Convert.ToBase64String(Encoding.ASCII.GetBytes(username + ":" + password))}");

            return request;
        }
    }
}
