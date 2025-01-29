// Copyright (c) 2024 Result Consultoria Empresarial®. All rights reserved.
// Copyright (c) 2024 Startando.com.vc®. All rights reserved.
// PRIVATE SOURCE. Any kind of unauthorized use is prohibited.

using E4U.Core.Extensions;

using Microsoft.EntityFrameworkCore;

namespace E4U.Core.Paginations;

/// <summary>
/// Extensão para paginação
/// </summary>
public static class PaginationExtension
{
    /// <summary>
    /// Realiza a paginação de uma lista
    /// </summary>
    /// <typeparam name="TResponse"></typeparam>
    /// <typeparam name="T"></typeparam>
    /// <param name="query"></param>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static async Task<PaginationResult<T>> GetPagedListAsync<TResponse, T>(this IQueryable<T> query, PaginationQuery request, CancellationToken cancellationToken) where TResponse : PaginationResult<T>, new()
    {
        var response = new TResponse();
        var count = await query.CountAsync(cancellationToken).ConfigureAwait(false);

        request.OrderByProperty ??= "Id";

        if (request.OrderByProperty.StartsWith('-'))
            query = query.OrderByPropertyDescending(request.OrderByProperty[1..]);
        else
            query = query.OrderByProperty(request.OrderByProperty);

        response.TotalPages = (int)Math.Round((decimal)count / request.PageSize, mode: MidpointRounding.ToPositiveInfinity);
        response.TotalRegisters = count;
        var finalQuery = query
                               .Skip((request.Page - 1) * request.PageSize)
                               .Take(request.PageSize);


        var queryString = finalQuery.ToQueryString();

        Console.WriteLine(queryString);

        response.Data = await finalQuery
                                .ToListAsync(cancellationToken).ConfigureAwait(false);
        return response;
    }

    /// <summary>
    /// Realiza a paginação de uma lista
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="query"></param>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static Task<PaginationResult<T>> GetPagedListAsync<T>(this IQueryable<T> query, PaginationQuery request, CancellationToken cancellationToken) => query.GetPagedListAsync<PaginationResult<T>, T>(request, cancellationToken);
}
