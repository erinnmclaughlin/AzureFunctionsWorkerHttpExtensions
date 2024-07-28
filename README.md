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
    // ..
}

// Or bind to POCO:
[Function(nameof(Example2))]
public HttpResponseData Example2(
    [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestData req,
    [BindQuery] Poco? poco)
{
    // ..
}

// Or do both!:
[Function(nameof(PocoAndPrimitiveCombo))]
public async Task<HttpResponseData> PocoAndPrimitiveCombo(
    [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestData req,
    [BindQuery] Person? person,
    [BindQuery] string? description)
{
    // ..
}

public record Person(string Name, int Age);
public record Poco(string Name, int[] LuckyNumbers);
```
