using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using TilesDashboard.Plugin.Azure.CodeCoverage.Dtos;
using TilesDashboard.PluginBase;

namespace TilesDashboard.Plugin.Azure.CodeCoverage
{
    public class AzureDevOpsHelper
    {
        private string _organization;

        private string _project;

        private string _buildDefinition;

        private string _personalAccessToken;

        public AzureDevOpsHelper(IPluginConfigProvider pluginConfigProvider, string rootConfig)
        {
            _organization = pluginConfigProvider.GetConfigEntry($"{rootConfig}:Organization");
            _project = pluginConfigProvider.GetConfigEntry($"{rootConfig}:Project");
            _personalAccessToken = pluginConfigProvider.GetConfigEntry($"{rootConfig}:PersonalAccessToken");
            _buildDefinition = pluginConfigProvider.GetConfigEntry($"{rootConfig}:BuildDefinition");
        }

        public async Task<CodeCoverageDto> GetCodeCoverageResultForLastGreenBuildAsync(CancellationToken cancellationToken)
        {
            var httpClient = new HttpClient();
            var autheticationHeader = Basic(_personalAccessToken);
            var buildListResponse = await GetBuildListHttpResponse(httpClient, autheticationHeader, cancellationToken);
            if (!buildListResponse.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Request for Azure Build List has fail, Http Response Code: {buildListResponse.StatusCode}.");
            }

            var lastBuild = JsonSerializer.Deserialize<BuildListDto>(await buildListResponse.Content.ReadAsStringAsync()).Value.FirstOrDefault();
            if (lastBuild == null || lastBuild.StartTime.Date != DateTimeOffset.Now.Date)
            {
                return null;
            }

            var codeCoverageResponse = await GetCodeCoverageHttpResponse(lastBuild.Id, httpClient, autheticationHeader, cancellationToken);
            if (!codeCoverageResponse.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Request for Azure Build Code Coverage details has fail, Http Response Code: {codeCoverageResponse.StatusCode}.");
            }

            return JsonSerializer.Deserialize<CodeCoverageDto>(await codeCoverageResponse.Content.ReadAsStringAsync());
        }


        private async Task<HttpResponseMessage> GetBuildListHttpResponse(HttpClient httpClient, AuthenticationHeaderValue autheticationHeader, CancellationToken cancellationToken)
        {
            var buildListHttpRequest = new HttpRequestMessage(HttpMethod.Get, $"https://dev.azure.com/{_organization}/{_project}/_apis/build/builds?$top=1&definitions={_buildDefinition}&api-version=5.1&resultFilter=succeeded");
            buildListHttpRequest.Headers.Authorization = autheticationHeader;

            return await httpClient.SendAsync(buildListHttpRequest, cancellationToken);
        }

        private async Task<HttpResponseMessage> GetCodeCoverageHttpResponse(int lastBuildId, HttpClient httpClient, AuthenticationHeaderValue autheticationHeader, CancellationToken cancellationToken)
        {
            using var codeCoverageHttpRequest = new HttpRequestMessage(HttpMethod.Get, $"https://dev.azure.com/{_organization}/{_project}/_apis/test/codecoverage?api-version=5.1-preview.1&buildId={lastBuildId}");
            codeCoverageHttpRequest.Headers.Authorization = autheticationHeader;

            return await httpClient.SendAsync(codeCoverageHttpRequest, cancellationToken);
        }

        private static AuthenticationHeaderValue Basic(string personalKey)
        {
            var basic = Convert.ToBase64String(Encoding.ASCII.GetBytes(":" + personalKey));
            return new AuthenticationHeaderValue("Basic", basic);
        }
    }
}
