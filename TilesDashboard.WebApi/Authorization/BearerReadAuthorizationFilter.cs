﻿using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using TilesDashboard.Core.Exceptions;
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

        private void ValidateToken(ISecurityConfig settings, string token)
        {
            if (settings.SecretReadEndpoints != token)
            {
                throw new AuthorizationException("Invalid token.");
            }
        }
    }
}