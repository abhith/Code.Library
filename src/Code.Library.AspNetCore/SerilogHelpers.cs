using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Formatting.Compact;
using System;
using System.Reflection;

namespace Code.Library.AspNetCore
{
    public static class SerilogHelpers
    {
        /// <summary>
        /// Provides standardized, centralized Serilog wire-up for a suite of applications.
        /// </summary>
        /// <param name="loggerConfig">Provide this value from the UseSerilog method param</param>
        /// <param name="provider">Provide this value from the UseSerilog method param as well</param>
        /// <param name="applicationName">Represents the name of YOUR APPLICATION and will be used to segregate your app
        /// from others in the logging sink(s).</param>
        /// <param name="config">IConfiguration settings -- generally read this from appsettings.json</param>
        public static void WithSimpleConfiguration(this LoggerConfiguration loggerConfig,
            IServiceProvider provider, string applicationName, IConfiguration config)
        {
            var name = Assembly.GetEntryAssembly().GetName();

            loggerConfig
                .ReadFrom.Configuration(config) // minimum levels defined per project in json files
                                                //.Enrich.WithAspnetcoreHttpcontext(provider, AddCustomContextDetails)
                .Enrich.FromLogContext()
                .Enrich.WithMachineName()
                .Enrich.WithProperty("Assembly", $"{name.Name}")
                .Enrich.WithProperty("Version", $"{name.Version}")
                .WriteTo.File(new CompactJsonFormatter(),
                    @"Logs\log.json", rollingInterval: RollingInterval.Day)
                //.WriteTo.File(@"Logs\log.txt", rollingInterval: RollingInterval.Day)
                .WriteTo.ApplicationInsights(TelemetryConfiguration.Active, TelemetryConverter.Traces);
        }
    }
}