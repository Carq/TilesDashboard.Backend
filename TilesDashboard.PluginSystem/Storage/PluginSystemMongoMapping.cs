using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using TilesDashboard.PluginSystem.Entities;
using TilesDashboard.V2.Core.Entities;
using TilesDashboard.V2.Core.Entities.Enums;

namespace TilesDashboard.PluginSystem.Storage
{
    public static class PluginSystemMongoMapping
    {
        /// <summary>
        /// https://mongodb.github.io/mongo-csharp-driver/2.11/reference/bson/mapping/.
        /// </summary>
        public static void RegisterMappings()
        {
            BsonClassMap.RegisterClassMap<PluginConfiguration>(cm =>
            {
                cm.AutoMap();
                cm.SetIgnoreExtraElements(true);
                cm.MapProperty(c => c.TileType).SetSerializer(new EnumSerializer<TileType>(BsonType.String));
            });

            BsonClassMap.RegisterClassMap<PluginTileConfig>(cm =>
          {
              cm.AutoMap();
              cm.SetIgnoreExtraElements(true);
              cm.MapProperty(c => c.TileStorageId).SetSerializer(new StringSerializer(BsonType.ObjectId));
          });
        }
    }
}
