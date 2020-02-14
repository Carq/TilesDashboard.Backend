using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MetricsDashboard.Contract;

namespace MetricsDashboard.WebApi.Services
{
    public interface IMetricsService
    {
        Task<IList<AvailableMetric>> GetAvailableMetricsAsync(CancellationToken cancellationToken);

        Task<MetricDto> GetLatestAsync(MetricKind metricKind, string metricName, CancellationToken cancellationToken);
    }
}