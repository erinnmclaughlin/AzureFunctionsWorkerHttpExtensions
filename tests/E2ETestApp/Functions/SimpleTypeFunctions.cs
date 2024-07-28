using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using System.Net;
using Tizzani.AzureFunctionsWorkerHttpExtensions;

namespace E2ETestApp.Functions;

public sealed class SimpleTypeFunctions
{
    [Function(nameof(EchoIntegerValue))]
    public async Task<HttpResponseData> EchoIntegerValue(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestData req,
        [BindQuery] int value)
    {
        var response = req.CreateResponse(HttpStatusCode.OK);
        await response.WriteAsJsonAsync(value);
        return response;
    }

    [Function(nameof(EchoNullableIntegerValue))]
    public async Task<HttpResponseData> EchoNullableIntegerValue(
    [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestData req,
    [BindQuery] int? value)
    {
        var response = req.CreateResponse(HttpStatusCode.OK);
        await response.WriteAsJsonAsync(value);
        return response;
    }
}
