using Code.Library.Domain.Exceptions;
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

            Log.Error(exception, "{Instance} | An exception was caught in the API request pipeline", instance);

            if (exception.GetType() == typeof(DomainException))
            {
                var domainProblemDetails = new ValidationProblemDetails()
                {
                    Instance = instance,
                    Status = StatusCodes.Status400BadRequest,
                    Detail = "Please refer to the errors property for additional details."
                };

                domainProblemDetails.Errors.Add("DomainValidations", new string[] { exception.Message });
                throw new ProblemDetailsException(domainProblemDetails);
            }

            var problemDetails = new ProblemDetails
            {
                Type = "https://httpstatuses.com/500",
                Title = "An unexpected error occurred!",
                Status = (short)HttpStatusCode.InternalServerError,
                Detail = "Please use the instance value and contact our support team if the problem persists.",
                Instance = instance
            };

            opts.AddResponseDetails?.Invoke(context, exception, problemDetails);
            throw new ProblemDetailsException(problemDetails);
        }
    }
}