using System.Net;
using System.Net.Http.Json;

namespace E2ETests;

public sealed class CollectionTypeTests
{
    private static readonly HttpClient _appClient = new()
    {
        BaseAddress = new Uri("http://localhost:7120/api/")
    };

    [Theory]
    [InlineData("")]
    [InlineData("?value=1&value=2&value=3", 1, 2, 3)]
    [InlineData("?value=1,2,3", 1, 2, 3, Skip ="TODO: Support this collection style")]
    public async Task EchoIntegerArrayTests(string queryString, params int[] expectedValues)
    {
        var response = await _appClient.GetAsync("EchoIntegerArray" + queryString);
        var responseBody = await response.Content.ReadFromJsonAsync<int[]>();

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        if (expectedValues.Length == 0)
        {
            Assert.Null(responseBody);
        }
        else
        {
            Assert.Equal(expectedValues, responseBody);
        }
    }

    [Theory]
    [InlineData("")]
    [InlineData("?value=1&value=2&value=3", 1, 2, 3)]
    [InlineData("?value=1,2,3", 1, 2, 3, Skip = "TODO: Support this collection style")]
    public async Task EchoIntegerListTests(string queryString, params int[] expectedValues)
    {
        var response = await _appClient.GetAsync("EchoIntegerList" + queryString);
        var responseBody = await response.Content.ReadFromJsonAsync<List<int>>();

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        if (expectedValues.Length == 0)
        {
            Assert.Null(responseBody);
        }
        else
        {
            Assert.Equal(expectedValues, responseBody);
        }
    }
}
