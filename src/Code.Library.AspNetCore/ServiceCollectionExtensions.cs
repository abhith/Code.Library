using AspNetMonsters.ApplicationInsights.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Code.Library.AspNetCore
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAppInsight(this IServiceCollection services, IConfiguration configuration, string cloudRoleName)
        {
            services
                .AddApplicationInsightsTelemetry(configuration)
                .AddCloudRoleNameInitializer(cloudRoleName);
            return services;
        }
    }
}