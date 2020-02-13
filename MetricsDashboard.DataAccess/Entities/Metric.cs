using System;
using MetricsDashboard.Dto;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MetricsDashboard.DataAccess.Entities
{
    public class Metric<TValue> : IMetric
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("metricName")]
        public string MetricName { get; set; }

        [BsonElement("metricKind")]
        public string MetricKind { get; set; }

        [BsonElement("addedOn")]
        public DateTimeOffset AddedOn { get; set; }

        [BsonElement("value")]
        public TValue Value { get; set; }

        public Metric ToDto() => new Metric { AddedOn = AddedOn, Value = Value };
    }
}
