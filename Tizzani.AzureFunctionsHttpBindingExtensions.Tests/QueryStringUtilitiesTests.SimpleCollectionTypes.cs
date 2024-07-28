namespace Tizzani.AzureFunctionsHttpBindingExtensions.Tests;

public partial class QueryStringUtilitiesTests
{
    [Theory(Skip = "TODO: Support collection types")]
    [InlineData("")]
    [InlineData("?myValue=hello", "hello")]
    [InlineData("?myValue=hello&myValue=world", "hello", "world")]
    public void CanConvertStringArray(string queryString, params string[] expectedValue)
    {
        var value = TestDeserialize<string[]>(queryString);
        Assert.Equal(expectedValue, value);
    }
}
