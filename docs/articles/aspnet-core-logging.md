# ASP.NET Core Logging

## Enriched Logging with Serilog

To enable Serilog logging, update the default `program.cs` from,

```cs
public class Program
{
    public static IHostBuilder CreateHostBuilder(string[] args) =>
                Host.CreateDefaultBuilder(args)
                    .ConfigureWebHostDefaults(webBuilder =>
                    {
                        webBuilder.UseStartup<Startup>();
                    });

    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }
}
```

To

```cs
using Serilog;
using Code.Library.AspNetCore;
public class Program
{
    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            })
        .UseSerilog((hostingContext, loggerConfiguration) => loggerConfiguration.WithSimpleConfiguration(hostingContext.Configuration));

    public static int Main(string[] args)
    {
        try
        {
            CreateHostBuilder(args).Build().Run();

            return 0;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Host terminated unexpectedly");
            Console.Write(ex.ToString());
            return 1;
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }

}
```

> You don't need to install **Serilog** via **NuGet** since it's a dependency for **Code.Library.AspNetCore** and will get installed automatically when you add **Code.Library.AspNetCore** via **NuGet**.

By doing so, it will generate logs under "Logs" folder in the root directory. And a sample log looks like,

```json
{
  "@t": "2020-04-11T07:56:11.3447207Z",
  "@mt": "{HostingRequestFinishedLog:l}",
  "@r": ["Request finished in 35.4764ms 404 "],
  "ElapsedMilliseconds": 35.4764,
  "StatusCode": 404,
  "ContentType": null,
  "HostingRequestFinishedLog": "Request finished in 35.4764ms 404 ",
  "EventId": { "Id": 2 },
  "SourceContext": "Microsoft.AspNetCore.Hosting.Diagnostics",
  "RequestId": "0HLUTTN5TMP4U:00000003",
  "RequestPath": "/favicon.ico",
  "SpanId": "1d6a0ce5f93a7642",
  "TraceId": "4436b8b32132c7468170c4e1defa84a9",
  "ParentId": "0000000000000000",
  "MachineName": "DESKTOP-MPFENH3",
  "Assembly": "AspNetCore.Microservice.API",
  "Version": "1.0.0.0"
}
```

Refine the logs by adding the below to the **appsettings.json**,

```json
"Serilog": {
    "MinimumLevel": {
      "Default": "Debug",
      "Override": {
        "Microsoft": "Warning",
        "System": "Warning",
        "IdentityServer4": "Information",
        "Orleans": "Warning"
      }
    }
  }
```

We don't need the default **Logging** configuration in the **appsettings.json** anymore, remove it.

```json
"Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Warning",
      "Microsoft.Hosting.Lifetime": "Information"
    }
  },
```

> Also, remember to exclude the logs directory from version control.
