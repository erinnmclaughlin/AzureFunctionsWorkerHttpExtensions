using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using System.Net;

namespace E2ETestApp.Functions;

public sealed class PingFunction
{
    [Function(nameof(Ping))]
    public HttpResponseData Ping(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequestData req)
    {
        var response = req.CreateResponse(HttpStatusCode.OK);
        response.WriteString("Pong");
        return response;
    }
}
