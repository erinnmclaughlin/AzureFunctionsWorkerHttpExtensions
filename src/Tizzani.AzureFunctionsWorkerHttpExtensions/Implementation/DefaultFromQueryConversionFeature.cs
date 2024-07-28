using Azure.Core.Serialization;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;

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
        if (targetType.IsSimpleType())
        {
            if (source is null)
            {
                return targetType.CreateDefaultInstance();
            }

            var typeConverter = TypeDescriptor.GetConverter(targetType);
            return typeConverter.IsValid(source) ? typeConverter.ConvertFrom(source) : targetType.CreateDefaultInstance();
        }

        var serializer = context.InstanceServices.GetService<IOptions<WorkerOptions>>()?.Value?.Serializer
            ?? throw new InvalidOperationException("A serializer is not configured for the worker.");

        return Convert(requestData.Query, targetType, serializer);
    }

    private static object? Convert(NameValueCollection query, Type targetType, ObjectSerializer serializer)
    {
        if (query.AllKeys.Length == 0)
        {
            return targetType.CreateDefaultInstance();
        }

        if (targetType.IsSimpleType())
        {
            return query[query.AllKeys[0]] is { } source ? ConvertSimpleType(targetType, source) : targetType.CreateDefaultInstance();
        }

        if (typeof(IEnumerable).IsAssignableFrom(targetType))
        {
            var collection = ConvertCollectionType(targetType, query.GetValues(query.AllKeys[0]) ?? []);
            var collectionStream = serializer.Serialize(collection).ToStream();
            return serializer.Deserialize(collectionStream, targetType, default);
        }

        var objectDictionary = GetObjectDictionary(query, targetType);
        var objectStream = serializer.Serialize(objectDictionary).ToStream();
        var obj = serializer.Deserialize(objectStream, targetType, default);

        return obj;
    }

    private static Dictionary<string, object?> GetObjectDictionary(NameValueCollection query, Type targetType)
    {
        var dict = new Dictionary<string, object?>(StringComparer.OrdinalIgnoreCase);
        const BindingFlags bindingFlags = BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance;

        foreach (var propertyName in query.AllKeys)
        {
            // Skip if the target type does not contain a property with a matching name:
            if (string.IsNullOrEmpty(propertyName) || targetType.GetProperty(propertyName, bindingFlags)?.PropertyType is not { } propertyType)
            {
                continue;
            }

            // Handle simple types:
            if (propertyType.IsSimpleType())
            {
                dict.Add(propertyName, ConvertSimpleType(propertyType, query[propertyName]));
                continue;
            }

            // Handle collection types:
            if (typeof(IEnumerable).IsAssignableFrom(propertyType))
            {
                dict.Add(propertyName, ConvertCollectionType(propertyType, query.GetValues(propertyName) ?? []));
                continue;
            }

            // TODO: Handle complex types
            // ..
        }

        return dict;
    }

    private static object? ConvertSimpleType(Type targetType, object? valueToConvert)
    {
        var stringValue = valueToConvert?.ToString();

        if (targetType == typeof(string))
        {
            return stringValue;
        }

        if (string.IsNullOrEmpty(stringValue))
        {
            return targetType.CreateDefaultInstance();
        }

        var typeConverter = TypeDescriptor.GetConverter(targetType);

        if (typeConverter.IsValid(stringValue))
        {
            return typeConverter.ConvertFromString(null, CultureInfo.InvariantCulture, stringValue);
        }

        return targetType.CreateDefaultInstance();
    }

    private static IEnumerable? ConvertCollectionType(Type targetCollectionType, string[] arrayValues)
    {
        var elementType = targetCollectionType.GetCollectionElementType();

        // Handle collections of simple types:
        if (elementType.IsSimpleType())
        {
            return arrayValues.Select(p => ConvertSimpleType(elementType, p));
        }

        // TODO: Handle collections of complex types
        // ..

        return null;
    }
}