using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Compact;
using System;
using System.Reflection;

namespace Code.Library.AspNetCore.Helpers
{
    public static class SerilogHelper
    {
        public static void EnrichFromRequest(
                    IDiagnosticContext diagnosticContext, HttpContext httpContext)
        {
            var request = httpContext.Request;
            diagnosticContext.Set("Host", request.Host);
            diagnosticContext.Set("Protocol", request.Protocol);
            diagnosticContext.Set("Scheme", request.Scheme);
            if (request.QueryString.HasValue)
            {
                diagnosticContext.Set("QueryString", request.QueryString.Value);
            }

            diagnosticContext.Set("ContentType", httpContext.Response.ContentType);
            var endpoint = httpContext.GetEndpoint();
            if (endpoint is object)
            {
                diagnosticContext.Set("EndpointName", endpoint.DisplayName);
            }
            diagnosticContext.Set("UserAgent", httpContext.Request.Headers["User-Agent"].ToString());
            diagnosticContext.Set("ClientIP", httpContext.Connection.RemoteIpAddress);
            diagnosticContext.Set("UserName", httpContext.User.Identity.Name ?? "(anonymous)");

            var clientIdClaim = httpContext.User.FindFirst("client_id");
            if (clientIdClaim != null)
            {
                diagnosticContext.Set("OAuthClientId", clientIdClaim.Value);
            }
        }

        /// <summary>
        /// Create a <see cref="Serilog.AspNetCore.RequestLoggingOptions.GetLevel"> method that
        /// uses the default logging level, except for the specified endpoint names, which are
        /// logged using the provided <paramref name="traceLevel" />.
        /// </summary>
        /// <param name="traceLevel">The level to use for logging "trace" endpoints</param>
        /// <param name="traceEndointNames">The display name of endpoints to be considered "trace" endpoints</param>
        /// <returns></returns>
        public static Func<HttpContext, double, Exception, LogEventLevel> GetLevel(LogEventLevel traceLevel, params string[] traceEndointNames)
        {
            if (traceEndointNames is null || traceEndointNames.Length == 0)
            {
                throw new ArgumentNullException(nameof(traceEndointNames));
            }

            return (ctx, _, ex) =>
                IsError(ctx, ex)
                ? LogEventLevel.Error
                : IsTraceEndpoint(ctx, traceEndointNames)
                    ? traceLevel
                    : LogEventLevel.Information;
        }

        /// <summary>
        /// Provides standardized, centralized Serilog wire-up for a suite of applications.
        /// </summary>
        /// <param name="loggerConfig">Provide this value from the UseSerilog method param</param>
        /// <param name="config">IConfiguration settings -- generally read this from appsettings.json</param>
        /// <remarks>
        /// Sample appsettings
        /// "Serilog": {
        ///  "MinimumLevel": {
        ///    "Default": "Debug",
        ///    "Override": {
        ///      "Microsoft": "Warning",
        ///      "System": "Warning",
        ///      "IdentityServer4": "Information",
        ///      "Orleans": "Warning"
        ///    }
        ///  },
        ///  "SeqServerUrl": null
        /// }
        /// </remarks>
        public static void WithSimpleConfiguration(this LoggerConfiguration loggerConfig,
                  IConfiguration configuration)
        {
            var name = Assembly.GetEntryAssembly().GetName();

            loggerConfig
                .ReadFrom.Configuration(configuration) // minimum levels defined per project in json files
                .Enrich.FromLogContext()
                .Enrich.WithMachineName()
                .Enrich.WithProperty("Assembly", $"{name.Name}")
                .Enrich.WithProperty("Version", $"{name.Version}")
                .WriteTo.File(new RenderedCompactJsonFormatter(),
                    @"logs\log.ndjson", rollingInterval: RollingInterval.Day)
                // TODO(abhith): find alternative for TelemetryConfiguration.Active
                .WriteTo.ApplicationInsights(TelemetryConfiguration.Active, TelemetryConverter.Traces);

            var seqServerUrl = configuration["Serilog:SeqServerUrl"];

            if (!string.IsNullOrWhiteSpace(seqServerUrl))
            {
                loggerConfig.WriteTo.Seq(seqServerUrl);
            }
        }

        private static bool IsError(HttpContext ctx, Exception ex)
            => ex != null || ctx.Response.StatusCode > 499;

        private static bool IsTraceEndpoint(HttpContext ctx, string[] traceEndoints)
        {
            var endpoint = ctx.GetEndpoint();
            if (endpoint is object)
            {
                for (var i = 0; i < traceEndoints.Length; i++)
                {
                    if (string.Equals(traceEndoints[i], endpoint.DisplayName, StringComparison.OrdinalIgnoreCase))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}