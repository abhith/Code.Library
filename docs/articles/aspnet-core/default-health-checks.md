# ASP.NET Core - Default Health Checks

Most of our apps have health checks and following are common among all,

```cs
 app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();

    // default health checks
    endpoints.MapHealthChecks("/hc", new HealthCheckOptions()
    {
        Predicate = _ => true,
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    });
    endpoints.MapHealthChecks("/liveness", new HealthCheckOptions
    {
        Predicate = r => r.Name.Contains("self")
    });
});

```

We can replace the above with the below code,

```cs
 app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();

    endpoints.MapDefaultHealthChecks();
});
```

Make sure Health checks configured in the `ConfigureServices`,

```cs
services.AddHealthChecks();
```

The same can be done by,

```cs
services..AddHealthChecks(Configuration);
```

```cs
internal static class CustomExtensionMethods
{
    public static IServiceCollection AddHealthChecks(this IServiceCollection services, IConfiguration configuration)
    {
        var hcBuilder = services.AddHealthChecks();

        hcBuilder.AddCheck("self", () => HealthCheckResult.Healthy());

        // other health checks goes here
        ...

        return services;
    }
}
```
