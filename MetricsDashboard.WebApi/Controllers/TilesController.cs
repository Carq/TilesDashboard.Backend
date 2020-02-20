using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MetricsDashboard.Contract;
using MetricsDashboard.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace MetricsDashboard.WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TilesController : ControllerBase
    {
        private readonly ITileService _tileService;

        private readonly ITileDataService _tileDataService;

        public TilesController(ITileService tileService, ITileDataService tileDataService)
        {
            _tileService = tileService;
            _tileDataService = tileDataService;
        }

        [HttpGet("")]
        public async Task<IList<TileDto>> GetAll(CancellationToken cancellationToken)
        {
            return await _tileService.GetAllTilesAsync(cancellationToken);
        }

        [HttpPost("metric")]
        public async Task SaveMetric(SaveValueDto<decimal> saveValueDto, CancellationToken cancellationToken)
        {
            await _tileDataService.SaveMetricAsync(saveValueDto, cancellationToken);
        }

        [HttpPost("status")]
        public async Task SaveStatus(SaveValueDto<bool> saveValueDto, CancellationToken cancellationToken)
        {
            await _tileDataService.SaveStatusAsync(saveValueDto, cancellationToken);
        }
    }
}