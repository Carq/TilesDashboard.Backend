using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MetricsDashboard.Contract;
using MetricsDashboard.Core.Interfaces;

namespace MetricsDashboard.WebApi.Services
{
    public class MetricsService : IMetricsService
    {
        private readonly IMetricsRepository _repository;

        public MetricsService(IMetricsRepository repository)
        {
            _repository = repository;
        }

        public async Task<IList<AvailableMetric>> GetAvailableMetricsAsync(CancellationToken cancellationToken)
        {
            var availableMetrics = await _repository.GetAvailableMetricsAsync(cancellationToken);

            return availableMetrics.Select(am => new AvailableMetric
            {
                Name = am.MetricName,
                Kind = (MetricKind)Enum.Parse(typeof(MetricKind), am.MetricKind, true),
            }).ToList();
        }

        public async Task<MetricDto> GetLatestAsync(MetricKind metricKind, string metricName, CancellationToken cancellationToken)
        {
            var latest = await _repository.GetLatestAsync(metricKind, metricName, cancellationToken);
            return latest != null
                ? new MetricDto
                {
                    AddedOn = latest.AddedOn,
                    Value = latest.MetricValue,
                }
                : null;
        }
    }
}
