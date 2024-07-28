using Azure;
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

    [Function(nameof(MiscFunction1))]
    public async Task<HttpResponseData> MiscFunction1(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestData req,
        [BindQuery] FilterDto filter)
    {
        var response = req.CreateResponse(System.Net.HttpStatusCode.OK);
        await response.WriteAsJsonAsync(filter);
        return response;
    }

    [Function(nameof(MiscFunction2))]
    public async Task<HttpResponseData> MiscFunction2(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestData req,
        [BindQuery] string id,
        [BindQuery] int skip,
        [BindQuery] int? take) // TODO: add support for default values (e.g., take = 10)
    {
        var filter = new FilterDto
        {
            Id = id,
            Skip = skip,
            Take = take ?? 10
        };

        var response = req.CreateResponse(System.Net.HttpStatusCode.OK);
        await response.WriteAsJsonAsync(filter);
        return response;
    }

    public record Person(string Name, int Age);

    public class FilterDto
    {
        public string Id { get; set; } = "";
        public int Skip { get; set; }
        public int Take { get; set; } = 10;
    }
}
