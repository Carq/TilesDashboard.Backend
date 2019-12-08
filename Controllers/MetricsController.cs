using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MetricsDashboard.WebApi.Dtos;
using MetricsDashboard.WebApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace MetricsDashboard.WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MetricsController : ControllerBase
    {
        private readonly IMetricService _metricService;

        private readonly IMetricReactiveService _metricReactiveService;

        public MetricsController(IMetricService metricService, IMetricReactiveService metricReactiveService)
        {
            _metricService = metricService ?? throw new ArgumentNullException(nameof(metricService));
            _metricReactiveService = metricReactiveService ?? throw new ArgumentNullException(nameof(metricReactiveService));
        }

        [HttpPost("[action]")]
        public async Task SaveValue(SaveValueDto saveValueDto, CancellationToken cancellationToken)
        {
            await _metricService.SaveValueAsync(saveValueDto.MetricId, saveValueDto.Value, saveValueDto.Date, cancellationToken);
        }

        [HttpGet("")]
        public async Task<IList<MetricData>> GetAll(CancellationToken cancellationToken)
        {
            return await _metricService.GetAllMetricsAsync(cancellationToken);
        }

        [HttpGet("{metricId}/history")]
        public async Task<IList<HistoryItem>> History(int metricId, CancellationToken cancellationToken)
        {
            return (await _metricReactiveService.GetMetricHistoryAsync(metricId, cancellationToken)).Select(x => new HistoryItem(x.Value, x.AddedOn.Date.ToShortDateString()))
                .ToList();
        }
    }
}
