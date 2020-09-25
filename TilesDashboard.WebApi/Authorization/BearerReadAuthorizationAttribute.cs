using System;
using Microsoft.AspNetCore.Mvc;

namespace TilesDashboard.WebApi.Authorization
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public sealed class BearerReadAuthorizationAttribute : TypeFilterAttribute
    {
        public BearerReadAuthorizationAttribute() : base(typeof(BearerReadAuthorizationFilter))
        {
        }
    }
}
