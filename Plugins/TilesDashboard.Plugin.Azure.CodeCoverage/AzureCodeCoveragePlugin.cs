using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TilesDashboard.Plugin.Azure.CodeCoverage.Dtos;
using TilesDashboard.PluginBase;
using TilesDashboard.PluginBase.MetricPlugin;

namespace TilesDashboard.Plugin.Azure.CodeCoverage
{
    public class AzureCodeCoveragePlugin : MetricPluginBase
    {
        private readonly HttpClient _httpClient;

        private string _organization;

        private string _project;

        private string _tileName;

        private string _buildDefinition;

        private string _personalAccessToken;

        public AzureCodeCoveragePlugin(IPluginConfigProvider configProvider) 
            : base(configProvider)
        {
            _httpClient = new HttpClient();
        }

        public override string TileName => _tileName;

        public override Task InitializeAsync()
        {
            _organization = ConfigProvider.GetConfigEntry("AzureCodeCoveragePlugin:Organization");
            _project = ConfigProvider.GetConfigEntry("AzureCodeCoveragePlugin:Project");
            _tileName = ConfigProvider.GetConfigEntry("AzureCodeCoveragePlugin:TileName");
            _buildDefinition = ConfigProvider.GetConfigEntry("AzureCodeCoveragePlugin:BuildDefinition");
            _personalAccessToken = ConfigProvider.GetConfigEntry("AzureCodeCoveragePlugin:PersonalAccessToken");

            return Task.CompletedTask;
        }

        public override async Task<MetricData> GetDataAsync()
        {
            var autheticationHeader = Basic(_personalAccessToken);
            
            var buildListResponse = await GetBuildListHttpResponse(autheticationHeader);
            if (!buildListResponse.IsSuccessStatusCode)
            {
                return MetricData.Error($"Code: {buildListResponse.StatusCode}");
            }

            var lastBuildId = JsonSerializer.Deserialize<BuildListDto>(await buildListResponse.Content.ReadAsStringAsync()).Value.First().Id;

            var codeCoverageResponse = await GetCodeCoverageHttpResponse(lastBuildId, autheticationHeader);
            if (!codeCoverageResponse.IsSuccessStatusCode)
            {
                return MetricData.Error($"Code: {codeCoverageResponse.StatusCode}");
            }

            var codeCoverage = JsonSerializer.Deserialize<CodeCoverageDto>(await codeCoverageResponse.Content.ReadAsStringAsync());
            var linesCoverageData = codeCoverage.CoverageData.First().CoverageStats.Single(x => x.Label == "Lines");
            var percentageCoverage = Math.Round(100m * linesCoverageData.Covered / linesCoverageData.Total, 1);

            return new MetricData(percentageCoverage, Status.OK);
        }

        private async Task<HttpResponseMessage> GetBuildListHttpResponse(AuthenticationHeaderValue autheticationHeader)
        {
            var buildListHttpRequest = new HttpRequestMessage(HttpMethod.Get, $"https://dev.azure.com/{_organization}/{_project}/_apis/build/builds?$top=1&definitions={_buildDefinition}&api-version=5.1&resultFilter=succeeded");
            buildListHttpRequest.Headers.Authorization = autheticationHeader;

            return await _httpClient.SendAsync(buildListHttpRequest);
        }

         private async Task<HttpResponseMessage> GetCodeCoverageHttpResponse(int lastBuildId, AuthenticationHeaderValue autheticationHeader)
        {
             using var codeCoverageHttpRequest = new HttpRequestMessage(HttpMethod.Get, $"https://dev.azure.com/sporttecag/sporttec/_apis/test/codecoverage?api-version=5.1-preview.1&buildId={lastBuildId}");
            codeCoverageHttpRequest.Headers.Authorization = autheticationHeader;

            return await _httpClient.SendAsync(codeCoverageHttpRequest);
        }

        public static AuthenticationHeaderValue Basic(string personalKey)
        {
            var basic = Convert.ToBase64String(Encoding.ASCII.GetBytes(":" + personalKey));
            return new AuthenticationHeaderValue("Basic", basic);
        }

    }
}
