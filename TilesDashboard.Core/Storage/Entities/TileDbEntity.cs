using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TilesDashboard.Core.Storage.Entities
{
    public class TileDbEntity
    {
        [BsonId]
        public TileId Id { get; set; }

        public IList<BsonDocument> Data { get; private set; }

        public BsonDocument Configuration { get; set; }

        public string Group { get; set; }
    }
}
