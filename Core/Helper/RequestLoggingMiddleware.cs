using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Helper
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestLoggingMiddleware> _logger;

        public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Log request details
            _logger.LogInformation("Handling request: {Method} {Url}", context.Request.Method, context.Request.Path);

            try
            {
                // Call the next middleware in the pipeline
                await _next(context);

                // Log response details
                _logger.LogInformation("Handled request: {Method} {Url} with status code {StatusCode}",
                    context.Request.Method, context.Request.Path, context.Response.StatusCode);
            }
            catch (Exception ex)
            {
                // Log exception details
                _logger.LogError(ex, "An error occurred while processing request: {Method} {Url}", context.Request.Method, context.Request.Path);
                throw; // Re-throw the exception after logging
            }
        }
    }

}
