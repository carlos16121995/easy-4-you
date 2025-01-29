// Copyright (c) 2024 Result Consultoria Empresarial®. All rights reserved.
// Copyright (c) 2024 Startando.com.vc®. All rights reserved.
// PRIVATE SOURCE. Any kind of unauthorized use is prohibited.

namespace E4U.Core.Paginations;

/// <summary>
/// Query para paginação
/// </summary>
public class PaginationQuery
{
    /// <summary>
    /// Página a ser acessada
    /// </summary>
    public int Page { get; set; } = 1;

    /// <summary>
    /// Tamanho da página, Quantidade de itens por página. Default = 10
    /// </summary>
    public int PageSize { get; set; } = 10;

    /// <summary>
    /// Propriedade da lista que será ordenada
    /// </summary>
    public virtual string OrderByProperty { get; set; } = "Id";
}
