using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TilesDashboard.Contract;
using TilesDashboard.Contract.Enums;
using TilesDashboard.Contract.RecordData;
using TilesDashboard.Core.Domain.Entities;
using TilesDashboard.Core.Domain.Services;
using TilesDashboard.Core.Type;
using TilesDashboard.Handy.Extensions;
using TilesDashboard.Handy.Tools;
using TilesDashboard.WebApi.Authorization;
using TilesDashboard.WebApi.Mappers;

namespace TilesDashboard.WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TilesController : ControllerBase
    {
        private const int AmountOfDate = 5;

        private readonly IMetricService _metricService;

        private readonly IIntegerTileService _integerTileService;

        private readonly ITileService _tileService;

        private readonly IDateTimeOffsetProvider _dateTimeOffsetProvider;

        public TilesController(IMetricService metricService, ITileService tileService, IIntegerTileService integerTileService, IDateTimeOffsetProvider dateTimeOffsetProvider)
        {
            _metricService = metricService ?? throw new ArgumentNullException(nameof(metricService));
            _tileService = tileService ?? throw new ArgumentNullException(nameof(tileService));
            _integerTileService = integerTileService ?? throw new ArgumentNullException(nameof(integerTileService));
            _dateTimeOffsetProvider = dateTimeOffsetProvider ?? throw new ArgumentNullException(nameof(dateTimeOffsetProvider));
        }

        [HttpGet("all")]
        public async Task<IList<TileWithCurrentDataDto>> GetAllTilesWithRecentData(CancellationToken cancellationToken)
        {
            return TileDtoMapper.Map(await _tileService.GetAllAsync(AmountOfDate, cancellationToken));
        }

        [HttpPost("{tileType}/{tileName}/group")]
        [BearerAuthorization]
        public async Task SetTileGroup(string tileType, string tileName, [FromBody] string group, CancellationToken cancellationToken)
        {
            await _tileService.SetGroupToTile(tileName, Enum.Parse<TileType>(tileType), group, cancellationToken);
        }

        [HttpGet("metric/{tileName}/recent")]
        public async Task<IList<object>> GetMetricRecentData(string tileName, CancellationToken cancellationToken)
        {
            return TileDtoMapper.Map(await _metricService.GetMetricRecentDataAsync(tileName, AmountOfDate, cancellationToken));
        }

        [HttpPost("metric/{tileName}/record")]
        [BearerAuthorization]
        public async Task RecordMetricData(string tileName, [FromBody] RecordMetricData<decimal> metricData, CancellationToken cancellationToken)
        {
            await _metricService.RecordMetricDataAsync(tileName, metricData.Type.Convert<MetricType>(), metricData.Value, cancellationToken);
        }

        [HttpGet("metric/{tileName}/since")]
        public async Task<IList<object>> GetMetricDataSince(string tileName, [FromQuery][Required][Range(1, 30)] int days, CancellationToken cancellationToken)
        {
            return TileDtoMapper.Map(await _metricService.GetMetricDataSinceAsync(tileName, days, cancellationToken));
        }

        [HttpGet("integer/{tileName}/recent")]
        public async Task<IList<object>> GetIntegerRecentData(string tileName, CancellationToken cancellationToken)
        {
            return TileDtoMapper.Map(await _integerTileService.GetIntegerRecentDataAsync(tileName, AmountOfDate, cancellationToken));
        }

        [HttpPost("integer/{tileName}/record")]
        [BearerAuthorization]
        public async Task RecordIntegerData(string tileName, [FromBody] int value, CancellationToken cancellationToken)
        {
            await _integerTileService.RecordIntegerDataAsync(tileName, value, cancellationToken);
        }

        [HttpGet("integer/{tileName}/since")]
        public async Task<IList<object>> GetIntegerDataSince(string tileName, [FromQuery][Required][Range(1, 30)] int days, CancellationToken cancellationToken)
        {
            return TileDtoMapper.Map(await _integerTileService.GetIntegerDataSinceAsync(tileName, days, cancellationToken));
        }

        [HttpGet("heartbeat/{tileName}/recent")]
        public async Task<IList<object>> GetHeartbeatRecentData(string tileName, CancellationToken cancellationToken)
        {
            return TileDtoMapper.Map(await _tileService.GetRecentDataAsync<HeartBeatData>(tileName, TileType.HeartBeat, AmountOfDate, cancellationToken));
        }

        [HttpGet("heartbeat/{tileName}/since")]
        public async Task<IList<object>> GetHeartbeatDataSince(string tileName, [FromQuery][Required][Range(1, 30)] int days, CancellationToken cancellationToken)
        {
            return TileDtoMapper.Map(await _tileService.GetDataSinceAsync<HeartBeatData>(tileName, TileType.HeartBeat, _dateTimeOffsetProvider.Now.AddDays(-days), cancellationToken));
        }
    }
}