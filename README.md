# AzureFunctionsWorkerHttpExtensions

This package adds support for binding query parameters in Azure Functions HTTP Triggers.

## Usage
```csharp
// Bind directly to primitive types:
[Function(nameof(SayHello))]
public HttpResponseData SayHello(
    [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequestData req,
    [BindQuery] string name)
{
    var response = req.CreateResponse(HttpStatusCode.OK);
    response.WriteString($"Hello, {name}");
    return response;
}

// Bind to multiple parameters at once:
[Function(nameof(AddNumbers))]
public HttpResponseData AddNumbers(
    [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequestData req,
    [BindQuery] int number1,
    [BindQuery] int number2)
{
    var response = req.CreateResponse(HttpStatusCode.OK);
    response.WriteString($"{number1} + {number2} = {number1 + number2}");
    return response;
}

// Or bind to POCO!:
[Function(nameof(SayHelloWithPoco))]
public HttpResponseData SayHelloWithPoco(
    [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequestData req,
    [BindQuery] CallerName? caller)
{
    var response = req.CreateResponse(HttpStatusCode.OK);
    response.WriteString($"Hello, {caller?.Name}");
    return response;
}

public record CallerName(string Name);
```
