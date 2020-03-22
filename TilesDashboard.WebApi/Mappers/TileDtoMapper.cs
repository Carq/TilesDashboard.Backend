using System.Collections.Generic;
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
                    Temperature = weatherData.Temperature.Value,
                    Humidity = weatherData.Humidity.Value,
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
                result.Add(new TileWithCurrentDataDto
                {
                    Name = item.Name,
                    Type = item.Type.Convert<TileTypeDto>(),
                    CurrentData = Map(item.Type, item.CurrentData),
                    Configuration = item.Configuration,
                });
            }

            return result;
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
                Temperature = converted.Temperature.Value,
                Humidity = converted.Humidity.Value,
                AddedOn = converted.AddedOn,
            };
        }
    }
}
