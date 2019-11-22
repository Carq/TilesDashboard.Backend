using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MetricsDashboard.WebApi.Database;
using MetricsDashboard.WebApi.Entities;
using MetricsDashboard.WebApi.Exceptions;
using MetricsDashboard.WebApi.Tools;
using Microsoft.EntityFrameworkCore;

namespace MetricsDashboard.WebApi.Services
{
    public class MetricService : IMetricService
    {
        private readonly MetricsDbContext _dbContext;

        private readonly IDateTimeOffsetProvider _dateTimeOffsetProvider;

        public MetricService(MetricsDbContext dbContext, IDateTimeOffsetProvider dateTimeOffsetProvider)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _dateTimeOffsetProvider = dateTimeOffsetProvider ?? throw new ArgumentNullException(nameof(dateTimeOffsetProvider));
        }

        public async Task<IList<Metric>> GetAllMetricsAsync(CancellationToken cancellationToken)
        {
            return await _dbContext.Metrics.ToArrayAsync(cancellationToken);
        }

        public async Task SaveValueAsync(int metricId, int value, DateTimeOffset? date, CancellationToken token)
        {
            var metric = await _dbContext.Metrics.Include(x => x.History).SingleOrDefaultAsync(x => x.Id == metricId, token);
            if (metric == null)
            {
                throw new NotFoundException($"Metric with Id {metricId} does not exists");
            }

            metric.SaveValue(value, date ?? _dateTimeOffsetProvider.Now);
            await _dbContext.SaveChangesAsync(token);
        }
    }
}
