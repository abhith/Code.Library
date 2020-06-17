using Code.Library.AspNetCore.Helpers;
using Microsoft.AspNetCore.Builder;
using Serilog;
using Serilog.Events;

namespace Code.Library.AspNetCore.Middleware
{
    public static class RequestResponseLoggingMiddlewareExtensions
    {
        public static IApplicationBuilder UseRequestResponseLogging(this IApplicationBuilder builder, bool excludeHealthChecks = true)
        {
            return builder
                .UseMiddleware<RequestResponseLoggingMiddleware>()
                .UseSerilogRequestLogging(options =>
                {
                    options.EnrichDiagnosticContext = SerilogHelper.EnrichFromRequest;

                    if (excludeHealthChecks)
                    {
                        options.GetLevel = SerilogHelper.GetLevel(LogEventLevel.Verbose, "Health checks");
                    }
                });
        }
    }
}