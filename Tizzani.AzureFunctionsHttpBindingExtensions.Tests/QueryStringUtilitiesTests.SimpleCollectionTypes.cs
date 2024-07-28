namespace Tizzani.AzureFunctionsHttpBindingExtensions.Tests;

public partial class QueryStringUtilitiesTests
{
    [Theory]
    [InlineData("", null)]
    [InlineData("?myValue=hello", "hello")]
    [InlineData("?myValue=hello&myValue=world", "hello", "world")]
    public void CanConvertStringArray(string queryString, params string[] expectedValue)
    {
        var value = TestDeserialize<string[]>(queryString);
        Assert.Equal(expectedValue, value);
    }

    [Theory]
    [InlineData("", null)]
    [InlineData("?myValue=hello", "hello")]
    [InlineData("?myValue=hello&myValue=world", "hello", "world")]
    public void CanConvertStringList(string queryString, params string[] expectedValue)
    {
        var value = TestDeserialize<List<string>>(queryString);
        Assert.Equal(expectedValue, value);
    }

    [Theory(Skip = "TODO: Support more collection types")]
    [InlineData("", null)]
    [InlineData("?myValue=hello", "hello")]
    [InlineData("?myValue=hello&myValue=world", "hello", "world")]
    public void CanConvertStringHashSet(string queryString, params string[] expectedValue)
    {
        var value = TestDeserialize<HashSet<string>>(queryString);
        Assert.Equal(expectedValue, value);
    }
}
