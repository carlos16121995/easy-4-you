// Copyright (c) 2024 Result Consultoria Empresarial®. All rights reserved.
// Copyright (c) 2024 Startando.com.vc®. All rights reserved.
// PRIVATE SOURCE. Any kind of unauthorized use is prohibited.

using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace E4U.Core.StronglyTypes;

/// <summary>
/// 
/// </summary>
public static class StronglyTypedExtensions
{
    private static readonly ConcurrentDictionary<Type, ValueConverter> StronglyTypedIdConverters = new();

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="propertyBuilder"></param>
    public static void HasStronglyTyped<T>(this PropertyBuilder<T> propertyBuilder) where T : IStronglyTypeId
    {
        IsStronglyTypedId(propertyBuilder.Metadata.ClrType, out var valueType);

        if (valueType is null)
            return;

        var converter = StronglyTypedIdConverters.GetOrAdd(
                       typeof(T),
                        _ => CreateStronglyTypedIdConverter(propertyBuilder.Metadata.Name, typeof(T), valueType));
        propertyBuilder.HasConversion(converter);
    }

    /// <summary>
    /// Creates a value converter for a strongly typed ID.
    /// </summary>
    /// <param name="propertyName"></param>
    /// <param name="stronglyTypedIdType">The strongly typed ID type.</param>
    /// <param name="valueType">The value type.</param>
    /// <returns>A value converter for the strongly typed ID.</returns>
    private static ValueConverter CreateStronglyTypedIdConverter(string propertyName,
        Type stronglyTypedIdType,
        Type valueType)
    {
        // id => id.Value
        var toProviderFuncType = typeof(Func<,>)
            .MakeGenericType(stronglyTypedIdType, valueType);
        var stronglyTypedIdParam = Expression.Parameter(stronglyTypedIdType, propertyName);
        var toProviderExpression = Expression.Lambda(
            toProviderFuncType,
            Expression.Property(stronglyTypedIdParam, "Value"),
            stronglyTypedIdParam);

        // value => new ProductId(value)
        var fromProviderFuncType = typeof(Func<,>)
            .MakeGenericType(valueType, stronglyTypedIdType);
        var valueParam = Expression.Parameter(valueType, "value");
        var ctor = stronglyTypedIdType.GetConstructor([valueType]);
        var fromProviderExpression = Expression.Lambda(
            fromProviderFuncType,
            Expression.New(ctor!, valueParam),
            valueParam);

        var converterType = typeof(ValueConverter<,>)
            .MakeGenericType(stronglyTypedIdType, valueType);

        return (ValueConverter)Activator.CreateInstance(
            converterType,
            toProviderExpression,
            fromProviderExpression,
            null)!;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="type"></param>
    /// <param name="idType"></param>
    /// <returns></returns>
    public static bool IsStronglyTypedId(Type type, [NotNullWhen(true)] out Type? idType)
    {
        ArgumentNullException.ThrowIfNull(type);

        if (type.BaseType is Type baseType &&
            baseType.IsGenericType &&
            baseType.GetGenericTypeDefinition() == typeof(StronglyTypedId<>))
        {
            idType = baseType.GetGenericArguments()[0];
            return true;
        }

        idType = null;
        return false;
    }
}
