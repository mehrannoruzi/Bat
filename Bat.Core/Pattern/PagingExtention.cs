namespace Bat.Core;

public static class PagingExtention
{
    public static PagingListDetails<T> ToPagingListDetails<T>(this IQueryable<T> source, PagingParameter pagingParameter)
    {
        var count = source.Count();
        var ps = pagingParameter.PageSize == 0 ? 10 : pagingParameter.PageSize;
        var pn = pagingParameter.PageNumber == 0 ? 1 : pagingParameter.PageNumber;
        var sourceList = source.Skip((pn - 1) * ps).Take(ps).ToList();

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

    public static PagingListDetails<T> ToPagingListDetails<T>(this IEnumerable<T> source, PagingParameter pagingParameter)
    {
        var count = source.Count();
        var ps = pagingParameter.PageSize == 0 ? 10 : pagingParameter.PageSize;
        var pn = pagingParameter.PageNumber == 0 ? 1 : pagingParameter.PageNumber;
        var sourceList = source.Skip((pn - 1) * ps).Take(ps).ToList();

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