using System.Net;

namespace E2ETests;

[Collection("E2E Tests")]
public sealed class PingTest
{
    private readonly HttpClient _appClient = new()
    {
        BaseAddress = new Uri("http://localhost:7120/api/")
    };

    [Fact]
    public async Task Run()
    {
        var response = await _appClient.GetAsync("Ping");
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}
