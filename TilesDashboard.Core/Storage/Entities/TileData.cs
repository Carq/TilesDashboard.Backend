using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TilesDashboard.Core.Storage.Entities
{
    public abstract class TileData
    {
        protected TileData(DateTimeOffset addedOn)
        {
            AddedOn = addedOn;
        }

        [BsonRepresentation(BsonType.String)]
        public DateTimeOffset AddedOn { get; private set; }
    }
}