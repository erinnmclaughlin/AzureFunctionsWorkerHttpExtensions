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
[Function(nameof(PocoExample))]
public HttpResponseData PocoExample(
    [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequestData req,
    [BindQuery] Poco? poco)
{
    var response = req.CreateResponse(HttpStatusCode.OK);
    response.WriteString($"Hello, {poco?.Name ?? "user"}!");

    if (poco?.Numbers is not null)
    {
        var numbersString = string.Join(" + ", poco.Numbers);
        response.WriteString($" {numbersString} = {poco.Numbers.Sum()}");
    }

    return response;
}

public record Poco(string Name, int[] Numbers);
```