// Copyright (c) 2024 Result Consultoria Empresarial®. All rights reserved.
// Copyright (c) 2024 Startando.com.vc®. All rights reserved.
// PRIVATE SOURCE. Any kind of unauthorized use is prohibited.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace E4U.Core.Extensions;

/// <summary>
/// Represa uma extensão do método linq
/// </summary>
public static class LinqExtension
{
    /// <summary>
    /// Orderna um resultado de forma ascendente pelo nome de uma propriedade da entidade
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="query"></param>
    /// <param name="propertyName"></param>
    /// <returns></returns>
    public static IQueryable<T> OrderByProperty<T>(this IQueryable<T> query, string propertyName)
    {
        if (typeof(T).GetProperty(propertyName, BindingFlags.IgnoreCase |
            BindingFlags.Public | BindingFlags.Instance) == null)
            return query;

        ParameterExpression parameterExpression = Expression.Parameter(typeof(T));
        Expression orderByProperty = Expression.Property(parameterExpression, propertyName);
        LambdaExpression lambda = Expression.Lambda(orderByProperty, parameterExpression);
        MethodInfo genericMethod = OrderByMethod.MakeGenericMethod(typeof(T), orderByProperty.Type);
        object ret = genericMethod.Invoke(null, new object[] { query, lambda })!;
        return (IQueryable<T>)ret!;
    }

    /// <summary>
    /// Orderna um resultado de forma descendente pelo nome de uma propriedade da entidade
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="query"></param>
    /// <param name="propertyName"></param>
    /// <returns></returns>
    public static IQueryable<T> OrderByPropertyDescending<T>(this IQueryable<T> query, string propertyName)
    {
        if (typeof(T).GetProperty(propertyName, BindingFlags.IgnoreCase |
            BindingFlags.Public | BindingFlags.Instance) == null)
            return query;

        ParameterExpression parameterExpression = Expression.Parameter(typeof(T));
        Expression orderByProperty = Expression.Property(parameterExpression, propertyName);
        LambdaExpression lambda = Expression.Lambda(orderByProperty, parameterExpression);
        MethodInfo genericMethod = OrderByDescendingMethod.MakeGenericMethod(typeof(T), orderByProperty.Type);
        object ret = genericMethod.Invoke(null, new object[] { query, lambda })!;
        return (IQueryable<T>)ret!;
    }

    /// <summary>
    /// Recupera o método Queryble.OrderBy
    /// </summary>
    private static readonly MethodInfo OrderByMethod =
    typeof(Queryable).GetMethods()
        .Single(method => method.Name == "OrderBy" && method.GetParameters().Length == 2);

    /// <summary>
    /// Recupera o método Queryble.OrderByDescending
    /// </summary>
    private static readonly MethodInfo OrderByDescendingMethod =
        typeof(Queryable).GetMethods()
            .Single(method => method.Name == "OrderByDescending" && method.GetParameters().Length == 2);

}
