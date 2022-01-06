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
using TilesDashboard.V2.Core.Entities.Metric;
using TilesDashboard.V2.Core.Services;
using TilesDashboard.WebApi.Authorization;
using TilesDashboard.WebApi.Mappers;

namespace TilesDashboard.WebApi.Controllers
{
    [Route("tiles")]
    [ApiController]
    public class TilesController
    {
        private readonly ITileBaseService _tileService;

        private readonly IWeatherService _weatherService;

        private readonly IMetricService _metricService;

        private readonly IIntegerService _integerService;

        private readonly IHeartBeatService _heartBeatService;

        private readonly IDualService _dualService;

        public TilesController(
            ITileBaseService tileService,
            IWeatherService weatherService,
            IMetricService metricService,
            IIntegerService integerService,
            IHeartBeatService heartBeatService,
            IDualService dualService)
        {
            _tileService = tileService ?? throw new ArgumentNullException(nameof(tileService));
            _weatherService = weatherService ?? throw new ArgumentNullException(nameof(weatherService));
            _metricService = metricService ?? throw new ArgumentNullException(nameof(metricService));
            _integerService = integerService ?? throw new ArgumentNullException(nameof(integerService));
            _heartBeatService = heartBeatService ?? throw new ArgumentNullException(nameof(heartBeatService));
            _dualService = dualService ?? throw new ArgumentNullException(nameof(dualService));
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

        [HttpPost("weather/id/{storageId}/record")]
        [BearerAuthorization]
        public async Task RecordWeatherValueByStorageId([MaxLength(StorageId.Length)] string storageId, [FromBody] RecordWeatherDataDto weatherDataDto)
        {
            await _weatherService.RecordValue(new StorageId(storageId), weatherDataDto.Temperature, weatherDataDto.Humidity);
        }

        [HttpPost("metric/{tileName}/record")]
        [BearerAuthorization]
        public async Task RecordMetricValue(string tileName, [FromBody] RecordMetricData<decimal> metricData)
        {
            await _metricService.RecordValue(new TileId(tileName, TileType.Metric), metricData.Type.Convert<MetricType>(), metricData.Value);
        }

        [HttpPost("metric/id/{storageId}/record")]
        [BearerAuthorization]
        public async Task RecordWeatherValueByStorageId(string storageId, [FromBody] RecordMetricData<decimal> metricData)
        {
            await _metricService.RecordValue(new StorageId(storageId), metricData.Type.Convert<MetricType>(), metricData.Value);
        }

        [HttpPost("integer/{tileName}/record")]
        [BearerAuthorization]
        public async Task RecordIntegerValue(string tileName, [FromBody] RecordValueDto<int> integerValue)
        {
            await _integerService.RecordValue(new TileId(tileName, TileType.Integer), integerValue.Value);
        }

        [HttpPost("integer/id/{storageId}/record")]
        [BearerAuthorization]
        public async Task RecordIntegerValueByStorageId(string storageId, [FromBody] RecordValueDto<int> integerValue)
        {
            await _integerService.RecordValue(new StorageId(storageId), integerValue.Value);
        }

        [HttpPost("heartbeat/{tileName}/record")]
        [BearerAuthorization]
        public async Task RecordHeartBeatValue(string tileName, [FromBody] RecordHeartBeatValueDto hearbeatValue)
        {
            await _heartBeatService.RecordValue(new TileId(tileName, TileType.HeartBeat), hearbeatValue.ResponseTimeInMs, hearbeatValue.AppVersion, hearbeatValue.AdditionalInfo);
        }

        [HttpPost("heartbeat/id/{storageId}/record")]
        [BearerAuthorization]
        public async Task RecordHeartBeatValueStorageId(string storageId, [FromBody] RecordHeartBeatValueDto hearbeatValue)
        {
            await _heartBeatService.RecordValue(new StorageId(storageId), hearbeatValue.ResponseTimeInMs, hearbeatValue.AppVersion, hearbeatValue.AdditionalInfo);
        }

        [HttpPost("dual/{tileName}/record")]
        [BearerAuthorization]
        public async Task RecordDualValue(string tileName, [FromBody] RecordDualValue dualValue)
        {
            await _dualService.RecordValue(new TileId(tileName, TileType.Dual), dualValue.Primary, dualValue.Secondary);
        }

        [HttpPost("dual/id/{storageId}/record")]
        [BearerAuthorization]
        public async Task RecordDualValueStorageId(string storageId, [FromBody] RecordDualValue dualValue)
        {
            await _dualService.RecordValue(new StorageId(storageId), dualValue.Primary, dualValue.Secondary);
        }
    }
}
