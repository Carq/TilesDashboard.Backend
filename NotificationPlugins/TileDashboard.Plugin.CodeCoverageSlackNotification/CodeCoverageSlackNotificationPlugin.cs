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
using TilesDashboard.V2.Core.Entities.Metric;

namespace TileDashboard.Plugin.CodeCoverageSlackNotification
{
    public class CodeCoverageSlackNotificationPlugin : NotificationPluginBase<PercentageMetricValue>
    {
        public override string UniquePluginName => $"TileCorePlugins.{nameof(CodeCoverageSlackNotificationPlugin)}";

        public override TileType TileType => TileType.Metric;

        public override async Task PerformNotificationAsync(TileId tileId, PercentageMetricValue newData, IDictionary<string, string> pluginConfiguration, CancellationToken cancellation = default)
        {
            decimal wish = decimal.Parse(pluginConfiguration["Wish"]);
            decimal goal = decimal.Parse(pluginConfiguration["Goal"]);
            decimal limit = decimal.Parse(pluginConfiguration["Limit"]);

            if (newData == null || !pluginConfiguration.TryGetValue("SlackHook", out var slackHook) || string.IsNullOrWhiteSpace(slackHook))
            {
                return;
            }

            var mood = CalculateMood(newData.Value, limit, goal, wish);
            await SendMessage(CreateMessage(tileId, mood, newData.Value, limit, goal, wish), slackHook, cancellation);
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

        private string CreateMessage(TileId tileId, MessageMood messageMood, decimal currentValue, decimal limit, decimal goal, decimal wish)
        {
            StringBuilder stringBuilder = new StringBuilder();
            Random random = new Random();
            stringBuilder.Append($"Current Code Coverage for {tileId.Name}: *{currentValue}%* (Limit: {limit}%, Goal: {goal}%, Wish {wish}%) ");
            switch (messageMood)
            {
                case MessageMood.Amazing:
                    stringBuilder.AppendLine($":gem: \n{MessageMoodDictionary.AmazingMessages[random.Next(MessageMoodDictionary.AmazingMessages.Count)]}");
                    break;
                case MessageMood.Good:
                    stringBuilder.AppendLine($":green_heart: \n{MessageMoodDictionary.GoodMessages[random.Next(MessageMoodDictionary.GoodMessages.Count)]}");
                    break;
                case MessageMood.CouldBeBetter:
                    stringBuilder.AppendLine($":large_yellow_circle: \n{MessageMoodDictionary.CouldBeBetterMessages[random.Next(MessageMoodDictionary.CouldBeBetterMessages.Count)]}");
                    break;
                case MessageMood.Bad:
                    stringBuilder.AppendLine($":red_circle: \n{MessageMoodDictionary.BadMessages[random.Next(MessageMoodDictionary.BadMessages.Count)]}");
                    break;
            }

            return stringBuilder.ToString();
        }

        private MessageMood CalculateMood(decimal currentValue, decimal limit, decimal? goal, decimal? wish)
        {
            if (wish.HasValue && currentValue >= wish) return MessageMood.Amazing;
            if (goal.HasValue && currentValue >= goal) return MessageMood.Good;
            if (currentValue >= limit) return MessageMood.CouldBeBetter;
            return MessageMood.Bad;
        }
    }
}
