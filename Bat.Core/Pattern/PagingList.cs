namespace Bat.Core;

public class PagingList<T> : List<T>
{
    public PagingDetails PagingDetails { get; set; }

    public PagingList(List<T> sourceList, int count, PagingParameter pagingParameter)
    {
        if (PagingDetails == null) PagingDetails = new PagingDetails();
        PagingDetails.TotalCount = count;
        PagingDetails.PageSize = pagingParameter.PageSize;
        PagingDetails.PageNumber = pagingParameter.PageNumber;
        PagingDetails.TotalPages = (int)Math.Ceiling(count / (double)pagingParameter.PageSize);
        PagingDetails.HasNext = PagingDetails.PageNumber < PagingDetails.TotalPages;
        PagingDetails.HasPrevious = PagingDetails.PageNumber > 1;

        AddRange(sourceList);
    }

    public static PagingList<T> GetPagingList(IQueryable<T> source, PagingParameter pagingParameter)
    {
        var count = source.Count();
        var ps = pagingParameter.PageSize == 0 ? 10 : pagingParameter.PageSize;
        var pn = pagingParameter.PageNumber == 0 ? 1 : pagingParameter.PageNumber;
        var sourceList = source.Skip((pn - 1) * ps).Take(ps).ToList();

        return new PagingList<T>(sourceList, count, new PagingParameter { PageNumber = pn, PageSize = ps });
    }

    public static PagingListDetails<T> GetPagingListDetails(IQueryable<T> source, PagingParameter pagingParameter)
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