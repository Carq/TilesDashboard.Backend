using System;
using MetricsDashboard.Dto;
using MongoDB.Bson.Serialization.Attributes;

namespace MetricsDashboard.DataAccess.Entities
{
    public class MetricType
    {
        [BsonElement("name")]
        public string MetricName { get; set; }

        [BsonElement("kind")]
        public string MetricKind { get; set; }

        public AvailableMetric ToDto()
        {
            return new AvailableMetric
            {
                Name = MetricName,
                Kind = (MetricKind)Enum.Parse(typeof(MetricKind), MetricKind, true),
            };
        }
    }
}