using System;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization.Conventions;
using TilesDashboard.Handy.Extensions;
using TilesDashboard.V2.Core.Entities.Enums;
using TilesDashboard.V2.Core.Entities.HeartBeat;
using TilesDashboard.V2.Core.Entities.Integer;
using TilesDashboard.V2.Core.Entities.Metric;
using TilesDashboard.V2.Core.Entities.Weather;

namespace TilesDashboard.V2.Core.Storage
{
    public class TileDiscriminatorConvention : IDiscriminatorConvention
    {
        public string ElementName => "TileId";

        public Type GetActualType(IBsonReader bsonReader, Type nominalType)
        {
            TileType tileType;
            var bookmark = bsonReader.GetBookmark();
            bsonReader.ReadStartDocument();
            if (bsonReader.FindElement(ElementName))
            {
                bsonReader.ReadStartDocument();
                tileType = bsonReader.FindStringElement("Type").Convert<TileType>();
            }
            else
            {
                throw new NotSupportedException();
            }

            bsonReader.ReturnToBookmark(bookmark);
            return GetTileType(tileType);
        }

        public MongoDB.Bson.BsonValue GetDiscriminator(Type nominalType, Type actualType)
        {
            return actualType.Name;
        }

        private static Type GetTileType(TileType tileType)
        {
            return tileType switch
            {
                TileType.Metric => typeof(MetricTile),
                TileType.Weather => typeof(WeatherTile),
                TileType.Integer => typeof(IntegerTile),
                TileType.HeartBeat => typeof(HeartBeatTile),
                TileType.Undefined => throw new NotSupportedException(),
                _ => throw new NotSupportedException()
            };
        }
    }
}
