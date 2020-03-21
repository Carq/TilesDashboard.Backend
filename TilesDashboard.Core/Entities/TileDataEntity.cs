using System;
using MongoDB.Bson;

namespace TilesDashboard.Core.Entities
{
    public class TileDataEntity<TValue>
    {
        public ObjectId Id { get; set; }

        public ObjectId TileId { get; set; }

        public TValue Value { get; set; }

        public DateTimeOffset AddedOn { get; set; }
    }
}