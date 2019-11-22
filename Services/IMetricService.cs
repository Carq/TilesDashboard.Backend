using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MetricsDashboard.WebApi.Entities;

namespace MetricsDashboard.WebApi.Services
{
    public interface IMetricService
    {
        Task<IList<Metric>> GetAllMetricsAsync(CancellationToken cancellationToken);

        Task SaveValueAsync(int metricId, int value, DateTimeOffset? date, CancellationToken token);
    }
}