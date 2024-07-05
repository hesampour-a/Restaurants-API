namespace U4.Application.Common;

public class PagedResult<T>
{
    public PagedResult(IEnumerable<T> items, int totalItemsCount, int pageSize, int pageNumber)
    {
        Items = items;
        TotalPages = (int)Math.Ceiling(totalItemsCount / (double)pageSize);
        TotalItemsCount = totalItemsCount;
        ItemsFrom = pageSize * (pageNumber - 1) + 1;
        ItemsTo = ItemsFrom + pageSize - 1;
    }

    public IEnumerable<T> Items { get; set; } 
    public int TotalPages { get; set; }
    public int TotalItemsCount { get; set; }
    public int ItemsFrom { get; set; }
    public int ItemsTo { get; set; }
}
