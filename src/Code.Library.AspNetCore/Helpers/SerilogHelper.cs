using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Formatting.Compact;
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
    }
}