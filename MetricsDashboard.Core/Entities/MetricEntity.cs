using System;
using MongoDB.Bson;

namespace MetricsDashboard.Core.Entities
{
    public class MetricEntity<TValue>
    {
        public ObjectId Id { get; set; }

        public ObjectId TileId { get; set; }

        public TValue Value { get; set; }

        public DateTimeOffset AddedOn { get; set; }
    }
}