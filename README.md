# AzureFunctionsWorkerHttpExtensions

This package adds support for binding query parameters in Azure Functions HTTP Triggers.

## Usage

For more examples, check [here](./tests/E2ETestApp/Functions).

```csharp
// Bind directly to primitive types:
[Function(nameof(Example1))]
public HttpResponseData Example1(
    [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequestData req,
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
    [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequestData req,
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

public record Poco(string Name, int[] LuckyNumbers);
```
