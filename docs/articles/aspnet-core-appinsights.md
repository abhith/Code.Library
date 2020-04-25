# ASP.NET Core - Application Insights with Cloud Role Name

In the `ConfigureServices` method,

```cs
  services.AddAppInsight(Configuration, "Project NAME API")
```

This will register application insights with the specified cloud role name.
