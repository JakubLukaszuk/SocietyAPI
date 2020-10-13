using System;
using System.Threading.Tasks;
using Application.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Text.Json;

namespace API.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            this._next = next;
            this.logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch(Exception exception)
            {
                await HandleExepctionAsync(context, exception, logger);
            }
        }

        private async Task HandleExepctionAsync(HttpContext context, Exception exception, ILogger<ErrorHandlingMiddleware> logger)
        {
            object errors = null;
            switch(exception)
            {
                case RestException e:
                    logger.LogError(e, "REST ERROR");
                    errors = e.Errors;
                    context.Response.StatusCode = (int) e.Code;
                    break;
                case Exception e:
                    logger.LogError(e, "SERVER ERROR");
                    errors = string.IsNullOrWhiteSpace(e.Message) ?  "error" : e.Message;
                    context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
                    break;
            }
            context.Response.ContentType = "application/json";
            if(errors != null)
            {
                string result = JsonSerializer.Serialize(new
                {
                    errors
                });

                await context.Response.WriteAsync(result);
            }
        }
    }
}