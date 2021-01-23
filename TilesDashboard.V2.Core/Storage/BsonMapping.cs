using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Options;
using MongoDB.Bson.Serialization.Serializers;
using TilesDashboard.V2.Core.Entities;
using TilesDashboard.V2.Core.Entities.Dual;
using TilesDashboard.V2.Core.Entities.Enums;
using TilesDashboard.V2.Core.Entities.HeartBeat;
using TilesDashboard.V2.Core.Entities.Integer;
using TilesDashboard.V2.Core.Entities.Metric;
using TilesDashboard.V2.Core.Entities.Weather;

namespace TilesDashboard.V2.Core.Storage
{
    public static class BsonMapping
    {
        /// <summary>
        /// https://mongodb.github.io/mongo-csharp-driver/2.11/reference/bson/mapping/.
        /// </summary>
        public static void RegisterMappings()
        {
            TileEntityMapping();
            TileDataMapping();
        }

        private static void TileEntityMapping()
        {
            BsonClassMap.RegisterClassMap<TileId>(cm =>
            {
                cm.AutoMap();
                cm.MapProperty(c => c.Type).SetSerializer(new EnumSerializer<TileType>(BsonType.String));
            });

            BsonClassMap.RegisterClassMap<TileEntity>(cm =>
            {
                cm.AutoMap();
                cm.SetIsRootClass(true);
                cm.MapIdMember(x => x.Id).SetSerializer(new StringSerializer(BsonType.ObjectId));
                cm.MapProperty("TileConfiguration")
                                    .SetSerializer(new DictionaryInterfaceImplementerSerializer<Dictionary<string, object>>(DictionaryRepresentation.Document));
            });

            BsonSerializer.RegisterDiscriminatorConvention(typeof(TileEntity), new TileDiscriminatorConvention());
            BsonClassMap.RegisterClassMap<MetricTile>();
            BsonClassMap.RegisterClassMap<WeatherTile>();
            BsonClassMap.RegisterClassMap<IntegerTile>();
            BsonClassMap.RegisterClassMap<HeartBeatTile>();
            BsonClassMap.RegisterClassMap<DualTile>();
        }

        private static void TileDataMapping()
        {
            BsonClassMap.RegisterClassMap<TileDataContainer>(cm =>
            {
                cm.AutoMap();
                cm.SetIsRootClass(true);
                cm.SetIgnoreExtraElements(true);
                cm.MapProperty(c => c.Type).SetSerializer(new EnumSerializer<TileType>(BsonType.String));
            });

            BsonClassMap.RegisterClassMap<TileValue>(cm =>
            {
                cm.AutoMap();
                cm.MapProperty(x => x.AddedOn).SetSerializer(new DateTimeOffsetSerializer(BsonType.String));
            });

            BsonClassMap.RegisterClassMap<WeatherValue>(cm =>
            {
                cm.AutoMap();
                cm.MapProperty(x => x.Temperature).SetSerializer(new DecimalSerializer(BsonType.Decimal128));
                cm.MapProperty(x => x.Humidity).SetSerializer(new DecimalSerializer(BsonType.Decimal128));
            });

            BsonClassMap.RegisterClassMap<MetricValue>(cm =>
            {
                cm.AutoMap();
                cm.SetIsRootClass(true);
                cm.MapProperty(x => x.Value).SetSerializer(new DecimalSerializer(BsonType.Decimal128));
            });

            BsonClassMap.RegisterClassMap<PercentageMetricValue>();
            BsonClassMap.RegisterClassMap<TimeMetricValue>();
            BsonClassMap.RegisterClassMap<MoneyMetricValue>();
            BsonClassMap.RegisterClassMap<IntegerValue>();
            BsonClassMap.RegisterClassMap<HeartBeatValue>();
            BsonClassMap.RegisterClassMap<DualValue>();
        }
    }
}
