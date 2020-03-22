﻿using TilesDashboard.Contract;
using TilesDashboard.Contract.Enums;
using TilesDashboard.Core.Domain.Entities;

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
    }
}