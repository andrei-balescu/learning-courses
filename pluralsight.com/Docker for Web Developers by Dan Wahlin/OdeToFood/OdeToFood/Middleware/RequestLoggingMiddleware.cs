using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Logging;

namespace OdeToFood.Middleware
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _nextMiddleware;
        private readonly ILogger<RequestLoggingMiddleware> _logger;

        public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
        {
            _nextMiddleware = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            await LogRequest(httpContext.Request);
            await _nextMiddleware(httpContext);
        }

        private async Task LogRequest(HttpRequest request)
        {
            string url = request.GetDisplayUrl();

            IEnumerable<string> formattedHeaders = request.Headers.Select(h => $"{h.Key}={h.Value}");
            string headerLog = string.Join(';', formattedHeaders);
            
            string body = await GetRequestBody(request);
            
            var requestLogBuilder = new StringBuilder();
            requestLogBuilder.AppendLine($"HTTP request: {request.Method} {url}");
            requestLogBuilder.AppendLine($"Headers: {headerLog}");
            if (!string.IsNullOrWhiteSpace(body))
            {
                requestLogBuilder.AppendLine($"Body: {body}");
            }

            var requestLog = requestLogBuilder.ToString();
            _logger.LogInformation(requestLog);
        }

        private async Task<string> GetRequestBody(HttpRequest request)
        {
            // placing this inside using statement leaves body empty
            request.EnableBuffering();

            string body;
            using (var reader = new StreamReader(request.Body, Encoding.UTF8, leaveOpen: true))
            {
                body = await reader.ReadToEndAsync();
                request.Body.Seek(0, SeekOrigin.Begin);
            }
            return body;
        }
    }
}