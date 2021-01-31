using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using TilesDashboard.PluginBase.Notification;
using TilesDashboard.V2.Core.Entities;
using TilesDashboard.V2.Core.Entities.Enums;
using TilesDashboard.V2.Core.Entities.Weather;

namespace TilesDashboard.Plugin.WeatherSlackNotification
{
    public class WeatherSlackNotification : NotificationPluginBase<WeatherValue>
    {
        public override string UniquePluginName => $"TileCorePlugins.{nameof(WeatherSlackNotification)}";

        public override TileType TileType => TileType.Weather;

        public override async Task PerformNotificationAsync(TileId tileId, WeatherValue newData, IDictionary<string, string> pluginConfiguration, CancellationToken cancellation = default)
        {
            if (newData == null || !pluginConfiguration.TryGetValue("SlackHook", out var slackHook) || string.IsNullOrWhiteSpace(slackHook))
            {
                return;
            }

            if (newData.Temperature > 22.5m)
            {
                await SendMessage($"New value for {tileId}: {newData.Temperature}°C", slackHook, cancellation);
            }
        }

        private async Task SendMessage(string message, string slackHook, CancellationToken cancellationToken)
        {
            using var httpClient = new HttpClient();
            var requestContent = JsonSerializer.Serialize(new { text = message });
            using var request = new HttpRequestMessage(HttpMethod.Post, slackHook)
            {
                Content = new StringContent(requestContent, Encoding.UTF8, "application/json")
            };

            await httpClient.SendAsync(request, cancellationToken);
        }
    }
}
