using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Tizzani.AzureFunctionsWorkerHttpExtensions;

namespace E2ETestApp.Functions;

public sealed class MiscFunctions
{
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
}
