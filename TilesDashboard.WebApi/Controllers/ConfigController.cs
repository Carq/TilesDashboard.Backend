using Microsoft.AspNetCore.Mvc;
using TilesDashboard.WebApi.Authorization;

namespace TilesDashboard.WebApi.Controllers
{
    [ApiController]
    public class ConfigController : ControllerBase
    {
        [HttpGet("check")]
        [BearerReadAuthorization]
        public IActionResult Check()
        {
            return Ok();
        }
    }
}
