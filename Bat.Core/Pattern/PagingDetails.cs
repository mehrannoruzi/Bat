namespace Bat.Core
{
    public class PagingDetails : PagingParameter
    {
        public int TotalPages { get; set; }
        public int TotalCount { get; set; }

        public bool HasPrevious { get; set; }
        public bool HasNext { get; set; }
    }
}
