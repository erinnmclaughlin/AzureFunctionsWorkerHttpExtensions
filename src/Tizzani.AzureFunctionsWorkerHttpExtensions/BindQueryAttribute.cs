using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Converters;

namespace Tizzani.AzureFunctionsWorkerHttpExtensions;

/// <summary>
/// Specifies that a parameter should be bound using the HTTP request query string when using the <see cref="HttpTriggerAttribute"/>.
/// </summary>
public class BindQueryAttribute() : InputConverterAttribute(typeof(BindQueryConverter))
{
}
