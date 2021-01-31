using System;
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

namespace TilesDashboard.Plugin.ApartmentWeatherNotification
{
    public class ApartmentWeatherNotification : NotificationPluginBase<WeatherValue>
    {
        public override string UniquePluginName => $"TileCorePlugins.{nameof(ApartmentWeatherNotification)}";

        public override TileType TileType => TileType.Weather;

        public override async Task PerformNotificationAsync(TileId tileId, WeatherValue newData, IDictionary<string, string> pluginConfiguration, CancellationToken cancellation = default)
        {

            if (newData == null)
            {
                return;
            }

            decimal lowHumidity = decimal.Parse(pluginConfiguration["LowHumidity"]);
            string webHook = pluginConfiguration["Webhook"];
            if (newData.Humidity <= lowHumidity)
            {
                await SendMessage(tileId, newData, webHook, cancellation);
            }
        }

        private async Task SendMessage(TileId tileId, WeatherValue newData, string webHook, CancellationToken cancellationToken)
        {
            using var httpClient = new HttpClient();
            var requestContent = JsonSerializer.Serialize(new { value1 = tileId.Name, value2 = $"{(int)newData.Humidity}%", value3 = $"{newData.Temperature}°C" });
            using var request = new HttpRequestMessage(HttpMethod.Post, webHook)
            {
                Content = new StringContent(requestContent, Encoding.UTF8, "application/json")
            };

            await httpClient.SendAsync(request, cancellationToken);
        }
    }
}
