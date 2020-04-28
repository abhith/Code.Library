using Code.Library.AspNetCore.Helpers;
using Microsoft.AspNetCore.Builder;
using Serilog;

namespace Code.Library.AspNetCore.Middlewares
{
    public static class RequestLoggingMiddlewareExtensions
    {
        public static IApplicationBuilder UseRequestLogging(this IApplicationBuilder builder)
        {
            return builder
                .UseSerilogRequestLogging(options =>
                options.EnrichDiagnosticContext = SerilogHelper.EnrichFromRequest);
        }
    }
}