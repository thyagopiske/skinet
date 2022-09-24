using Api.Errors;
using System.Diagnostics;
using System.Net;
using System.Text.Json;

namespace Api.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _env;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                var stack = new StackTrace(ex,true);
                var frame = stack.GetFrame(0);
                _logger.LogError(ex, ex.Message);

                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var responseBody = _env.IsDevelopment()
                    ? new ApiException((int)HttpStatusCode.InternalServerError, ex.Message, $"File: {frame?.GetFileName()?.Split("\\")?.Last()}; Method: {frame?.GetMethod()?.Name}; Line: {frame?.GetFileLineNumber()}")
                    : new ApiException((int)HttpStatusCode.InternalServerError);

                await context.Response.WriteAsJsonAsync(responseBody, new JsonSerializerOptions()
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });
            }
        }
    }
}
