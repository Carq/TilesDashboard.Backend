using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using TilesDashboard.Plugin.Azure.Dtos;

namespace TilesDashboard.Plugin.Azure
{
    public class AzureDevOpsHelper
    {
        private string _organization;

        private string _project;

        private string _buildDefinition;

        private string _personalAccessToken;

        public AzureDevOpsHelper(string organization, string project, string buildDefinition, string personalAccessToken)
        {
            _organization = ValidateParameter(organization, nameof(organization));
            _project = ValidateParameter(project, nameof(project));
            _buildDefinition = ValidateParameter(buildDefinition, nameof(buildDefinition));
            _personalAccessToken = ValidateParameter(personalAccessToken, nameof(personalAccessToken));
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

            var lastBuild = JsonSerializer.Deserialize<BuildListDto>(await buildListResponse.Content.ReadAsStringAsync())?.Value.FirstOrDefault();
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

        public async Task<BuildListDto> GetGreenBuildsAsync(DateTime from, DateTime to, CancellationToken cancellationToken)
        {
            var httpClient = new HttpClient();
            var autheticationHeader = Basic(_personalAccessToken);
            var fromString = from.ToString("yyyy-MM-dd");
            var toString = to.ToString("yyyy-MM-dd");

            var buildListHttpRequest = new HttpRequestMessage(HttpMethod.Get, $"https://dev.azure.com/{_organization}/{_project}/_apis/build/builds?definitions={_buildDefinition}&minTime={fromString}&maxTime={toString}&api-version=5.1&resultFilter=succeeded");
            buildListHttpRequest.Headers.Authorization = autheticationHeader;

            var respose = await httpClient.SendAsync(buildListHttpRequest, cancellationToken);
            if (!respose.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Request for Azure Build List has fail, Http Response Code: {respose.StatusCode}.");
            }

            return JsonSerializer.Deserialize<BuildListDto>(await respose.Content.ReadAsStringAsync());
        }

        private static string ValidateParameter(string organization, string parameterName)
        {
            return string.IsNullOrWhiteSpace(organization) ? throw new ArgumentException($"Config entry '{parameterName}' is empty, please verify plugin configuration.") : organization;
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
