namespace Bat.Core
{
    public class PagingParameter
    {
        public PagingParameter()
        {
            PageNumber = 1;
            PageSize = 10;
        }

        public PagingParameter(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }

        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}