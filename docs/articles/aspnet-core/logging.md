# ASP.NET Core - Logging

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

By doing so, it will generate logs under "logs" folder in the root directory. And a sample log looks like,

```json
{
  "@t": "2020-04-25T13:55:20.3908717Z",
  "@m": "HTTP \"GET\" \"/liveness\" responded 200 in 5.5626 ms",
  "@i": "62d0885c",
  "Host": "localhost:5015",
  "Protocol": "HTTP/1.1",
  "Scheme": "http",
  "ContentType": "text/plain",
  "UserAgent": "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/81.0.4044.113 Safari/537.36",
  "ClientIP": "::1",
  "UserName": "(anonymous)",
  "RequestMethod": "GET",
  "RequestPath": "/liveness",
  "StatusCode": 200,
  "Elapsed": 5.5626,
  "SourceContext": "Serilog.AspNetCore.RequestLoggingMiddleware",
  "RequestId": "0HLV93VUMSS33:00000002",
  "SpanId": "7b3e3dd92277ae4c",
  "TraceId": "81595b40301ed846b6e9a9b3fbac85d5",
  "ParentId": "0000000000000000",
  "ConnectionId": "0HLV93VUMSS33",
  "MachineName": "WORKSTN1",
  "Assembly": "Projet.Name.API",
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

## Request logging

Under **Startup.cs** `Configure` method,

```cs
app.UseRequestLogging();
```

Which internally uses Serilog's default request logging along with the enrichers.

## Flurl request logging

To enable logging requests made by Flurl, add following under `ConfigureServices`

```cs
 services.AddFlurlTelemetry();
```
