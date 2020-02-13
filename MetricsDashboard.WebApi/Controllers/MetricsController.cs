using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MetricsDashboard.DataAccess.Interfaces;
using MetricsDashboard.Dto;
using Microsoft.AspNetCore.Mvc;

namespace MetricsDashboard.WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MetricsController : ControllerBase
    {
        private readonly IMetricsRepository _repository;

        public MetricsController(IMetricsRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<Metric>> Get([FromQuery]MetricKind kind, [FromQuery]string name, CancellationToken cancellationToken)
        {
            var latest = await _repository.GetLatestAsync(kind, name, cancellationToken);
            if (latest == null)
            {
                return NotFound();
            }

            return Ok(latest.ToDto());
        }

        [HttpGet("available")]
        public async Task<IList<AvailableMetric>> GetAvailableMetrics(CancellationToken cancellationToken)
        {
            return (await _repository.GetAvailableMetricsAsync(cancellationToken)).Select(m => m.ToDto()).ToList();
        }
    }
}
