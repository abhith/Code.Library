using Code.Library.AspNetCore.Helpers;
using Microsoft.AspNetCore.Builder;
using Serilog;
using Serilog.Events;
using System;

namespace Code.Library.AspNetCore.Middleware.RequestResponseLogging
{
    public static class RequestResponseLoggingMiddlewareExtensions
    {
        public static IApplicationBuilder UseRequestResponseLogging(this IApplicationBuilder builder, Action<RequestResponseLoggingOptions> configureOptions)
        {
            var options = new RequestResponseLoggingOptions();
            configureOptions(options);
            return BuilderWithRequestResponseLogging(builder, options);
        }

        public static IApplicationBuilder UseRequestResponseLogging(this IApplicationBuilder builder)
        {
            var options = new RequestResponseLoggingOptions();
            return BuilderWithRequestResponseLogging(builder, options);
        }

        private static IApplicationBuilder BuilderWithRequestResponseLogging(IApplicationBuilder builder, RequestResponseLoggingOptions options)
        {
            return builder
               .UseMiddleware<RequestResponseLoggingMiddleware>(options)
               .UseSerilogRequestLogging(opts =>
               {
                   opts.EnrichDiagnosticContext = SerilogHelper.EnrichFromRequest;

                   if (options.Exclude.HealthChecks)
                   {
                       opts.GetLevel = SerilogHelper.GetLevel(LogEventLevel.Verbose, "Health checks");
                   }
               });
        }
    }
}