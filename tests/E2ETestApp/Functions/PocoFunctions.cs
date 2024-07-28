using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Tizzani.AzureFunctionsWorkerHttpExtensions;

namespace E2ETestApp.Functions;

public sealed class PocoFunctions
{
    public record SimplePoco(string Name);

    [Function(nameof(EchoSimplePoco))]
    public async Task<HttpResponseData> EchoSimplePoco(
        [HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequestData req,
        [BindQuery] SimplePoco poco)
    {
        var response = req.CreateResponse(System.Net.HttpStatusCode.OK);
        await response.WriteAsJsonAsync(poco);
        return response;
    }
}
