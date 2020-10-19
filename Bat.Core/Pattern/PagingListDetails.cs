namespace Bat.Core
{
    public class PagingListDetails<T> : PagingDetails
    {
        public PagingList<T> Items { get; set; }
    }
}
