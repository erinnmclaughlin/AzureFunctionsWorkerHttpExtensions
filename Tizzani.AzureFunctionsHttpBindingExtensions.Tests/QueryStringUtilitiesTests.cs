using Azure.Core.Serialization;
using Microsoft.Azure.Functions.Worker.Extensions.Http;

namespace Tizzani.AzureFunctionsHttpBindingExtensions.Tests;

public partial class QueryStringUtilitiesTests
{
    private static readonly ObjectSerializer _serializer = new JsonObjectSerializer();

    private static T? TestDeserialize<T>(string queryString)
    {
        return QueryStringUtilities.Deserialize<T>(queryString, _serializer);
    }
}
