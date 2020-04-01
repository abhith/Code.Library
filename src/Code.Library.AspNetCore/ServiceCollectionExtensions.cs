using AspNetMonsters.ApplicationInsights.AspNetCore;
using Flurl.Http;
using Hellang.Middleware.ProblemDetails;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System.Threading.Tasks;

namespace Code.Library.AspNetCore
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApiExceptionHandler(this IServiceCollection services)
        {
            return services
                .AddProblemDetails();
        }

        public static IServiceCollection AddAppInsight(this IServiceCollection services, IConfiguration configuration, string cloudRoleName)
        {
            services
                .AddApplicationInsightsTelemetry(configuration)
                .AddCloudRoleNameInitializer(cloudRoleName);
            return services;
        }

        public static IServiceCollection AddFlurlTelemetry(this IServiceCollection services)
        {
            FlurlHttp.Configure((settings) => settings.AfterCallAsync = LogFlurlCallsAsync);
            return services;
        }

        private static async Task LogFlurlCallsAsync(HttpCall call)
        {
            var responseBody = string.Empty;

            if (call.Response != null &&
                call.Response.Content != null)
            {
                var ct = call.Response.Content.Headers.ContentType.ToString();
                // avoid logging file downloads
                if (ct.Contains("application/json") ||
                    ct.Contains("text/plain"))
                {
                    responseBody = await call.Response.Content.ReadAsStringAsync();
                }
            }

            Log.Information("External Request | {Endpoint} {Succeeded} {RequestBody} {HttpStatusCode} {ResponseBody}", call.ToString(), call.Succeeded, call.RequestBody, call.HttpStatus.HasValue ?
                    (int)call.HttpStatus : default(int?), responseBody);
        }
    }
}