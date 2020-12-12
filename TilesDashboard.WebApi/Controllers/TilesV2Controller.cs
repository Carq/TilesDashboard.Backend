using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TilesDashboard.Contract;
using TilesDashboard.Contract.RecordData;
using TilesDashboard.Handy.Extensions;
using TilesDashboard.V2.Core.Entities;
using TilesDashboard.V2.Core.Entities.Enums;
using TilesDashboard.V2.Core.Services;
using TilesDashboard.WebApi.Authorization;
using TilesDashboard.WebApi.Mappers;

namespace TilesDashboard.WebApi.Controllers
{
    [Route("tiles")]
    [ApiController]
    public class TilesV2Controller
    {
        private readonly ITileBaseService _tileService;

        private readonly IWeatherService _weatherService;

        private readonly IMetricService _metricService;

        public TilesV2Controller(ITileBaseService tileService, IWeatherService weatherService, IMetricService metricService)
        {
            _tileService = tileService ?? throw new ArgumentNullException(nameof(tileService));
            _weatherService = weatherService ?? throw new ArgumentNullException(nameof(weatherService));
            _metricService = metricService ?? throw new ArgumentNullException(nameof(metricService));
        }

        [HttpGet("all")]
        [BearerReadAuthorization]
        public async Task<IList<TileWithCurrentDataDto>> GetAllTilesWithRecentData()
        {
            return (await _tileService.GetAllTilesWithRecentData()).MapToContract();
        }

        [HttpGet("{tileType}/{tileName}")]
        [BearerReadAuthorization]
        public async Task<TileWithCurrentDataDto> GetTileBasicData(TileType tileType, string tileName)
        {
            return (await _tileService.GetTile(new TileId(tileName, tileType))).MapToContract();
        }

        [HttpGet("{tileType}/{tileName}/recent")]
        [BearerReadAuthorization]
        public async Task<IList<object>> GetTileRecentData(TileType tileType, string tileName, [FromQuery][Range(1, 120)] int amountOfData = 30)
        {
            return (await _tileService.GetTileRecentData(new TileId(tileName, tileType), amountOfData)).MapToContract();
        }

        [HttpGet("{tileType}/{tileName}/since")]
        [BearerReadAuthorization]
        public async Task<IList<object>> GetTileSinceDate(TileType tileType, string tileName, [FromQuery][Range(1, 120)] int? days, [Range(1, 48)] int? hours)
        {
            int sinceHours = (days ?? 30) * 24;
            if (hours.HasValue)
            {
                sinceHours = hours.Value;
            }

            return (await _tileService.GetTileSinceData(new TileId(tileName, tileType), sinceHours)).MapToContract();
        }

        [HttpPost("weather/{tileName}/record")]
        [BearerAuthorization]
        public async Task RecordWeatherValue(string tileName, [FromBody] RecordWeatherDataDto weatherDataDto)
        {
            await _weatherService.RecordValue(new TileId(tileName, TileType.Weather), weatherDataDto.Temperature, weatherDataDto.Humidity);
        }

        [HttpPost("weather/id/{id}/record")]
        [BearerAuthorization]
        public async Task RecordWeatherValueById([MaxLength(TileStorageId.Length)] string id, [FromBody] RecordWeatherDataDto weatherDataDto)
        {
            await _weatherService.RecordValue(new TileStorageId(id), weatherDataDto.Temperature, weatherDataDto.Humidity);
        }

        [HttpPost("metric/{tileName}/record")]
        [BearerAuthorization]
        public async Task RecordMetricValue(string tileName, [FromBody] RecordMetricData<decimal> metricData)
        {
            await _metricService.RecordValue(new TileId(tileName, TileType.Metric), metricData.Type.Convert<MetricType>(), metricData.Value);
        }
    }
}
