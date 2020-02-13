using MongoDB.Bson.Serialization.Attributes;

namespace MetricsDashboard.DataAccess.Entities
{
    public class Group<TKey>
    {
        [BsonId]
        public TKey Id { get; set; }
    }
}