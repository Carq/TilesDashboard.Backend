using System;
using Microsoft.AspNetCore.Mvc;

namespace TilesDashboard.WebApi.Authorization
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public sealed class BearerWriteAuthorizationAttribute : TypeFilterAttribute
    {
        public BearerWriteAuthorizationAttribute() : base(typeof(BearerWriteAuthorizationFilter))
        {
        }
    }
}
