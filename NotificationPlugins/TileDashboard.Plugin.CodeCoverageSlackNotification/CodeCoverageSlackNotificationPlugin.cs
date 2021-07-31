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

namespace TilesDashboard.Plugin.CodeCoverageSlackNotification
{
    public class CodeCoverageSlackNotificationPlugin : MetricNotificationPlugin
    {
        public override string UniquePluginName => $"TileCorePlugins.{nameof(CodeCoverageSlackNotificationPlugin)}";

        public override TileType TileType => TileType.Metric;

        public override async Task PerformNotificationAsync(TileId tileId, MetricValue newData, IReadOnlyDictionary<string, string> pluginConfiguration, IReadOnlyDictionary<string, string> tileConfiguration, CancellationToken cancellation = default)
        {
            decimal wish = decimal.Parse(tileConfiguration["Wish"]);
            decimal goal = decimal.Parse(tileConfiguration["Goal"]);
            decimal limit = decimal.Parse(tileConfiguration["Limit"]);

            var oldData = await TileDataProvider.GetLast5Data(cancellation);

            if (newData == null || !pluginConfiguration.TryGetValue("SlackHook", out var slackHook) || string.IsNullOrWhiteSpace(slackHook))
            {
                return;
            }

            var mood = CalculateMood(newData.Value, limit, goal, wish);
            await SendMessage(CreateMessage(tileId, mood, newData.Value, oldData[1].Value, limit, goal, wish), slackHook, cancellation);
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

        private string CreateMessage(TileId tileId, MessageMood messageMood, decimal currentValue, decimal previousValue, decimal limit, decimal goal, decimal wish)
        {
            StringBuilder stringBuilder = new StringBuilder();
            Random random = new Random();
            var diff = currentValue - previousValue;
            var diffIcon = diff > 0m ? ":arrow_upper_right:" : diff < 0m ? ":arrow_lower_right:" : string.Empty;
            stringBuilder.Append($"Code Coverage for `{tileId.Name}` *{currentValue}%* ({diffIcon} `{diff}%`)");
            stringBuilder.Append($"\n[Limit: {limit}%, Goal: {goal}%, Wish {wish}%] ");
            switch (messageMood)
            {
                case MessageMood.Amazing:
                    stringBuilder.Append($":gem:\n{MessageMoodDictionary.AmazingMessages[random.Next(MessageMoodDictionary.AmazingMessages.Count)]}");
                    break;
                case MessageMood.Good:
                    stringBuilder.Append($":green_heart:\n{MessageMoodDictionary.GoodMessages[random.Next(MessageMoodDictionary.GoodMessages.Count)]}");
                    break;
                case MessageMood.CouldBeBetter:
                    stringBuilder.Append($":large_yellow_circle:\n{MessageMoodDictionary.CouldBeBetterMessages[random.Next(MessageMoodDictionary.CouldBeBetterMessages.Count)]}");
                    break;
                case MessageMood.Bad:
                    stringBuilder.Append($":red_circle:\n{MessageMoodDictionary.BadMessages[random.Next(MessageMoodDictionary.BadMessages.Count)]}");
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
