using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using TilesDashboard.Core.Type.Enums;

namespace TilesDashboard.Core.Domain.Entities
{
    public class MetricConfiguration
    {
        public decimal Limit { get; set; }

        public decimal? Wish { get; set; }

        public decimal? Goal { get; set; }

        public string Unit { get; set; }

        public bool LowerIsBetter { get; set; }

        [BsonRepresentation(BsonType.String)]
        public MetricType MetricType { get; set; }
    }
}