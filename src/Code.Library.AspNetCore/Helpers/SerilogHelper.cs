using Destructurama;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Compact;
using Serilog.Formatting.Elasticsearch;
using System;
using System.Collections.Generic;
using System.Linq;
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
            if (endpoint != null)
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
        /// <param name="traceEndpointNames">The display name of endpoints to be considered "trace" endpoints</param>
        /// <returns></returns>
        public static Func<HttpContext, double, Exception, LogEventLevel> GetLevel(LogEventLevel traceLevel, params string[] traceEndpointNames)
        {
            if (traceEndpointNames is null || traceEndpointNames.Length == 0)
            {
                throw new ArgumentNullException(nameof(traceEndpointNames));
            }

            return (ctx, _, ex) =>
                IsError(ctx, ex)
                ? LogEventLevel.Error
                : IsTraceEndpoint(ctx, traceEndpointNames)
                    ? traceLevel
                    : LogEventLevel.Information;
        }

        /// <summary>
        /// Provides standardized, centralized Serilog wire-up for a suite of applications.
        /// </summary>
        /// <param name="loggerConfig">Provide this value from the UseSerilog method param</param>
        /// <param name="configuration">IConfiguration settings -- generally read this from appsettings.json</param>
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
                  IConfiguration configuration, IServiceProvider serviceProvider)
        {
            var name = Assembly.GetEntryAssembly().GetName();

            loggerConfig
                .ReadFrom.Configuration(configuration) // minimum levels defined per project in json files
                .Enrich.FromLogContext()
                .Enrich.WithMachineName()
                .Enrich.WithProperty("Assembly", $"{name.Name}")
                .Enrich.WithProperty("Version", $"{name.Version}")
                .Destructure.UsingAttributes()
                .WriteTo.ApplicationInsights(serviceProvider.GetRequiredService<TelemetryConfiguration>(), TelemetryConverter.Traces);

            if (configuration.GetValue<bool>("Serilog:UseElasticsearchFormatter", false))
            {
                loggerConfig.WriteTo.Async(a => a.Console(new ElasticsearchJsonFormatter()));
            }
            else
            {
                loggerConfig.WriteTo.Async(a => a.Console());
            }

            if (configuration.GetValue<bool>("Serilog:WriteToFile", false))
            {
                loggerConfig.WriteTo.Async(a => a.File(new RenderedCompactJsonFormatter(),
                    @"logs\log.ndjson", rollingInterval: RollingInterval.Day));
            }

            var seqServerUrl = configuration["Serilog:SeqServerUrl"];

            if (!string.IsNullOrWhiteSpace(seqServerUrl))
            {
                loggerConfig.WriteTo.Seq(seqServerUrl);
            }
        }

        private static bool IsError(HttpContext ctx, Exception ex)
            => ex != null || ctx.Response.StatusCode > 499;

        private static bool IsTraceEndpoint(HttpContext ctx, IEnumerable<string> traceEndpoints)
        {
            var endpoint = ctx.GetEndpoint();
            return endpoint != null && traceEndpoints.Any(traceEndpoint => string.Equals(traceEndpoint, endpoint.DisplayName, StringComparison.OrdinalIgnoreCase));
        }
    }
}