﻿using System.Net;
using System.Net.Http.Json;

namespace E2ETests;

[Collection("E2E Tests")]
public sealed class SimpleTypeTests
{
    private static readonly HttpClient _appClient = new()
    {
        BaseAddress = new Uri("http://localhost:7120/api/")
    };

    [Theory]
    [InlineData("", null)]
    [InlineData("?value=Mia", "Mia")]
    [InlineData("?otherValue=Mia", null)]
    public async Task EchoStringValueTests(string queryString, string? expectedBody)
    {
        var response = await _appClient.GetAsync("EchoStringValue" + queryString);
        var responseBody = await response.Content.ReadFromJsonAsync<string>();

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal(expectedBody, responseBody);
    }

    [Theory]
    [InlineData("", 0)]
    [InlineData("?value=42", 42)]
    [InlineData("?otherValue=42", 0)]
    public async Task EchoIntegerValueTests(string queryString, int expectedBody)
    {
        var response = await _appClient.GetAsync("EchoIntegerValue" + queryString);
        var responseBody = await response.Content.ReadFromJsonAsync<int>();
        
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal(expectedBody, responseBody);
    }

    [Theory]
    [InlineData("", null)]
    [InlineData("?value=42", 42)]
    [InlineData("?otherValue=42", null)]
    public async Task EchoNullableIntegerValueTests(string queryString, int? expectedBody)
    {
        var response = await _appClient.GetAsync("EchoNullableIntegerValue" + queryString);
        var responseBody = await response.Content.ReadFromJsonAsync<int?>();

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal(expectedBody, responseBody);
    }
}