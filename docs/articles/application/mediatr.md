# MediatR Extensions

## Behaviors

Behaviors need to be added to the DI, we can follow [jasontaylordev/CleanArchitecture](https://github.com/jasontaylordev/CleanArchitecture) and can be registered in the application project itself by adding a `DependencyInjection.cs` like below,

```cs
using AutoMapper;
using Code.Library.Application.Behaviors;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Project.Name.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPerformanceBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));

            return services;
        }
    }
}
```

And register it under **Startup.cs** of our ASP.NET Core project's `ConfigureServices` method,

```cs
 services.AddApplication();
```

### LoggingBehavior

Under **Startup.cs** `ConfigureServices` method,

```cs
    services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
```

By adding the logging behavior, it wraps the request generated logs like below, the **handling** request log will have the request params and **handled** request log will have the response.

```txt
{"@t":"2020-04-30T05:27:17.3082716Z","@m":"----- Handling \"SomeCommand\" (SomeCommand { params: ... })" ...}
{"@t":"2020-04-30T05:27:18.4984836Z","@m":"External Request | \"POST http://www.mocky.io/v2/5eaa5f4f2d000052002685bf\"...}
{"@t":"2020-04-30T05:27:18.5162345Z","@m":"Long Running Request: \"SomeCommand\" (1040 milliseconds) SomeCommand { params }",...}
{"@t":"2020-04-30T05:27:18.5284381Z","@m":"----- Handled \"SomeCommand\" - response: Unit {  }","@i":"84b9c5be",...}
```

### RequestPerformanceBehavior

```cs
    services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPerformanceBehavior<,>));
```

This will add an additional log for requests that taking more than the threshold (for now, it is set to 500ms), and the log looks like

```
{"@t":"2020-04-30T05:27:18.5162345Z","@m":"Long Running Request: \"SomeCommand\" (1040 milliseconds) SomeCommand { params }","@i":"4334d3bb","@l":"Warning","RequestName":"SomeCommand","ElapsedMilliseconds":1040,"Request":{...,"$type":"SomeCommand"},"SourceContext":Transaction.Commands.actionTransaction.SomeCommand","ActionId":"415e12cf-e4e5-44f4-8e81-3b7cc904f244","ActionName":"Project.Name.API.Controllers.TransactionsController.actionTransactionAsync (Project.Name.API)","RequestId":"0HLVCOSIODRL1:00000001","RequestPath":"/v1/Transactions/79f22472-8787-4f10-816f-53b90701f373/action","SpanId":"9addef6447c19243","TraceId":"8b8e5a2475f2d24c9fcd140c13fbb19d","ParentId":"c46f37f8374da944","ConnectionId":"0HLVCOSIODRL1","MachineName":"WORKSTN1","Assembly":"Project.Name.API","Version":"1.0.0.0"}
```

### UnhandledExceptionBehavior

Under **Startup.cs** `ConfigureServices` method,

```cs
    services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehavior<,>));
```

For any exception that thrown in the request pipeline, except `DomainException`s, by adding this behavior, it will log the exception.
