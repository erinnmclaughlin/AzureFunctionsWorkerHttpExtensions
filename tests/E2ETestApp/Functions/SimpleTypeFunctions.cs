﻿using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using System.Net;
using Tizzani.AzureFunctionsWorkerHttpExtensions;

namespace E2ETestApp.Functions;

public sealed class SimpleTypeFunctions
{
    [Function(nameof(EchoStringValue))]
    public async Task<HttpResponseData> EchoStringValue(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestData req,
        [BindQuery] string value)
    {
        var response = req.CreateResponse(HttpStatusCode.OK);
        await response.WriteAsJsonAsync(value);
        return response;
    }

    [Function(nameof(EchoIntegerValue))]
    public async Task<HttpResponseData> EchoIntegerValue(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestData req,
        [BindQuery] int value)
    {
        var response = req.CreateResponse(HttpStatusCode.OK);
        await response.WriteAsJsonAsync(value);
        return response;
    }

    [Function(nameof(EchoNullableIntegerValue))]
    public async Task<HttpResponseData> EchoNullableIntegerValue(
    [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestData req,
    [BindQuery] int? value)
    {
        var response = req.CreateResponse(HttpStatusCode.OK);
        await response.WriteAsJsonAsync(value);
        return response;
    }

    [Function(nameof(EchoDateTimeValue))]
    public async Task<HttpResponseData> EchoDateTimeValue(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestData req,
        [BindQuery] DateTime value)
    {
        var response = req.CreateResponse(HttpStatusCode.OK);
        await response.WriteAsJsonAsync(value);
        return response;
    }

    [Function(nameof(EchoNullableDateTimeValue))]
    public async Task<HttpResponseData> EchoNullableDateTimeValue(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestData req,
        [BindQuery] DateTime? value)
    {
        var response = req.CreateResponse(HttpStatusCode.OK);
        await response.WriteAsJsonAsync(value);
        return response;
    }

    [Function(nameof(EchoMultipleValues))]
    public async Task<HttpResponseData> EchoMultipleValues(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestData req,
        [BindQuery] string value1,
        [BindQuery] int value2)
    {
        var response = req.CreateResponse(HttpStatusCode.OK);
        await response.WriteAsJsonAsync(new { value1, value2 });
        return response;
    }
}
