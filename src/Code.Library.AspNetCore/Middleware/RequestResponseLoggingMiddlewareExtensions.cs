using Code.Library.AspNetCore.Helpers;
using Microsoft.AspNetCore.Builder;
using Serilog;

namespace Code.Library.AspNetCore.Middleware
{
    public static class RequestResponseLoggingMiddlewareExtensions
    {
        public static IApplicationBuilder UseRequestResponseLogging(this IApplicationBuilder builder)
        {
            return builder
                .UseMiddleware<RequestResponseLoggingMiddleware>()
                .UseSerilogRequestLogging(options => options.EnrichDiagnosticContext = SerilogHelper.EnrichFromRequest);
        }
    }
}