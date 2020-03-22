using System.Collections.Generic;
using TilesDashboard.Contract;
using TilesDashboard.Contract.Enums;
using TilesDashboard.Core.Domain.Entities;
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

        public static IList<TileWithCurrentDataDto> Map(IList<TileWithCurrentData> list)
        {
            var result = new List<TileWithCurrentDataDto>();
            foreach (var item in list)
            {
                result.Add(new TileWithCurrentDataDto
                {
                    Name = item.Name,
                    Type = item.Type.Convert<TileTypeDto>(),
                    CurrentData = item.CurrentData,
                    Configuration = item.Configuration,
                });
            }

            return result;
        }
    }
}
