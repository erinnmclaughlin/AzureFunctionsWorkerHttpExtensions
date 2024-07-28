using System.Diagnostics.CodeAnalysis;

namespace Tizzani.AzureFunctionsWorkerHttpExtensions;

internal static class TypeExtensions
{
    public static object? CreateDefaultInstance(this Type type)
    {
        return type.IsValueType ? Activator.CreateInstance(type) : null;
    }

    public static Type GetCollectionElementType(this Type type)
    {
        return type.GetElementType() ?? type.GenericTypeArguments[0];
    }

    public static bool IsNullable(this Type type, [NotNullWhen(true)] out Type? underlyingType)
    {
        underlyingType = Nullable.GetUnderlyingType(type);
        return underlyingType is not null;
    }

    public static bool IsSimpleType(this Type type)
    {
        if (Nullable.GetUnderlyingType(type) is { } underlyingType)
        {
            type = underlyingType;
        }

        return type.IsPrimitive
            || type.IsEnum
            || type == typeof(string)
            || type == typeof(decimal)
            || type == typeof(DateTime)
            || type == typeof(DateTimeOffset)
            || type == typeof(TimeSpan)
            || type == typeof(DateOnly)
            || type == typeof(TimeOnly)
            || type == typeof(Guid);
    }
}
