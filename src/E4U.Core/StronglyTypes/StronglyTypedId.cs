// Copyright (c) 2024 Result Consultoria Empresarial®. All rights reserved.
// Copyright (c) 2024 Startando.com.vc®. All rights reserved.
// PRIVATE SOURCE. Any kind of unauthorized use is prohibited.

namespace E4U.Core.StronglyTypes;

/// <summary>
/// Identificadores Fortemente tipados
/// </summary>
public interface IStronglyTypeId;

/// <summary>
/// Identificadores Fortemente tipados
/// </summary>
public abstract record StronglyTypedId<T>(T Value) : IStronglyTypeId
    where T : notnull
{
    /// <summary>
    /// Returns a string that represents the current object.
    /// </summary>
    /// <returns>
    /// A string that represents the current value.
    /// </returns>
    public override string ToString() => Value.ToString()!;
}
