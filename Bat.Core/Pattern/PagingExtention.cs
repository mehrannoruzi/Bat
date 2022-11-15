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

    public static PagingListDetails<T> ToPagingListDetails<T>(this IEnumerable<T> source, int count, PagingParameter pagingParameter)
    {
        var result = new PagingListDetails<T>
        {
            Items = new PagingList<T>(source.ToList(), count, pagingParameter),

            TotalCount = count,
            PageSize = pagingParameter.PageSize,
            PageNumber = pagingParameter.PageNumber,
            TotalPages = (int)Math.Ceiling(count / (double)pagingParameter.PageSize)
        };
        result.HasNext = pagingParameter.PageNumber < result.TotalPages;
        result.HasPrevious = result.PageNumber > 1;

        return result;
    }

    public static PagingListDetails<T> ToPagingListModel<T>(this IEnumerable<T> source, PagingParameter pagingParameter)
    {
        PagingList<T> pagingList = new(source.ToList(), source.Count(), pagingParameter);

        return new PagingListDetails<T>
        {
            Items = pagingList,
            TotalCount = source.Count(),
            TotalPages = (int)Math.Ceiling(source.Count() / (double)pagingParameter.PageSize),
            PageNumber = pagingParameter.PageNumber,
            PageSize = pagingParameter.PageSize,
            HasPrevious = pagingParameter.PageNumber > 1,
            HasNext = pagingParameter.PageNumber < (int)Math.Ceiling(source.Count() / (double)pagingParameter.PageSize)
        };
    }

    public static PagingListDetails<T> ToPagingListModel<T>(this IEnumerable<T> source, int count, PagingParameter pagingParameter)
    {
        PagingList<T> pagingList = new(source.ToList(), count, pagingParameter);

        return new PagingListDetails<T>
        {
            Items = pagingList,
            TotalCount = count,
            TotalPages = (int)Math.Ceiling(count / (double)pagingParameter.PageSize),
            PageNumber = pagingParameter.PageNumber,
            PageSize = pagingParameter.PageSize,
            HasPrevious = pagingParameter.PageNumber > 1,
            HasNext = pagingParameter.PageNumber < (int)Math.Ceiling(count / (double)pagingParameter.PageSize)
        };
    }

}