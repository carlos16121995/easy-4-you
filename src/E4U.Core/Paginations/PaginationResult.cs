// Copyright (c) 2024 Result Consultoria Empresarial®. All rights reserved.
// Copyright (c) 2024 Startando.com.vc®. All rights reserved.
// PRIVATE SOURCE. Any kind of unauthorized use is prohibited.

namespace E4U.Core.Paginations;

/// <summary>
/// Classe base para os results paginados
/// </summary>
public class PaginationResult<TData>
{
    /// <summary>
    /// Resposta
    /// </summary>
    public IEnumerable<TData>? Data { get; set; }

    /// <summary>
    /// Total de respostas por página
    /// </summary>
    public int TotalPages { get; set; }

    /// <summary>
    /// Total de respostas
    /// </summary>
    public int TotalRegisters { get; set; }
}
