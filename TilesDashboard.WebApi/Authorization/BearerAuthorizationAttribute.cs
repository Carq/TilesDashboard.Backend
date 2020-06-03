using System;
using Microsoft.AspNetCore.Mvc;

namespace TilesDashboard.WebApi.Authorization
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public sealed class BearerAuthorizationAttribute : TypeFilterAttribute
    {
        public BearerAuthorizationAttribute() : base(typeof(BearerAuthorizationFilter))
        {
        }
    }
}
