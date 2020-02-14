using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MetricsDashboard.DataAccess.Entities;
using MetricsDashboard.DataAccess.Interfaces;
using MetricsDashboard.DataAccess.Utils;
using MetricsDashboard.Dto;
using MetricsDashboard.Dto.Attributes;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace MetricsDashboard.DataAccess
{
    public class MetricsRepository : IMetricsRepository
    {
        private const string MetricsCollectionName = "Metrics";

        private readonly IDictionary<MetricKind, Type> _metricKinds;

        private readonly IMongoCollection<BsonDocument> _metricsCollection;

        public MetricsRepository(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _metricsCollection = database.GetCollection<BsonDocument>(MetricsCollectionName);

            var metricKinds = (MetricKind[])Enum.GetValues(typeof(MetricKind));
            _metricKinds = metricKinds.ToDictionary(
                mk => mk,
                mk => mk.GetAttributeOfType<MetricKindTypeAttribute>().ValueType);
        }

        public async Task<IEnumerable<MetricType>> GetAvailableMetricsAsync(CancellationToken cancellationToken)
        {
            var grouped = await _metricsCollection.Aggregate().Group(new BsonDocument { { "_id", new BsonDocument { { "kind", "$metricKind" }, { "name", "$metricName" } } } })
                .As<Group<MetricType>>()
                .ToListAsync(cancellationToken);

            return grouped.Select(g => g.Id);
        }

        public async Task<Entities.Metric> GetLatestAsync(MetricKind metricKind, string metricName, CancellationToken cancellationToken)
        {
            if (!_metricKinds.TryGetValue(metricKind, out var valueType))
            {
                return null;
            }

            var result = await _metricsCollection
                .Find(Builders<BsonDocument>.Filter.And(
                    Builders<BsonDocument>.Filter.Eq("metricKind", metricKind.ToString().ToLowerInvariant()),
                    Builders<BsonDocument>.Filter.Eq("metricName", metricName)))
                .Sort(Builders<BsonDocument>.Sort.Descending("addedOn"))
                .Limit(1)
                .ToListAsync(cancellationToken);

            // TODO: catch serialization exception
            var metricType = typeof(Metric<>).MakeGenericType(valueType);
            return result.Select(metric => (Entities.Metric)BsonSerializer.Deserialize(metric, metricType)).FirstOrDefault();
        }
    }
}