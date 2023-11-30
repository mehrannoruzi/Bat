namespace Bat.EntityFrameworkCore;

public class QueryFilterWithSelector<TEntity, TResult> : QueryFilter<TEntity> where TEntity : class
{
    public QueryFilterWithSelector() { AsNoTracking = true; }
    public Expression<Func<TEntity, TResult>> Selector { set; get; }
}