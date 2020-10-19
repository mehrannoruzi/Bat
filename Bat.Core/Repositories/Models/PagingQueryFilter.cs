namespace Bat.Core
{
    public class PagingQueryFilter<TEntity> : QueryFilter<TEntity> where TEntity : class
    {
        public PagingParameter PagingParameter { get; set; }
    }
}