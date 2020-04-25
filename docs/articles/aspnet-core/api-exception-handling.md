# ASP.NET Core - API Exception Handling

Under **Startup.cs** `Configure` method,

```cs
app.UseApiExceptionHandler();
```

and in the `ConfigureServices` method,

```cs
 services.AddApiExceptionHandler();
```

This will catch any unhandled exceptions and return a internal server error problem details response.

```json
{
  "type": "https://httpstatuses.com/500",
  "title": "An unexpected error occurred!",
  "status": 500,
  "detail": "Please use the instance value and contact our support team if the problem persists.",
  "instance": "urn:project.name.api:error:4b51a4a8-4058-40d2-9de2-6c6bf248dcd7"
}
```

The instance value can be used to get details about exception from the logs.

If the exception is of type `DomainException` (From Code.Library), this will transform it to a bad request with domain validation error in problem details format.

```json
{
  "title": "One or more validation errors occurred.",
  "status": 400,
  "detail": "Please refer to the errors property for additional details.",
  "instance": "urn:project.name.api:error:9b726bb9-ca80-4290-8d0b-c459dee09f68",
  "errors": {
    "DomainValidations": ["Transaction number does not exist"]
  }
}
```
