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
using TilesDashboard.WebApi.Mappers;

namespace TilesDashboard.WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TilesController : ControllerBase
    {
        private readonly IWeatherServices _weatherService;

        private readonly IMetricService _metricService;

        private readonly ITileService _tileService;

        public TilesController(IWeatherServices weatherServices, IMetricService metricService, ITileService tileService)
        {
            _weatherService = weatherServices ?? throw new System.ArgumentNullException(nameof(weatherServices));
            _metricService = metricService ?? throw new System.ArgumentNullException(nameof(weatherServices));
            _tileService = tileService ?? throw new System.ArgumentNullException(nameof(tileService));
        }

        [HttpGet("all")]
        public async Task<IList<TileWithCurrentDataDto>> GetAllTilesWithRecentData(CancellationToken cancellationToken)
        {
            return TileDtoMapper.Map(await _tileService.GetAllAsync(cancellationToken));
        }

        [HttpGet("weather/{tileName}/recent")]
        public async Task<TileDataDto> GetWeatherRecentData(string tileName, CancellationToken cancellationToken)
        {
            return TileDtoMapper.Map(await _weatherService.GetWeatherRecentDataAsync(tileName, cancellationToken));
        }

        [HttpPost("weather/{tileName}/record")]
        public async Task RecortdWeatherData(string tileName, [FromBody]RecordWeatherDataDto weatherDataDto, CancellationToken cancellationToken)
        {
            await _weatherService.RecordWeatherDataAsync(tileName, new Temperature(weatherDataDto.Temperature), new Percentage(weatherDataDto.Huminidy), null, cancellationToken);
        }

        [HttpGet("metric/{tileName}/recent")]
        public async Task<TileDataDto> GetMetricRecentData(string tileName, CancellationToken cancellationToken)
        {
            return TileDtoMapper.Map(await _metricService.GetMetricRecentDataAsync(tileName, cancellationToken));
        }

        [HttpPost("metric/{tileName}/record")]
        public async Task RecordMetricData(string tileName, [FromBody]RecordMetricData<decimal> metricData, CancellationToken cancellationToken)
        {
            await _metricService.RecordMetricDataAsync(tileName, metricData.Type.Convert<MetricType>(), metricData.Value, cancellationToken);
        }
    }
}