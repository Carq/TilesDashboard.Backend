using System.Threading;
using System.Threading.Tasks;
using MetricsDashboard.Contract;
using MetricsDashboard.Core;
using Microsoft.AspNetCore.Mvc;

namespace MetricsDashboard.WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TileDataController : ControllerBase
    {
        private readonly ITileDataService _tileDataService;

        public TileDataController(ITileDataService tileDataService)
        {
            _tileDataService = tileDataService;
        }

        [HttpPost("metric")]
        public async Task SaveMetric(SaveValueDto<decimal> saveValueDto, CancellationToken cancellationToken)
        {
            await _tileDataService.SaveMetricAsync(saveValueDto, cancellationToken);
        }

        [HttpPost("boolean")]
        public async Task SaveBoolean(SaveValueDto<bool> saveValueDto, CancellationToken cancellationToken)
        {
            await _tileDataService.SaveBooleanAsync(saveValueDto, cancellationToken);
        }
    }
}