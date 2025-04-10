using System.Net;
using System.Text.Json;
using Microsoft.Extensions.Localization;
using WebApplication1.Resources;

namespace WebApplication1.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IStringLocalizer<Trad> _localizer;

        public ErrorHandlingMiddleware(RequestDelegate next, IStringLocalizer<Trad> localizer)
        {
            _next = next;
            _localizer = localizer;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var statusCode = (int)HttpStatusCode.InternalServerError;
            var path = context.Request.Path;

            var problemDetails = new
            {
                type = "https://httpstatuses.com/500",
                title = _localizer["ErrorTitle"].Value,
                status = statusCode,
                detail = ex.Message,
                instance = path,
            };

            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/problem+json";

            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };
            var json = JsonSerializer.Serialize(problemDetails, options);

            await context.Response.WriteAsync(json);
        }
    }
}
