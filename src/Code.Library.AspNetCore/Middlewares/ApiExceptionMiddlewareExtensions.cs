using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Builder;
using System;

namespace Code.Library.AspNetCore.Middlewares
{
    public static class ApiExceptionMiddlewareExtensions
    {
        public static IApplicationBuilder UseApiExceptionHandler(this IApplicationBuilder builder,
            Action<ApiExceptionOptions> configureOptions)
        {
            var options = new ApiExceptionOptions();
            configureOptions(options);
            return BuilderWithApiExceptionHandler(builder, options);
        }

        public static IApplicationBuilder UseApiExceptionHandler(this IApplicationBuilder builder)
        {
            var options = new ApiExceptionOptions();
            return BuilderWithApiExceptionHandler(builder, options);
        }

        private static IApplicationBuilder BuilderWithApiExceptionHandler(IApplicationBuilder builder, ApiExceptionOptions options)
        {
            return builder
                .UseProblemDetails()
                .UseMiddleware<ApiExceptionMiddleware>(options);
        }
    }
}