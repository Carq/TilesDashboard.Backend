using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TilesDashboard.Contract;
using TilesDashboard.Handy.Extensions;
using TilesDashboard.V2.Core.Entities;
using TilesDashboard.V2.Core.Entities.Enums;
using TilesDashboard.V2.Core.Entities.Exceptions;
using TilesDashboard.V2.Core.Entities.Metric;
using TilesDashboard.V2.Core.Entities.Weather;
using TilesDashboard.V2.Core.Services;
using TilesDashboard.WebApi.Authorization;
using TilesDashboard.WebApi.Mappers;

namespace TilesDashboard.WebApi.Controllers
{
    [Route("tiles/v2")]
    [ApiController]
    public class TilesV2Controller
    {
        private readonly ITileBaseService _tileService;

        public TilesV2Controller(ITileBaseService tileService)
        {
            _tileService = tileService ?? throw new ArgumentNullException(nameof(tileService));
        }

        [HttpGet("all")]
        [BearerReadAuthorization]
        public async Task<IList<TileWithCurrentDataDto>> GetAllTilesWithRecentData()
        {
            return (await _tileService.GetAllTiles()).MapToContract();
        }

        [HttpGet("{tileType}/{tileName}/basic-data")]
        [BearerReadAuthorization]
        public async Task<TileWithCurrentDataDto> GetTileBasicData(TileType tileType, string tileName)
        {
            return (await _tileService.GetTile(new TileId(tileName, tileType))).MapToContract();
        }
    }
}
