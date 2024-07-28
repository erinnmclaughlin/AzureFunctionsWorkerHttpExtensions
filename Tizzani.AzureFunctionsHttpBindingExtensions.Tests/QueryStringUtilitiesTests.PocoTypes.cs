using Azure.Core.Serialization;
using Microsoft.Azure.Functions.Worker.Extensions.Http;
using System.Text.Json;

namespace Tizzani.AzureFunctionsHttpBindingExtensions.Tests;

public partial class QueryStringUtilitiesTests
{
    [Fact]
    public void CanConvertSimplePoco()
    {
        const string queryString = "?MyValue=hello";
        var value = TestDeserialize<SimplePocoClass>(queryString);

        Assert.NotNull(value);
        Assert.Equal("hello", value.MyValue);
    }

    [Fact]
    public void CanConvertSimplePoco_WhenJsonOptionsAreCaseInsensitive()
    {
        const string queryString = "?myvalue=hello";

        var serializer = new JsonObjectSerializer(new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        var value = QueryStringUtilities.Deserialize<SimplePocoClass>(queryString, serializer);

        Assert.NotNull(value);
        Assert.Equal("hello", value.MyValue);
    }

    [Fact]
    public void CanConvertSimplePoco_WhenJsonOptionsAreCaseSensitive()
    {
        const string queryString = "?myvalue=hello";

        var serializer = new JsonObjectSerializer(new JsonSerializerOptions { PropertyNameCaseInsensitive = false });
        var value = QueryStringUtilities.Deserialize<SimplePocoClass>(queryString, serializer);

        Assert.NotNull(value);
        Assert.Equal("", value.MyValue); // property should be ignored because of case sensitivity
    }

    [Fact]
    public void CanConvertPocoWithCollection()
    {
        const string queryString = "?MyValues=42&MyValues=24";

        var value = TestDeserialize<PocoClassWithCollection>(queryString);
        Assert.NotNull(value);
        Assert.Equal([42, 24], value.MyValues);
    }
}
