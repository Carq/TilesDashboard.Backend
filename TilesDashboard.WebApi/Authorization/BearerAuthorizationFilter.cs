using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using TilesDashboard.Core.Exceptions;
using TilesDashboard.WebApi.Configuration;

namespace TilesDashboard.WebApi.Authorization
{
    public class BearerAuthorizationFilter : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            string authHeader = context.HttpContext.Request.Headers["Authorization"];
            if (authHeader == null)
            {
                throw new AuthorizationException("Unauthorized.");
            }

            var authHeaderValue = AuthenticationHeaderValue.Parse(authHeader);
            ValidateToken(context, authHeaderValue.Parameter);
        }

        private void ValidateToken(AuthorizationFilterContext context, string token)
        {
            var settings = context.HttpContext.RequestServices.GetRequiredService<ISecurityConfig>();
            if (settings.SecurityToken != token)
            {
                throw new AuthorizationException("Invalid token.");
            }
        }
    }
}
