using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;

namespace Code.Library.AspNetCore.Middlewares
{
    public class ApiExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ApiExceptionOptions _options;

        public ApiExceptionMiddleware(ApiExceptionOptions options, RequestDelegate next)
        {
            _next = next;
            _options = options;
        }

        public async Task Invoke(HttpContext context /* other dependencies */)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex, _options);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception, ApiExceptionOptions opts)
        {
            var nid = opts.Nid ?? Assembly.GetEntryAssembly().GetName().Name.ToLowerInvariant();
            var instance = $"urn:{nid}:error:{Guid.NewGuid()}";

            var problemDetails = new ProblemDetails
            {
                Type = "https://httpstatuses.com/500",
                Title = "An unexpected error occurred!",
                Status = (short)HttpStatusCode.InternalServerError,
                Detail = "Please use the instance value and contact our support team if the problem persists.",
                Instance = instance
            };

            opts.AddResponseDetails?.Invoke(context, exception, problemDetails);

            Log.Error(exception, "{Instance} | An exception was caught in the API request pipeline", instance);

            throw new ProblemDetailsException(problemDetails);
        }
    }
}