using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using System.Net;
using Tizzani.AzureFunctionsWorkerHttpExtensions;

namespace E2ETestApp.Functions;

public sealed class CollectionTypeFunctions
{
    [Function(nameof(EchoIntegerArray))]
    public async Task<HttpResponseData> EchoIntegerArray(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestData req,
        [BindQuery] int[] value)
    {
        var response = req.CreateResponse(HttpStatusCode.OK);
        await response.WriteAsJsonAsync(value);
        return response;
    }

    [Function(nameof(EchoIntegerList))]
    public async Task<HttpResponseData> EchoIntegerList(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestData req,
        [BindQuery] List<int> value)
    {
        var response = req.CreateResponse(HttpStatusCode.OK);
        await response.WriteAsJsonAsync(value);
        return response;
    }
}
