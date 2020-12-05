using System;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization.Conventions;
using TilesDashboard.Handy.Extensions;
using TilesDashboard.V2.Core.Entities.Enums;
using TilesDashboard.V2.Core.Entities.Metric;
using TilesDashboard.V2.Core.Entities.Weather;

namespace TilesDashboard.V2.Core.Storage
{
    public class TileValueDiscriminatorConvention : IDiscriminatorConvention
    {
        public string ElementName => "Type";

        public Type GetActualType(IBsonReader bsonReader, Type nominalType)
        {
            var bookmark = bsonReader.GetBookmark();
            bsonReader.ReadStartDocument();
            TileType tileType = bsonReader.FindStringElement("Type").Convert<TileType>();

            bsonReader.ReturnToBookmark(bookmark);
            return GetTileType(tileType);
        }

        public MongoDB.Bson.BsonValue GetDiscriminator(Type nominalType, Type actualType)
        {
            return null;
        }

        private static Type GetTileType(TileType tileType)
        {
            return tileType switch
                {
                TileType.Metric => typeof(MetricValue),
                TileType.Weather => typeof(WeatherValue),
                TileType.Integer => throw new NotSupportedException(),
                TileType.HeartBeat => throw new NotSupportedException(),
                TileType.Undefined => throw new NotSupportedException(),
                _ => throw new NotSupportedException()
            };
        }
    }
}
