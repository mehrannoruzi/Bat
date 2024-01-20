namespace Bat.EntityFrameworkCore;

public static class RepositoryExtension
{
    public static IQueryable<T> AsQueryable<T>(this EFGenericRepo<T> repo) where T : class, IBaseEntity
        => repo._dbSet.AsQueryable();

    public static IQueryable<T> AsNoTracking<T>(this EFGenericRepo<T> repo) where T : class, IBaseEntity
        => repo._dbSet.AsNoTracking();

    public static IQueryable<T> AsNoTrackingWithIdentityResolution<T>(this EFGenericRepo<T> repo) where T : class, IBaseEntity
        => repo._dbSet.AsNoTrackingWithIdentityResolution();

    public static IIncludableQueryable<T, TProperty> Include<T, TProperty>(this EFGenericRepo<T> repo, Expression<Func<T, TProperty>> includeProperty) where T : class, IBaseEntity
        => repo._dbSet.AsQueryable().Include(includeProperty);


    public static async Task<bool> AnyAsync<T>(this EFGenericRepo<T> repo, CancellationToken cancellationToken = default) where T : class, IBaseEntity
        => await repo._dbSet.AnyAsync(cancellationToken);

    public static async Task<bool> AnyAsync<T>(this EFGenericRepo<T> repo, Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default) where T : class, IBaseEntity
        => await repo._dbSet.AnyAsync(predicate, cancellationToken);


    public static async Task<int> CountAsync<T>(this EFGenericRepo<T> repo, CancellationToken cancellationToken = default) where T : class, IBaseEntity
        => await repo._dbSet.CountAsync(cancellationToken);

    public static async Task<int> CountAsync<T>(this EFGenericRepo<T> repo, Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default) where T : class, IBaseEntity
        => await repo._dbSet.CountAsync(predicate, cancellationToken);

    public static async Task<long> LongCountAsync<T>(this EFGenericRepo<T> repo, CancellationToken cancellationToken = default) where T : class, IBaseEntity
        => await repo._dbSet.LongCountAsync(cancellationToken);

    public static async Task<long> LongCountAsync<T>(this EFGenericRepo<T> repo, Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default) where T : class, IBaseEntity
        => await repo._dbSet.LongCountAsync(predicate, cancellationToken);


    public static async Task<T> FirstOrDefaultAsync<T>(this EFGenericRepo<T> repo, CancellationToken cancellationToken = default) where T : class, IBaseEntity
        => await repo._dbSet.FirstOrDefaultAsync(cancellationToken);

    public static async Task<T> FirstOrDefaultAsync<T>(this EFGenericRepo<T> repo, Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default) where T : class, IBaseEntity
        => await repo._dbSet.FirstOrDefaultAsync(predicate, cancellationToken);


    public static IQueryable<T> Where<T>(this EFGenericRepo<T> repo, Expression<Func<T, bool>> predicate) where T : class, IBaseEntity
        => repo._dbSet.Where(predicate);

    public static IQueryable<TResult> Select<T, TResult>(this EFGenericRepo<T> repo, Expression<Func<T, TResult>> selector) where T : class, IBaseEntity
        => repo._dbSet.Select(selector);


    public static IOrderedQueryable<T> OrderBy<T, TKey>(this EFGenericRepo<T> repo, Expression<Func<T, TKey>> orderBy) where T : class, IBaseEntity
        => repo._dbSet.AsQueryable().OrderBy(orderBy);

    public static IOrderedQueryable<T> OrderByDescending<T, TKey>(this EFGenericRepo<T> repo, Expression<Func<T, TKey>> orderBy) where T : class, IBaseEntity
        => repo._dbSet.AsQueryable().OrderByDescending(orderBy);


    public static async Task<IEnumerable<T>> ToListAsync<T>(this EFGenericRepo<T> repo, CancellationToken cancellationToken = default) where T : class, IBaseEntity
        => await repo._dbSet.ToListAsync(cancellationToken);

    public static async Task<PagingListDetails<T>> ToPagingListAsync<T>(this EFGenericRepo<T> repo, PagingParameter pagingParameter, CancellationToken cancellationToken = default) where T : class, IBaseEntity
        => await repo._dbSet.ToPagingListDetailsAsync(pagingParameter, cancellationToken);

    public static async Task<PagingListDetails<T>> ToPagingListAsync<T>(this EFGenericRepo<T> repo, PagingParameter pagingParameter, string orderBy, CancellationToken cancellationToken = default) where T : class, IBaseEntity
        => await repo._dbSet.ToPagingListDetailsAsync(pagingParameter, orderBy, cancellationToken);

}