using System.Linq.Expressions;

namespace Bat.Core;

public class QueryFilter<TEntity> where TEntity : class
{
    public bool AsNoTracking { get; set; } = true;
    public CancellationToken Token { get; set; } = default;
    public PagingParameter PagingParameter { get; set; } = null;
    public Expression<Func<TEntity, bool>> Conditions { get; set; }
    public List<Expression<Func<TEntity, object>>> IncludeProperties { get; set; }
    //public IIncludableQueryable<TEntity, object>> IncludeProperties { get; set; }
    public Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> OrderBy { get; set; }
}