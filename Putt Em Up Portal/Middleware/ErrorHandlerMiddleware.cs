using System;
using System.Diagnostics;

namespace Putt_Em_Up_Portal.Middleware
{
    public class ErrorHandlerMiddleware
    {

        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {

            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, ILogger<ErrorHandlerMiddleware> logger)
        {

            try
            {

                await _next(context);
            }
            catch (Exception ex) {

                string traceId = Guid.NewGuid().ToString();
                await context.Response.WriteAsync(new
                {
                    statusCode = 500,
                    message = "Internal Server Error."

                }.ToString());

            }

        }
    }
}
