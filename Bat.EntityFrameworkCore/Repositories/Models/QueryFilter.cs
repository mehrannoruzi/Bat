﻿namespace Bat.EntityFrameworkCore;

public class QueryFilter<TEntity> where TEntity : class
{
    public bool AsNoTracking { get; set; } = true;
    public CancellationToken CancellationToken { get; set; } = default;
    public PagingParameter PagingParameter { get; set; } = null;
    public Expression<Func<TEntity, bool>> Conditions { get; set; }
    public List<Expression<Func<TEntity, object>>> IncludeProperties { get; set; }
    public Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> ThenIncludeProperties { get; set; }
    public Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> OrderBy { get; set; }
}