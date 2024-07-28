using Tizzani.AzureFunctionsWorkerHttpExtensions.Tests;

namespace Tizzani.AzureFunctionsHttpBindingExtensions.Tests;

public partial class QueryStringUtilitiesTests
{
    [Theory]
    [InlineData("")]
    [InlineData("?myValue=hello", "hello")]
    [InlineData("?myValue=hello&myValue=hello", "hello", "hello")]
    [InlineData("?myValue=hello&myValue=world", "hello", "world")]
    public void CanConvertStringArray(string queryString, params string[] expectedValue)
    {
        var value = TestDeserialize<string[]>(queryString);

        if (expectedValue.Length == 0)
        {
            Assert.Null(value);
        }
        else
        {
            Assert.Equal(expectedValue, value);
        }
    }

    [Theory]
    [InlineData("")]
    [InlineData("?myValue=hello", "hello")]
    [InlineData("?myValue=hello&myValue=world", "hello", "world")]
    public void CanConvertStringList(string queryString, params string[] expectedValue)
    {
        var value = TestDeserialize<List<string>>(queryString);

        if (expectedValue.Length == 0)
        {
            Assert.Null(value);
        }
        else
        {
            Assert.Equal(expectedValue, value);
        }
    }

    [Theory]
    [InlineData("")]
    [InlineData("?myValue=hello", "hello")]
    [InlineData("?myValue=hello&myValue=world", "hello", "world")]
    public void CanConvertStringHashSet(string queryString, params string[] expectedValue)
    {
        var value = TestDeserialize<HashSet<string>>(queryString);
        
        if (expectedValue.Length == 0)
        {
            Assert.Null(value);
        }
        else
        {
            Assert.Equal(expectedValue, value);
        }
    }

    [Theory]
    [InlineData("")]
    [InlineData("?myValue=42", 42)]
    [InlineData("?myValue=42&myValue=42", 42, 42)]
    [InlineData("?myValue=42&myValue=24", 42, 24)]
    public void CanConvertIntegerArray(string queryString, params int[] expectedValue)
    {
        var value = TestDeserialize<int[]>(queryString);

        if (expectedValue.Length == 0)
        {
            Assert.Null(value);
        }
        else
        {
            Assert.Equal(expectedValue, value);
        }
    }

    [Theory]
    [InlineData("")]
    [InlineData("?myValue=42", 42)]
    [InlineData("?myValue=42&myValue=24", 42, 24)]
    public void CanConvertIntegerList(string queryString, params int[] expectedValue)
    {
        var value = TestDeserialize<List<int>>(queryString);

        if (expectedValue.Length == 0)
        {
            Assert.Null(value);
        }
        else
        {
            Assert.Equal(expectedValue, value);
        }
    }

    [Theory]
    [InlineData("")]
    [InlineData("?myValue=Value2", TestEnum.Value2)]
    [InlineData("?myValue=Value2&myValue=Value2", TestEnum.Value2, TestEnum.Value2)]
    [InlineData("?myValue=Value1&myValue=Value2", TestEnum.Value1, TestEnum.Value2)]
    public void CanConvertEnumArray(string queryString, params TestEnum[] expectedValue)
    {
        var value = TestDeserialize<TestEnum[]>(queryString);

        if (expectedValue.Length == 0)
        {
            Assert.Null(value);
        }
        else
        {
            Assert.Equal(expectedValue, value);
        }
    }

    [Fact(Skip = "TODO: Support query strings with indexed collections")]
    public void CanConvertOrderedArray()
    {
        const string queryString = "?myValue[1]=world&myValue[0]=hello";
        var value = TestDeserialize<string[]>(queryString);
        Assert.Equal(["hello", "world"], value);
    }
}
