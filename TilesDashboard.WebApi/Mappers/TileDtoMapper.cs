using System;
using System.Collections.Generic;
using System.Linq;
using TilesDashboard.Contract;
using TilesDashboard.Contract.Enums;
using TilesDashboard.Core.Domain.Entities;
using TilesDashboard.Core.Domain.Enums;
using TilesDashboard.Core.Storage.Entities;
using TilesDashboard.Handy.Extensions;

namespace TilesDashboard.WebApi.Mappers
{
    public static class TileDtoMapper
    {
        public static TileDataDto Map(WeatherData weatherData)
        {
            return new TileDataDto
            {
                Data = new
                {
                    Temperature = Math.Round(weatherData.Temperature.Value, 1),
                    Humidity = Math.Round(weatherData.Humidity.Value, 0),
                    AddedOn = weatherData.AddedOn,
                },
                Type = TileTypeDto.Weather,
            };
        }

        public static TileDataDto Map(MetricData metricData)
        {
            return new TileDataDto
            {
                Data = new
                {
                    metricData.Value,
                    AddedOn = metricData.AddedOn,
                },
                Type = TileTypeDto.Metric,
            };
        }

        public static IList<TileWithCurrentDataDto> Map(IList<GenericTileWithCurrentData> list)
        {
            var result = new List<TileWithCurrentDataDto>();
            foreach (var item in list)
            {
                var tileWithDataDto = new TileWithCurrentDataDto
                {
                    Name = item.Name,
                    Type = item.Type.Convert<TileTypeDto>(),
                    CurrentData = Map(item.Type, item.CurrentData),
                    Configuration = item.Configuration,
                };

                tileWithDataDto.RecentData.AddRange(Map(item.Type, item.RecentData));
                result.Add(tileWithDataDto);
            }

            return result;
        }

        private static IList<object> Map(TileType type, IList<TileData> recentData)
        {
            switch (type)
            {
                case TileType.Metric:
                    return recentData.Select(x => MapMetricData(x)).ToList();
                case TileType.Weather:
                    return recentData.Select(x => MapWeatherData(x)).ToList();
                default:
                    return null;
            }
        }

        private static object Map(TileType type, TileData currentData)
        {
            switch (type)
            {
                case TileType.Metric:
                    return MapMetricData(currentData);
                case TileType.Weather:
                    return MapWeatherData(currentData);
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
                AddedOn = converted.AddedOn,
            };
        }

        private static object MapWeatherData(TileData weatherData)
        {
            var converted = weatherData as WeatherData;

            return new
            {
                Temperature = Math.Round(converted.Temperature.Value, 1),
                Humidity = Math.Round(converted.Humidity.Value, 0),
                AddedOn = converted.AddedOn,
            };
        }
    }
}
