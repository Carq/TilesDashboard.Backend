using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using TilesDashboard.Plugin.Crypto.Dto;
using TilesDashboard.PluginBase.Data;
using TilesDashboard.PluginBase.Data.IntegerPlugin;

namespace TilesDashboard.Plugin.Crypto
{
    public class LtcIntegerPlugin : IntegerDataPlugin
    {
        public override string UniquePluginName => $"TileCorePlugins.{nameof(LtcIntegerPlugin)}";

        public override async Task<IntegerData> GetTileValueAsync(IDictionary<string, string> pluginConfiguration, CancellationToken cancellation = default)
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync($"https://api.bitbay.net/rest/trading/ticker", cancellation);
            if (response.IsSuccessStatusCode)
            {
                var responseDto = JsonSerializer.Deserialize<CryptoTickerDto>(await response.Content.ReadAsStringAsync());
                if (responseDto != null)
                {
                    return new IntegerData((int)decimal.Parse(responseDto.Items.LtcPln.Rate, CultureInfo.InvariantCulture), Status.OK);
                }
            }

            return IntegerData.Error($"Code: {response.StatusCode}");
        }
    }
}
