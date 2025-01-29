// Copyright (c) 2024 Result Consultoria Empresarial®. All rights reserved.
// Copyright (c) 2024 Startando.com.vc®. All rights reserved.
// PRIVATE SOURCE. Any kind of unauthorized use is prohibited.

using System.ComponentModel;
using System.Reflection;

namespace E4U.Core.Extensions;

/// <summary>
/// Provides extension methods for enumerations.
/// </summary>
public static class EnumExtensions
{
    /// <summary>
    /// Retrieves the description attribute of an enumeration value.
    /// </summary>
    /// <param name="value">The enumeration value.</param>
    /// <returns>The description of the enumeration value, or the value's name if no description is found.</returns>
    public static string GetDescription(this Enum value)
    {
        FieldInfo? field = value.GetType().GetField(value.ToString());
        return field != null && Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) is DescriptionAttribute attribute
            ? attribute.Description
            : value.ToString();
    }
}

