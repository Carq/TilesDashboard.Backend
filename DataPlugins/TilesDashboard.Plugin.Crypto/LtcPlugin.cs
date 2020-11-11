using System.Globalization;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using TilesDashboard.PluginBase;
using TilesDashboard.PluginBase.Data.MetricPlugin;
using TilesDashboard.V2.Core.Entities.Enums;

namespace TilesDashboard.Plugin.Crypto
{
    public class LtcPlugin : MetricPluginBase
    {
        public override string TileName { get; } = "LTC PLN";

        public override string CronSchedule => ConfigProvider.GetConfigEntry($"{RootConfig}:CronSchedule");

        private readonly string RootConfig = "LtcPlugin";

        public LtcPlugin(IPluginConfigProvider pluginConfigProvider) : base(pluginConfigProvider)
        {
        }

        public override async Task<MetricData> GetDataAsync(CancellationToken cancellationToken = default)
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync($"https://api.bitbay.net/rest/trading/ticker", cancellationToken);
            if (response.IsSuccessStatusCode)
            {
                var responseDto = JsonSerializer.Deserialize<CryptoTickerDto>(await response.Content.ReadAsStringAsync());
                return new MetricData(decimal.Parse(responseDto.Items.LtcPln.Rate, CultureInfo.InvariantCulture), MetricType.Money, Status.OK);
            }

            return MetricData.Error($"Code: {response.StatusCode}");
        }
    }
}
