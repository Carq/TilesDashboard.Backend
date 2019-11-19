using System;
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

        public MetricsController(IMetricService metricService)
        {
            _metricService = metricService ?? throw new ArgumentNullException(nameof(metricService));
        }

        [HttpPost("[action]")]
        public void SaveValue(SaveValueDto saveValueDto)
        {
            _metricService.SaveValue(saveValueDto.MetricId, saveValueDto.Value);
        }

        [HttpGet("{metricId}/history")]
        public string History(int metricId)
        {
            _metricService.SaveValue(metricId, metricId);
            return "metric " + metricId;
        }
    }
}