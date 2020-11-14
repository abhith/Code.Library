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

        private static async Task LogFlurlCallsAsync(FlurlCall call)
        {
            var responseBody = string.Empty;

            if (call.Response?.ResponseMessage?.Content != null)
            {
                var contentType = $"{call.Response.ResponseMessage.Content.Headers.ContentType}";
                // avoid logging file downloads
                if (contentType.Contains("application/json") ||
                    contentType.Contains("text/plain"))
                {
                    responseBody = await call.Response.ResponseMessage.Content.ReadAsStringAsync();
                }
            }

            Log.Information("[HTTP] {Endpoint} {Succeeded} {RequestBody} {HttpStatusCode} {ResponseBody}", call.ToString(), call.Succeeded, call.RequestBody, call.Response?.StatusCode, responseBody);
        }
    }
}