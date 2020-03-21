using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using TilesDashboard.Core.Storage.Entities;

namespace TilesDashboard.Core.Entities
{
    public class TileDbEntity<TData>
        where TData : TileData
    {
        [BsonId]
        public TileId Id { get; set; }

        public IList<TData> Data { get; private set; }
    }
}
