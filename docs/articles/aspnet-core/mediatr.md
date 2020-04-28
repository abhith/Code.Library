# ASP.NET Core - MediatR

## Behaviors

### LoggingBehavior

Under **Startup.cs** `ConfigureServices` method,

```cs
    services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
```
