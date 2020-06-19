using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TilesDashboard.Contract;
using TilesDashboard.Contract.RecordData;
using TilesDashboard.Core.Domain.Enums;
using TilesDashboard.Core.Domain.Services;
using TilesDashboard.Core.Domain.ValueObjects;
using TilesDashboard.Handy.Extensions;
using TilesDashboard.WebApi.Authorization;
using TilesDashboard.WebApi.Mappers;

namespace TilesDashboard.WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TilesController : ControllerBase
    {
        private const int AmountOfDate = 5;

        private readonly IWeatherServices _weatherService;

        private readonly IMetricService _metricService;

        private readonly ITileService _tileService;

        public TilesController(IWeatherServices weatherServices, IMetricService metricService, ITileService tileService)
        {
            _weatherService = weatherServices ?? throw new ArgumentNullException(nameof(weatherServices));
            _metricService = metricService ?? throw new ArgumentNullException(nameof(weatherServices));
            _tileService = tileService ?? throw new ArgumentNullException(nameof(tileService));
        }

        [HttpGet("all")]
        public async Task<IList<TileWithCurrentDataDto>> GetAllTilesWithRecentData(CancellationToken cancellationToken)
        {
            return TileDtoMapper.Map(await _tileService.GetAllAsync(AmountOfDate, cancellationToken));
        }

        [HttpGet("weather/{tileName}/recent")]
        public async Task<IList<object>> GetWeatherRecentData(string tileName, CancellationToken cancellationToken)
        {
            return TileDtoMapper.Map(await _weatherService.GetWeatherRecentDataAsync(tileName, AmountOfDate, cancellationToken));
        }

        [HttpGet("weather/{tileName}/last24h")]
        public async Task<IList<object>> GetWeatherTodayData(string tileName, CancellationToken cancellationToken)
        {
            return TileDtoMapper.Map(await _weatherService.GetWeatherDataFromLast24hAsync(tileName, cancellationToken));
        }

        [HttpPost("{tileType}/{tileName}/group")]
        [BearerAuthorization]
        public async Task SetTileGroup(string tileType, string tileName, [FromBody]string group, CancellationToken cancellationToken)
        {
            await _tileService.SetGroupToTile(tileName, Enum.Parse<TileType>(tileType), group, cancellationToken);
        }

        [HttpPost("weather/{tileName}/record")]
        [BearerAuthorization]
        public async Task RecordWeatherData(string tileName, [FromBody]RecordWeatherDataDto weatherDataDto, CancellationToken cancellationToken)
        {
            await _weatherService.RecordWeatherDataAsync(tileName, new Temperature(weatherDataDto.Temperature), new Percentage(weatherDataDto.Humidity), null, cancellationToken);
        }

        [HttpDelete("weather/{tileName}/removeFakeData")]
        [BearerAuthorization]
        public async Task RemoveWeatherFakeData(string tileName, CancellationToken cancellationToken)
        {
            await _weatherService.RemoveFakeDataAsync(tileName, cancellationToken);
        }

        [HttpGet("metric/{tileName}/recent")]
        public async Task<IList<object>> GetMetricRecentData(string tileName, CancellationToken cancellationToken)
        {
            return TileDtoMapper.Map(await _metricService.GetMetricRecentDataAsync(tileName, AmountOfDate, cancellationToken));
        }

        [HttpPost("metric/{tileName}/record")]
        [BearerAuthorization]
        public async Task RecordMetricData(string tileName, [FromBody]RecordMetricData<decimal> metricData, CancellationToken cancellationToken)
        {
            await _metricService.RecordMetricDataAsync(tileName, metricData.Type.Convert<MetricType>(), metricData.Value, cancellationToken);
        }
    }
}