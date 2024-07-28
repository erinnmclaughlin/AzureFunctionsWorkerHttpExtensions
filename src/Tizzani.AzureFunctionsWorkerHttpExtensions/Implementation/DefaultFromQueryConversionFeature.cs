using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Tizzani.AzureFunctionsWorkerHttpExtensions.Implementation;

internal class DefaultFromQueryConversionFeature : IQueryStringConversionFeature
{
    internal static IQueryStringConversionFeature Instance { get; } = new DefaultFromQueryConversionFeature();

    public ValueTask<object?> ConvertAsync(FunctionContext context, Type targetType, object? source)
    {
        var requestData = context.GetHttpRequestDataAsync();

        if (requestData.IsCompletedSuccessfully)
        {
            return new ValueTask<object?>(Convert(context, requestData.Result, targetType, source));
        }

        return ConvertAsync(context, requestData, targetType, source);
    }

    private async ValueTask<object?> ConvertAsync(FunctionContext context, ValueTask<HttpRequestData?> requestDataResult, Type targetType, object? source)
    {
        var requestData = await requestDataResult;
        return Convert(context, requestData, targetType, source);
    }

    private object? Convert(FunctionContext context, HttpRequestData? requestData, Type targetType, object? source)
    {
        if (requestData is null)
        {
            throw new InvalidOperationException($"The '{nameof(DefaultFromQueryConversionFeature)} expects an '{nameof(HttpRequestData)}' instance in the current context.");
        }

        return ConvertQuery(context, requestData, targetType, source);
    }

    private static object? ConvertQuery(FunctionContext context, HttpRequestData requestData, Type targetType, object? source)
    {
        var serializer = context.InstanceServices.GetService<IOptions<WorkerOptions>>()?.Value?.Serializer
            ?? throw new InvalidOperationException("A serializer is not configured for the worker.");

        return QueryStringUtilities.Deserialize(requestData.Query, targetType, serializer);
    }
}