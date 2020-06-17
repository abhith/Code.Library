using Code.Library.AspNetCore.Helpers;
using Microsoft.AspNetCore.Builder;
using Serilog;
using Serilog.Events;

namespace Code.Library.AspNetCore.Middleware
{
    public static class RequestLoggingMiddlewareExtensions
    {
        public static IApplicationBuilder UseRequestLogging(this IApplicationBuilder builder, bool excludeHealthChecks = true)
        {
            return builder
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