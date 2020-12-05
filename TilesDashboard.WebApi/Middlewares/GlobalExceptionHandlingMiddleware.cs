using System;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TilesDashboard.V2.Core.Entities.Exceptions;

namespace TilesDashboard.WebApi.Middlewares
{
    public class GlobalExceptionHandlingMiddleware : IMiddleware
    {
        private readonly IWebHostEnvironment _env;

        private readonly JsonSerializerOptions _jsonOptions;

        private readonly ILogger<GlobalExceptionHandlingMiddleware> _logger;

        public GlobalExceptionHandlingMiddleware(
            IWebHostEnvironment env,
            IOptions<JsonOptions> jsonOptions,
            ILogger<GlobalExceptionHandlingMiddleware> logger)
        {
            _jsonOptions = jsonOptions.Value.JsonSerializerOptions;
            _logger = logger;
            _env = env;
        }

        [SuppressMessage("Microsoft.Design", "CA1031", Justification = "Global Exception handler.")]
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);
            }
            catch (AuthorizationException e)
            {
                await CreateHttpResponse(context, HttpStatusCode.Unauthorized, e);
            }
            catch (NotSupportedOperationException e)
            {
                await CreateHttpResponse(context, HttpStatusCode.BadRequest, e);
            }
            catch (ValidationException e)
            {
                await CreateHttpResponse(context, HttpStatusCode.BadRequest, e);
            }
            catch (NotFoundException e)
            {
                await CreateHttpResponse(context, HttpStatusCode.NotFound, e);
            }
            catch (Exception e)
            {
                await CreateHttpResponse(context, HttpStatusCode.InternalServerError, e);
            }
        }

        private async Task CreateHttpResponse(HttpContext context, HttpStatusCode httpCode, Exception exception)
        {
            _logger.LogWarning(exception, $"Unexpected exception occured: {exception.Message}");
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)httpCode;
            await context.Response.WriteAsync(
                JsonSerializer.Serialize(
                    new
                    {
                        Details = CreateDetailsResponse(exception),
                    },
                    _jsonOptions));
        }

        private string CreateDetailsResponse(Exception exception)
        {
            if (_env.IsDevelopment())
            {
                return exception.ToString();
            }

            return exception.Message;
        }
    }
}
