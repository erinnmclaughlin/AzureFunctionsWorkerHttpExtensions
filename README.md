# AzureFunctionsWorkerHttpExtensions

This package adds support for binding query parameters in Azure Functions HTTP Triggers.

```cmd
dotnet add package Tizzani.AzureFunctionsWorkerHttpExtensions
```

## Usage

For more examples, check [here](./tests/E2ETestApp/Functions).

```csharp
// Bind directly to primitive types:
[Function(nameof(Example1))]
public HttpResponseData Example1(
    [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestData req,
    [BindQuery] string? name,
    [BindQuery] int[]? luckyNumbers)
{
    var response = req.CreateResponse(HttpStatusCode.OK);
    response.WriteString($"Hello, {(name ?? "user")}! ");

    if (luckyNumbers == null || luckyNumbers.Length == 0)
    {
        response.WriteString("You do not have any lucky numbers!");
    }
    else
    {
        response.WriteString($"Your lucky numbers are: {string.Join(", ", luckyNumbers)}");
    }
    return response;
}

// Or bind to POCO:
[Function(nameof(Example2))]
public HttpResponseData Example2(
    [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestData req,
    [BindQuery] Poco? poco)
{
    var response = req.CreateResponse(HttpStatusCode.OK);
    response.WriteString($"Hello, {poco?.Name ?? "user"}! ");

    if (poco?.LuckyNumbers == null)
    {
        response.WriteString("You do not have any lucky numbers!");
    }
    else
    {
        response.WriteString($"Your lucky numbers are: {string.Join(", ", poco.LuckyNumbers)}");
    }

    return response;
}

// Or do both!:
[Function(nameof(PocoAndPrimitiveCombo))]
public async Task<HttpResponseData> PocoAndPrimitiveCombo(
    [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestData req,
    [BindQuery] Person? person,
    [BindQuery] string? description)
{
    var response = req.CreateResponse(System.Net.HttpStatusCode.OK);

    await response.WriteAsJsonAsync(new 
    {
        person?.Name,
        person?.Age,
        description 
    });
    
    return response;
}

public record Person(string Name, int Age);
public record Poco(string Name, int[] LuckyNumbers);
```
