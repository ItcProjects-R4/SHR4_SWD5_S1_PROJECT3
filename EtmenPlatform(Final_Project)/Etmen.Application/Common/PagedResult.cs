namespace Etmen.Application.Common;

/// <summary>
/// Generic paginated response wrapper.
/// All list endpoints that can return large datasets must use this.
/// </summary>
/// <typeparam name="T">The item type in the page.</typeparam>
public sealed class PagedResult<T>
{
    /// <summary>Items in the current page.</summary>
    public IEnumerable<T> Items      { get; init; } = Enumerable.Empty<T>();

    /// <summary>Total number of records matching the query (all pages combined).</summary>
    public int TotalCount  { get; init; }

    /// <summary>Current page number (1-based).</summary>
    public int Page        { get; init; }

    /// <summary>Number of items per page.</summary>
    public int PageSize    { get; init; }

    /// <summary>Total number of pages.</summary>
    public int TotalPages  => PageSize > 0 ? (int)Math.Ceiling(TotalCount / (double)PageSize) : 0;

    /// <summary>Whether a previous page exists.</summary>
    public bool HasPrevious => Page > 1;

    /// <summary>Whether a next page exists.</summary>
    public bool HasNext     => Page < TotalPages;

    public static PagedResult<T> Create(IEnumerable<T> items, int totalCount, int page, int pageSize)
        => new() { Items = items, TotalCount = totalCount, Page = page, PageSize = pageSize };

    public static PagedResult<T> Empty(int page = 1, int pageSize = 20)
        => new() { Items = Enumerable.Empty<T>(), TotalCount = 0, Page = page, PageSize = pageSize };
}

/// <summary>Pagination query parameters — passed to all list queries.</summary>
public sealed class PaginationParams
{
    private const int MaxPageSize = 100;

    private int _pageSize = 20;

    /// <summary>Page number (1-based). Defaults to 1.</summary>
    public int Page     { get; init; } = 1;

    /// <summary>Items per page. Max 100. Defaults to 20.</summary>
    public int PageSize
    {
        get => _pageSize;
        init => _pageSize = value > MaxPageSize ? MaxPageSize : value < 1 ? 20 : value;
    }
}
