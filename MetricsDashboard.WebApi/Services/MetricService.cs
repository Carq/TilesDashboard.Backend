using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MetricsDashboard.Core.Exceptions;
using MetricsDashboard.Core.Tools;
using MetricsDashboard.WebApi.Database;
using MetricsDashboard.WebApi.Dtos;
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

        public async Task<IList<MetricData>> GetAllMetricsAsync(CancellationToken cancellationToken)
        {
            return await _dbContext.Metrics.Select(x =>
                    new MetricData(x.Id, x.Name, x.History.OrderByDescending(y => y.AddedOn).First().Value, x.Limit, x.Goal, x.Wish, x.History.OrderByDescending(y => y.AddedOn).First().AddedOn, x.Type))
                    .ToListAsync(cancellationToken);
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
