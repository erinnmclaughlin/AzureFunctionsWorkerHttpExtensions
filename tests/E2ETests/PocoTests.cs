using System.Net.Http.Json;
using System.Text;

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
    
    record PocoWithCollection(string Name, string[] Nicknames);

    [Theory]
    [InlineData("Erin")]
    [InlineData("John", "Johnny")]
    [InlineData("Benjamin", "Ben", "Benny")]
    public async Task PocoWithCollectionTests(string name, params string[] nicknames)
    {
        var queryStringBuilder = new StringBuilder($"?name={name}");

        foreach (var nickname in nicknames)
            queryStringBuilder.Append($"&nicknames={nickname}");

        var response = await _appClient.GetFromJsonAsync<PocoWithCollection>($"EchoPocoWithCollection{queryStringBuilder}");

        Assert.NotNull(response);
        Assert.Equal(name, response.Name);

        if (nicknames.Length == 0)
        {
            Assert.Null(response.Nicknames);
        }
        else
        {
            Assert.Equivalent(nicknames, response.Nicknames);
        }
    }
}
