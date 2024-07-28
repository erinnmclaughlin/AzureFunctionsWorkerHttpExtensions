﻿using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Converters;

namespace Tizzani.AzureFunctionsHttpBindingExtensions;

/// <summary>
/// Specifies that a parameter should be bound using the HTTP request query string when using the <see cref="HttpTriggerAttribute"/>.
/// </summary>
public class BindQueryAttribute() : InputConverterAttribute(typeof(BindQueryConverter))
{
    public ValueTask<ConversionResult> ConvertAsync(ConverterContext context)
    {
        var queryConversionFeature = context.FunctionContext.Features.Get<IQueryStringConversionFeature>()
            ?? DefaultFromQueryConversionFeature.Instance;

        var result = queryConversionFeature.ConvertAsync(context.FunctionContext, context.TargetType, context.Source);

        if (result.IsCompletedSuccessfully)
        {
            return new ValueTask<ConversionResult>(ConversionResult.Success(result.Result));
        }

        return HandleResultAsync(result);
    }

    private async ValueTask<ConversionResult> HandleResultAsync(ValueTask<object?> result)
    {
        var bodyResult = await result;
        return ConversionResult.Success(bodyResult);
    }
}
