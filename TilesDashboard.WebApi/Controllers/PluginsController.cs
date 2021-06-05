using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TilesDashboard.PluginSystem.Services;
using TilesDashboard.WebApi.Authorization;

namespace TilesDashboard.WebApi.Controllers
{
    [Route("plugins")]
    [ApiController]
    public class PluginsController
    {
        private readonly IDataPluginService _dataPluginService;

        public PluginsController(IDataPluginService dataPluginService)
        {
            _dataPluginService = dataPluginService;
        }

        [HttpPost("{pluginId}/{tileStorageId}")]
        [BearerAuthorization]
        public async Task TriggerPlugin(string pluginName, string tileStorageId, CancellationToken cancellationToken)
        {
            await _dataPluginService.ExecuteDataPluginForTile(pluginName, tileStorageId, cancellationToken);
        }
    }
}
