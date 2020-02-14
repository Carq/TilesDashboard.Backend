using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MetricsDashboard.Dto;
using MetricsDashboard.WebApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace MetricsDashboard.WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MetricsController : ControllerBase
    {
        private readonly IMetricsService _service;

        public MetricsController(IMetricsService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<Metric>> Get([FromQuery]MetricKind kind, [FromQuery]string name, CancellationToken cancellationToken)
        {
            var latest = await _service.GetLatestAsync(kind, name, cancellationToken);
            if (latest == null)
            {
                return NotFound();
            }

            return Ok(latest);
        }

        [HttpGet("available")]
        public async Task<IList<AvailableMetric>> GetAvailableMetrics(CancellationToken cancellationToken)
        {
            return await _service.GetAvailableMetricsAsync(cancellationToken);
        }
    }
}
