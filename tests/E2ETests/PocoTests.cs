using System.Net.Http.Json;

namespace E2ETests;

[Collection("E2E Tests")]
public sealed class PocoTests
{
    private static readonly HttpClient _appClient = new()
    {
        BaseAddress = new Uri("http://localhost:7120/api/")
    };

    record SimplePoco(string Name);

    [Fact]
    public async Task SimplePocoTest()
    {
        var response = await _appClient.GetFromJsonAsync<SimplePoco>("EchoSimplePoco?name=Hello");

        Assert.NotNull(response);
        Assert.Equal("Hello", response.Name);
    }
}
