using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MetricsDashboard.Contract;
using MetricsDashboard.Core;
using Microsoft.AspNetCore.Mvc;

namespace MetricsDashboard.WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TilesController : ControllerBase
    {
        private readonly ITileService _tileService;

        public TilesController(ITileService tileService)
        {
            _tileService = tileService;
        }

        [HttpGet("")]
        public async Task<IList<TileDto>> GetAll(CancellationToken cancellationToken)
        {
            return await _tileService.GetAllTilesAsync(cancellationToken);
        }
    }
}