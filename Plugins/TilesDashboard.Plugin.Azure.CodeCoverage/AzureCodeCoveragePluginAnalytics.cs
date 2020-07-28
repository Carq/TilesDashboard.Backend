using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using TilesDashboard.Core.Type;
using TilesDashboard.Plugin.Azure.CodeCoverage.Dtos;
using TilesDashboard.PluginBase;
using TilesDashboard.PluginBase.MetricPlugin;

namespace TilesDashboard.Plugin.Azure.CodeCoverage
{
    public class AzureCodeCoveragePluginAnalytics : MetricPluginBase
    {
        private string _tileName;

        private string _cronSchedule;

        private string _organization;

        private string _project;

        private string _buildDefinition;

        private string _personalAccessToken;

        private readonly string RootConfig = "AzureCodeCoveragePluginAnalytics";

        public AzureCodeCoveragePluginAnalytics(IPluginConfigProvider configProvider)
            : base(configProvider)
        {

        }

        public override string TileName => _tileName;

        public override string CronSchedule => _cronSchedule;

        public override Task InitializeAsync()
        {
            _organization = ConfigProvider.GetConfigEntry($"{RootConfig}:Organization");
            _project = ConfigProvider.GetConfigEntry($"{RootConfig}:Project");
            _tileName = ConfigProvider.GetConfigEntry($"{RootConfig}:TileName");
            _buildDefinition = ConfigProvider.GetConfigEntry($"{RootConfig}:BuildDefinition");
            _personalAccessToken = ConfigProvider.GetConfigEntry($"{RootConfig}:PersonalAccessToken");
            _cronSchedule = ConfigProvider.GetConfigEntry($"{RootConfig}:CronSchedule");

            return Task.CompletedTask;
        }

        public override async Task<MetricData> GetDataAsync(CancellationToken cancellationToken)
        {
            var httpClient = new HttpClient();
            var autheticationHeader = Basic(_personalAccessToken);
            var buildListResponse = await GetBuildListHttpResponse(httpClient, autheticationHeader, cancellationToken);
            if (!buildListResponse.IsSuccessStatusCode)
            {
                return MetricData.Error($"Code: {buildListResponse.StatusCode}");
            }

            var lastBuildId = JsonSerializer.Deserialize<BuildListDto>(await buildListResponse.Content.ReadAsStringAsync()).Value.First().Id;

            var codeCoverageResponse = await GetCodeCoverageHttpResponse(lastBuildId, httpClient, autheticationHeader, cancellationToken);
            if (!codeCoverageResponse.IsSuccessStatusCode)
            {
                return MetricData.Error($"Code: {codeCoverageResponse.StatusCode}");
            }

            var codeCoverage = JsonSerializer.Deserialize<CodeCoverageDto>(await codeCoverageResponse.Content.ReadAsStringAsync());
            var linesCoverageData = codeCoverage.CoverageData.First().CoverageStats.Single(x => x.Label == "Lines");
            var percentageCoverage = Math.Round(100m * linesCoverageData.Covered / linesCoverageData.Total, 1);

            return new MetricData(percentageCoverage, MetricType.Percentage, Status.OK);
        }

        private async Task<HttpResponseMessage> GetBuildListHttpResponse(HttpClient httpClient, AuthenticationHeaderValue autheticationHeader, CancellationToken cancellationToken)
        {
            var buildListHttpRequest = new HttpRequestMessage(HttpMethod.Get, $"https://dev.azure.com/{_organization}/{_project}/_apis/build/builds?$top=1&definitions={_buildDefinition}&api-version=5.1&resultFilter=succeeded");
            buildListHttpRequest.Headers.Authorization = autheticationHeader;

            return await httpClient.SendAsync(buildListHttpRequest, cancellationToken);
        }

        private async Task<HttpResponseMessage> GetCodeCoverageHttpResponse(int lastBuildId, HttpClient httpClient, AuthenticationHeaderValue autheticationHeader, CancellationToken cancellationToken)
        {
            using var codeCoverageHttpRequest = new HttpRequestMessage(HttpMethod.Get, $"https://dev.azure.com/sporttecag/sporttec/_apis/test/codecoverage?api-version=5.1-preview.1&buildId={lastBuildId}");
            codeCoverageHttpRequest.Headers.Authorization = autheticationHeader;

            return await httpClient.SendAsync(codeCoverageHttpRequest, cancellationToken);
        }

        public static AuthenticationHeaderValue Basic(string personalKey)
        {
            var basic = Convert.ToBase64String(Encoding.ASCII.GetBytes(":" + personalKey));
            return new AuthenticationHeaderValue("Basic", basic);
        }

    }
}
