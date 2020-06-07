using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using TilesDashboard.Core.Domain.Enums;

namespace TilesDashboard.Core.Domain.Entities
{
    public class MetricConfiguration
    {
        public decimal Limit { get; set; }

        public decimal? Wish { get; set; }

        public decimal? Goal { get; set; }

        [BsonRepresentation(BsonType.String)]
        public MetricType MetricType { get; set; }
    }
}