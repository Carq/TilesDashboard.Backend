using System;
using System.Threading;
using Microsoft.AspNetCore.Http;
using TilesDashboard.Handy.Tools;

namespace TilesDashboard.WebApi.Configuration
{
    public class CancellationTokenProvider : ICancellationTokenProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CancellationTokenProvider(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        public CancellationToken GetToken() => _httpContextAccessor.HttpContext.RequestAborted;
    }
}
