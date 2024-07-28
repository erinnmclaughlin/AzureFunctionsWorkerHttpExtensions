using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using System.Net;
using Tizzani.AzureFunctionsWorkerHttpExtensions;

namespace E2ETestApp;

public sealed class TestFunctions
{
    [Function(nameof(SayHello))]
    public HttpResponseData SayHello(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequestData req,
        [BindQuery] string name)
    {
        var response = req.CreateResponse(HttpStatusCode.OK);
        response.WriteString($"Hello, {name}");
        return response;
    }

    public record CallerName(string Name);

    [Function(nameof(SayHelloWithPoco))]
    public HttpResponseData SayHelloWithPoco(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequestData req,
        [BindQuery] CallerName? caller)
    {
        var response = req.CreateResponse(HttpStatusCode.OK);
        response.WriteString($"Hello, {caller?.Name}");
        return response;
    }

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

}
