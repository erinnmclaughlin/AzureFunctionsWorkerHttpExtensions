using Azure.Core.Serialization;
using Tizzani.AzureFunctionsWorkerHttpExtensions.Implementation;

namespace Tizzani.AzureFunctionsHttpBindingExtensions.Tests;

public partial class QueryStringUtilitiesTests
{
    private static readonly ObjectSerializer _serializer = new JsonObjectSerializer();

    private static T? TestDeserialize<T>(string queryString)
    {
        return QueryStringUtilities.Deserialize<T>(queryString, _serializer);
    }
}
