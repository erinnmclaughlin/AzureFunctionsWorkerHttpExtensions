namespace Tizzani.AzureFunctionsHttpBindingExtensions.Tests;

public partial class QueryStringUtilitiesTests
{
    [Theory]
    [InlineData("?myValue=hello", "hello")]
    [InlineData("?myValue=Hello", "Hello")]
    [InlineData("?myValue=HELLO", "HELLO")]
    [InlineData("", null)]
    public void CanConvertString(string queryString, string expectedValue)
    {
        var value = TestDeserialize<string>(queryString);
        Assert.Equal(expectedValue, value);
    }

    [Theory]
    [InlineData("?myValue=42", 42)]
    [InlineData("?myValue=InvalidInt", 0)]
    [InlineData("", 0)]
    public void CanConvertInteger(string queryString, int expectedValue)
    {
        var value = TestDeserialize<int>(queryString);
        Assert.Equal(expectedValue, value);
    }

    [Theory]
    [InlineData("?myValue=42", 42)]
    [InlineData("?myValue=InvalidInt", null)]
    [InlineData("", null)]
    public void CanConvertNullableInteger(string queryString, int? expectedValue)
    {
        var value = TestDeserialize<int?>(queryString);
        Assert.Equal(expectedValue, value);
    }

    [Theory]
    [InlineData("?myValue=42.0", 42.0)]
    [InlineData("?myValue=42.25", 42.25)]
    [InlineData("?myValue=InvalidDecimal", 0)]
    [InlineData("", 0)]
    public void CanConvertDecimal(string queryString, decimal expectedValue)
    {
        var value = TestDeserialize<decimal>(queryString);
        Assert.Equal(expectedValue, value);
    }

    // https://stackoverflow.com/questions/37854264/having-an-actual-decimal-value-as-parameter-for-an-attribute-example-xunit-net
    [Theory]
    [InlineData("?myValue=42.0", "42.0")]
    [InlineData("?myValue=42.25", "42.25")]
    [InlineData("?myValue=InvalidDecimal", null)]
    [InlineData("", null)]
    public void CanConvertNullableDecimal(string queryString, string? expectedValue)
    {
        var expectedDecimalValue = decimal.TryParse(expectedValue, out var decimalValue) ? decimalValue : (decimal?)null;
        var value = TestDeserialize<decimal?>(queryString);
        Assert.Equal(expectedDecimalValue, value);
    }

    [Theory]
    [InlineData("?myValue=2021-01-01", "2021-01-01")]
    [InlineData("?myValue=InvalidDate", "")]
    [InlineData("", "")]
    public void CanConvertDateTime(string queryString, string expectedValue)
    {
        var expectedDateValue = DateTime.TryParse(expectedValue, out var dateValue) ? dateValue : default;
        var value = TestDeserialize<DateTime>(queryString);
        Assert.Equal(expectedDateValue, value);
    }

    [Theory]
    [InlineData("?myValue=2021-01-01", "2021-01-01")]
    [InlineData("?myValue=InvalidDate", null)]
    [InlineData("", null)]
    public void CanConvertNullableDateTime(string queryString, string? expectedValue)
    {
        var expectedDateValue = DateTime.TryParse(expectedValue, out var dateValue) ? dateValue : (DateTime?)null;
        var value = TestDeserialize<DateTime?>(queryString);
        Assert.Equal(expectedDateValue, value);
    }

    [Theory]
    [InlineData("?myValue=2021-01-01", "2021-01-01")]
    [InlineData("?myValue=InvalidDate", "")]
    [InlineData("", "")]
    public void CanConvertDateOnly(string queryString, string expectedValue)
    {
        var expectedDateValue = DateOnly.TryParse(expectedValue, out var dateValue) ? dateValue : default;
        var value = TestDeserialize<DateOnly>(queryString);
        Assert.Equal(expectedDateValue, value);
    }

    [Theory]
    [InlineData("?myValue=2021-01-01", "2021-01-01")]
    [InlineData("?myValue=InvalidDate", null)]
    [InlineData("", null)]
    public void CanConvertNullableDateOnly(string queryString, string? expectedValue)
    {
        var expectedDateValue = DateOnly.TryParse(expectedValue, out var dateValue) ? dateValue : (DateOnly?)null;
        var value = TestDeserialize<DateOnly?>(queryString);
        Assert.Equal(expectedDateValue, value);
    }

}