using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using TilesDashboard.PluginBase;
using TilesDashboard.PluginSystem.Entities;
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
                cm.MapProperty(c => c.PluginType).SetSerializer(new EnumSerializer<PluginType>(BsonType.String));
            });

            BsonClassMap.RegisterClassMap<PluginTileConfig>(cm =>
            {
                cm.AutoMap();
                cm.SetIgnoreExtraElements(true);
                cm.MapProperty(c => c.TileStorageId).SetSerializer(new StringSerializer(BsonType.ObjectId));
            });

            BsonClassMap.RegisterClassMap<NotificationPluginTileConfig>(cm =>
            {
                cm.AutoMap();
                cm.SetIgnoreExtraElements(true);
                cm.MapProperty(c => c.TileStorageId).SetSerializer(new StringSerializer(BsonType.ObjectId));
            });
        }
    }
}
