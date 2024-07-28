using Microsoft.Azure.Functions.Worker.Converters;
using Tizzani.AzureFunctionsWorkerHttpExtensions.Implementation;

namespace Tizzani.AzureFunctionsWorkerHttpExtensions;

internal class BindQueryConverter : IInputConverter
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