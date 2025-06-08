using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Application.Middleware
{
    [ExcludeFromCodeCoverage]
    public class ApiMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ApiMiddleware> _logger;

        public ApiMiddleware(
            RequestDelegate next,
            ILogger<ApiMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            httpContext.Request.EnableBuffering();
            string requestBody = await ReadRequestBodyAsync(httpContext.Request);
            var originalBodyStream = httpContext.Response.Body;

            using (var responseBodyStream = new MemoryStream())
            {
                httpContext.Response.Body = responseBodyStream;
                try
                {
                    await _next(httpContext);
                    httpContext.Response.Body.Seek(0, SeekOrigin.Begin);
                    string responseBody = await new StreamReader(httpContext.Response.Body).ReadToEndAsync();

                    httpContext.Response.Body.Seek(0, SeekOrigin.Begin);
                    await responseBodyStream.CopyToAsync(originalBodyStream);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Exception occurred");
                    throw;
                }
                finally
                {
                    httpContext.Response.Body = originalBodyStream;
                }
            }
        }

        private async Task<string> ReadRequestBodyAsync(HttpRequest request)
        {
            request.Body.Seek(0, SeekOrigin.Begin);
            using (var reader = new StreamReader(request.Body, Encoding.UTF8, true, 1024, true))
            {
                string body = await reader.ReadToEndAsync();
                request.Body.Seek(0, SeekOrigin.Begin);
                return body;
            }
        }
    }
}