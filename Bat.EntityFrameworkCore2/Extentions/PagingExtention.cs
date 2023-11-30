namespace Bat.EntityFrameworkCore;

public static class PagingExtention
{
    public static async Task<PagingListDetails<T>> ToPagingListDetailsAsync<T>(this IQueryable<T> source, PagingParameter pagingParameter, CancellationToken cancellationToken = default)
    {
        var count = await source.CountAsync(cancellationToken);
        var ps = pagingParameter.PageSize == 0 ? 10 : pagingParameter.PageSize;
        var pn = pagingParameter.PageNumber == 0 ? 1 : pagingParameter.PageNumber;
        var sourceList = await source.Skip((pn - 1) * ps).Take(ps).ToListAsync(cancellationToken);

        var list = new PagingList<T>(sourceList, count, new PagingParameter { PageNumber = pn, PageSize = ps });
        return new PagingListDetails<T>
        {
            Items = list,

            PageNumber = list.PagingDetails.PageNumber,
            PageSize = list.PagingDetails.PageSize,
            TotalPages = list.PagingDetails.TotalPages,
            TotalCount = list.PagingDetails.TotalCount,
            HasPrevious = list.PagingDetails.HasPrevious,
            HasNext = list.PagingDetails.HasNext
        };
    }

}