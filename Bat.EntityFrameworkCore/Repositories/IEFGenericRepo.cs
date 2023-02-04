namespace Bat.EntityFrameworkCore;

public interface IEFGenericRepo<TEntity> : ITransientInjection where TEntity : class, IBaseEntity
{
    void Add(TEntity model);
    Task AddAsync(TEntity model, CancellationToken cancellationToken = default);
    void AddRange(IEnumerable<TEntity> models);
    Task AddRangeAsync(IEnumerable<TEntity> models, CancellationToken cancellationToken = default);
    void Update(TEntity model);
    void UpdateSpecificProperties(TEntity entity, List<string> updatedProperties);
    void UpdateSpecificProperties(TEntity entity, params Expression<Func<TEntity, object>>[] updatedProperties);
    void UpdateRange(IEnumerable<TEntity> models);
    void UpdateRange(Expression<Func<SetPropertyCalls<TEntity>, SetPropertyCalls<TEntity>>> setProperties, CancellationToken cancellationToken = default);
    void Delete(TEntity model);
    void DeleteUnAttached(TEntity model);
    void DeleteRange(IEnumerable<TEntity> models);
    Task DeleteRange(Expression<Func<TEntity, bool>> Conditions, CancellationToken cancellationToken = default);
    Task<TEntity> FindAsync(object id, CancellationToken cancellationToken = default);
    Task<bool> AnyAsync(QueryFilter<TEntity> model = null);
    Task<int> CountAsync(QueryFilter<TEntity> model = null);
    Task<long> LongCountAsync(QueryFilter<TEntity> model = null);
    Task<TEntity> FirstOrDefaultAsync(QueryFilter<TEntity> model = null);
    Task<TResult> FirstOrDefaultAsync<TResult>(QueryFilterWithSelector<TEntity, TResult> model);
    Task<List<TEntity>> GetAsync(QueryFilter<TEntity> model = null);
    Task<List<TResult>> GetAsync<TResult>(QueryFilterWithSelector<TEntity, TResult> model);
    Task<PagingListDetails<TEntity>> GetPagingAsync(QueryFilter<TEntity> model = null);
    Task<PagingListDetails<TResult>> GetPagingAsync<TResult>(QueryFilterWithSelector<TEntity, TResult> model);

    Task<List<TEntity>> ExecuteQueryAsync(string sql, CancellationToken cancellationToken = default, params object[] parameters);
}