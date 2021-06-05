using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using TilesDashboard.Plugin.Crypto.Dto;
using TilesDashboard.PluginBase.Data;
using TilesDashboard.PluginBase.Data.MetricPlugin;
using TilesDashboard.V2.Core.Entities.Metric;

namespace TilesDashboard.Plugin.Crypto
{
    public class LtcMetricPlugin : MetricDataPlugin
    {
        public override string UniquePluginName => $"TileCorePlugins.{nameof(LtcMetricPlugin)}";

        public override async Task<MetricData> GetTileValueAsync(IDictionary<string, string> pluginConfiguration, CancellationToken cancellation = default)
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync($"https://api.bitbay.net/rest/trading/ticker", cancellation);
            if (response.IsSuccessStatusCode)
            {
                var responseDto = JsonSerializer.Deserialize<CryptoTickerDto>(await response.Content.ReadAsStringAsync());
                return new MetricData(decimal.Parse(responseDto.Items.LtcPln.Rate, CultureInfo.InvariantCulture), MetricType.Money, Status.OK);
            }

            return MetricData.Error($"Code: {response.StatusCode}");
        }
    }
}
