using MongoDB.Bson.Serialization.Attributes;

namespace MetricsDashboard.Core.Entities
{
    public class Metric<TValue> : MetricBase
    {
        [BsonElement("value")]
        public TValue Value { get; set; }

        public override object MetricValue => Value;
    }
}
