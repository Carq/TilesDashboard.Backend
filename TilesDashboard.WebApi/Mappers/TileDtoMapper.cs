using System.Collections.Generic;
using System.Linq;
using TilesDashboard.Contract;
using TilesDashboard.Core.Domain.Entities;
using TilesDashboard.Core.Storage.Entities;
using TilesDashboard.Core.Type.Enums;
using TilesDashboard.Handy.Extensions;

namespace TilesDashboard.WebApi.Mappers
{
    public static class TileDtoMapper
    {
        public static IList<TileWithCurrentDataDto> Map(IList<GenericTileWithCurrentData> list)
        {
            var result = new List<TileWithCurrentDataDto>();
            foreach (var item in list)
            {
                var tileWithDataDto = new TileWithCurrentDataDto
                {
                    Name = item.Name,
                    Type = item.Type,
                    Configuration = item.Configuration,
                    Group = item.Group != null ? new GroupDto(item.Group.Name, item.Group.Order) : null,
                };

                tileWithDataDto.Data.AddRange(Map(item.Type, item.Data));
                result.Add(tileWithDataDto);
            }

            return result;
        }

        public static IList<object> Map(IList<WeatherData> data)
        {
            return data.Select(MapWeatherData).ToList();
        }

        public static IList<object> Map(IList<MetricData> data)
        {
            return data.Select(MapMetricData).ToList();
        }

        public static IList<object> Map(IList<IntegerData> data)
        {
            return data.Select(MapIntegerData).ToList();
        }

        public static IList<object> Map(IList<HeartBeatData> data)
        {
            return data.Select(MapHeartBeatData).ToList();
        }

        public static IList<object> Map(TileType type, IList<TileData> recentData)
        {
            switch (type)
            {
                case TileType.Metric:
                    return recentData.Select(MapMetricData).ToList();
                case TileType.Weather:
                    return recentData.Select(MapWeatherData).ToList();
                case TileType.Integer:
                    return recentData.Select(MapIntegerData).ToList();
                case TileType.HeartBeat:
                    return recentData.Select(MapHeartBeatData).ToList();
                default:
                    return null;
            }
        }

        private static object MapMetricData(TileData metricData)
        {
            var converted = metricData as MetricData;
            return new
            {
                converted.Value,
                converted.AddedOn,
            };
        }

        private static object MapIntegerData(TileData metricData)
        {
            var converted = metricData as IntegerData;
            return new
            {
                converted.Value,
                converted.AddedOn,
            };
        }

        private static object MapWeatherData(TileData weatherData)
        {
            var converted = weatherData as WeatherData;

            return new
            {
                Temperature = converted.Temperature.GetRoundedValue(),
                Humidity = converted.Humidity.GetRoundedValue(),
                converted.AddedOn,
            };
        }

        private static object MapHeartBeatData(TileData heartbeat)
        {
            var converted = heartbeat as HeartBeatData;

            return new
            {
                converted.ResponseTimeInMs,
                converted.AppVersion,
                converted.AddedOn,
            };
        }
    }
}
