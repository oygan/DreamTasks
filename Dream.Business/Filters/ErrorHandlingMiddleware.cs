using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Dream.Business.Filters
{
    /// <summary>
    /// Catch and log errors.
    /// </summary>
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next;
            _logger = loggerFactory.CreateLogger(nameof(ErrorHandlingMiddleware));
        }

        public async Task Invoke(HttpContext context)
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

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var code = HttpStatusCode.InternalServerError;
            _logger?.LogError(exception, "Exception");

            LogContext(context);

            var result = JsonConvert.SerializeObject(new { error = exception.Message });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(result);
        }

        private void LogContext(HttpContext context)
        {
            try
            {
                _logger?.LogError("Query String: {0}", context.Request.QueryString.Value);


                var requestBody = context.Request.Body;
                var body = StreamToString(requestBody);
                _logger?.LogError("Body: {0}", body);
            }
            catch
            {
                //eat
            }
        }

        private static string StreamToString(Stream requestBody)
        {
            if (requestBody == null)
                return "";

            requestBody.Position = 0;
            StreamReader reader = new StreamReader(requestBody);
            string text = reader.ReadToEnd();
            requestBody.Position = 0;
            return text;
        }
    }
}
