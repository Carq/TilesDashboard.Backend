using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using TilesDashboard.V2.Core.Entities.Exceptions;
using TilesDashboard.WebApi.Configuration;

namespace TilesDashboard.WebApi.Authorization
{
    public class BearerReadAuthorizationFilter : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var settings = context.HttpContext.RequestServices.GetRequiredService<ISecurityConfig>();
            if (!settings.ProtectReadEndpoints)
            {
                return;
            }

            string authHeader = context.HttpContext.Request.Headers["Authorization"];
            if (authHeader == null)
            {
                throw new AuthorizationException("Unauthorized.");
            }

            ValidateToken(settings, authHeader);
        }

        private static void ValidateToken(ISecurityConfig settings, string token)
        {
            if (settings.SecretReadEndpoints != token)
            {
                throw new AuthorizationException("Invalid token.");
            }
        }
    }
}
