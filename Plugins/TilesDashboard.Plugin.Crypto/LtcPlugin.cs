using System.Globalization;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using TilesDashboard.PluginBase;
using TilesDashboard.PluginBase.IntegerPlugin;
using TilesDashboard.PluginBase.MetricPlugin;

namespace TilesDashboard.Plugin.Crypto
{
    public class LtcPlugin : IntegerPluginBase
    {
        public override string TileName { get; } = "LTC PLN";

        public override string CronSchedule => ConfigProvider.GetConfigEntry($"{RootConfig}:CronSchedule");

        private readonly string RootConfig = "LtcPlugin";

        public LtcPlugin(IPluginConfigProvider pluginConfigProvider) : base(pluginConfigProvider)
        {
        }

        public override async Task<IntegerData> GetDataAsync(CancellationToken cancellationToken = default)
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync($"https://api.bitbay.net/rest/trading/ticker", cancellationToken);
            if (response.IsSuccessStatusCode)
            {
                var responseDto = JsonSerializer.Deserialize<CryptoTickerDto>(await response.Content.ReadAsStringAsync());
                return new IntegerData((int)decimal.Parse(responseDto.Items.LtcPln.Rate, CultureInfo.InvariantCulture), Status.OK);
            }

            return IntegerData.Error($"Code: {response.StatusCode}");
        }
    }
}
