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

We can replace the above with the below single line of code,

```cs
 app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();

    endpoints.MapDefaultHealthChecks();
});
```
