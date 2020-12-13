using System;
using System.Threading.Tasks;
using TilesDashboard.Handy.Tools;
using TilesDashboard.V2.Core.Entities;
using TilesDashboard.V2.Core.Entities.Enums;
using TilesDashboard.V2.Core.Entities.Metric;
using TilesDashboard.V2.Core.Repositories;

namespace TilesDashboard.V2.Core.Services
{
    public class MetricService : TileBaseService, IMetricService
    {
        public MetricService(IDateTimeProvider dateTimeProvider, ITileRepository tileRepository)
            : base(tileRepository, dateTimeProvider)
        {
        }

        public async Task<MetricTile> GetMetricTile(TileId tileId) => await TileRepository.GetTile<MetricTile>(tileId);

        public async Task RecordValue(TileId tileId, MetricType metricType, decimal newValue)
        {
            var metric = await TileRepository.GetTile<MetricTile>(tileId);
            MetricValue newMetricValue = CreateMericValue(metric, newValue);

            await TileRepository.RecordValue(tileId, newMetricValue);
        }

        public async Task RecordValue(StorageId storageId, MetricType metricType, decimal newValue)
        {
            var metric = await TileRepository.GetTile<MetricTile>(storageId, TileType.Metric);
            MetricValue newMetricValue = CreateMericValue(metric, newValue);

            await TileRepository.RecordValue(storageId, newMetricValue, TileType.Metric);
        }

        private MetricValue CreateMericValue(MetricTile metric, decimal newValue)
        {
            MetricValue newMetricValue;
            switch (metric.MetricType)
            {
                case MetricType.Percentage:
                    newMetricValue = new PercentageMetricValue(newValue, DateTimeProvider.Now);
                    break;
                case MetricType.Money:
                    newMetricValue = new MoneyMetricValue(newValue, DateTimeProvider.Now);
                    break;
                case MetricType.Time:
                    newMetricValue = new TimeMetricValue(newValue, DateTimeProvider.Now);
                    break;
                default:
                    throw new NotSupportedException();
            }

            return newMetricValue;
        }
    }
}
