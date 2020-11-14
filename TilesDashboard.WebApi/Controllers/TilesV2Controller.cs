using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TilesDashboard.V2.Core.Entities;
using TilesDashboard.V2.Core.Entities.Enums;
using TilesDashboard.V2.Core.Services;
using TilesDashboard.WebApi.Authorization;

namespace TilesDashboard.WebApi.Controllers
{
    [Route("[controller]/v2")]
    [ApiController]
    public class TilesV2Controller
    {
        private readonly IMetricService _metricService;

        public TilesV2Controller(IMetricService metricService)
        {
            _metricService = metricService ?? throw new ArgumentNullException(nameof(metricService));
        }

        [HttpGet("{tileType}/{tileName}/basic-data")]
        [BearerReadAuthorization]
        public async Task<object> GetMetricBasicData(TileType tileType, string tileName)
        {
            return await _metricService.GetTile(new TileId(tileName, tileType));
        }
    }
}
