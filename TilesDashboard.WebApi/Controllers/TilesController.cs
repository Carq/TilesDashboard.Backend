using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TilesDashboard.Contract;
using TilesDashboard.Core.Domain.Services;
using TilesDashboard.WebApi.Mappers;

namespace TilesDashboard.WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TilesController : ControllerBase
    {
        private readonly IWeatherServices _weatherRepository;

        public TilesController(IWeatherServices weatherServices)
        {
            _weatherRepository = weatherServices ?? throw new System.ArgumentNullException(nameof(weatherServices));
        }

        [HttpGet("weather/{tileName}/recent")]
        public async Task<TileDataDto> GetWeatherRecentData(string tileName, CancellationToken cancellationToken)
        {
            return TileDtoMapper.Map(await _weatherRepository.GetWeatherRecentDataAsync(tileName, cancellationToken));
        }

        [HttpPost("weather/{tileName}/record")]
        public async Task RecortdWeatherRecentData(string tileName, [FromBody]WeatherDataDto weatherDataDto, CancellationToken cancellationToken)
        {
            await _weatherRepository.RecordWeatherDataAsync(tileName, weatherDataDto.Temperature, weatherDataDto.Huminidy, cancellationToken);
        }
    }
}