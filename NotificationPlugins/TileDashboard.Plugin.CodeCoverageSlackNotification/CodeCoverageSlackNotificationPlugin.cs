﻿using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using TilesDashboard.PluginBase.Notification;
using TilesDashboard.V2.Core.Entities;
using TilesDashboard.V2.Core.Entities.Enums;
using TilesDashboard.V2.Core.Entities.Metric;

namespace TileDashboard.Plugin.CodeCoverageSlackNotification
{
    public class CodeCoverageSlackNotificationPlugin : NotificationPluginBase<PercentageMetricValue>
    {
        public override string UniquePluginName => $"TileCorePlugins.{nameof(CodeCoverageSlackNotificationPlugin)}";

        public override TileType TileType => TileType.Metric;

        public override async Task PerformNotificationAsync(TileId tileId, PercentageMetricValue newData, IDictionary<string, string> pluginConfiguration, CancellationToken cancellation = default)
        {
            if (newData == null || !pluginConfiguration.TryGetValue("SlackHook", out var slackHook) || string.IsNullOrWhiteSpace(slackHook))
            {
                return;
            }

            await SendMessage($"New value for {tileId}: {newData.Value}%", slackHook, cancellation);
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
