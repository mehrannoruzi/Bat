namespace Bat.EntityFrameworkCore;

public interface IEFGenericRepo<TEntity> : ITransientInjection where TEntity : class, IBaseEntity
{
    Task AddAsync(TEntity model, CancellationToken token = default);
    Task AddRangeAsync(IEnumerable<TEntity> models, CancellationToken token = default);
    void Update(TEntity model);
	void UpdateRange(IEnumerable<TEntity> models);
    void UpdateUnAttached(TEntity model);
	void UpdateSpecificProperties(TEntity entity, List<string> updatedProperties);
	void UpdateSpecificProperties(TEntity entity, params Expression<Func<TEntity, object>>[] updatedProperties);
    void Delete(TEntity model);
    void DeleteRange(IEnumerable<TEntity> models);
    void DeleteUnAttached(TEntity model);
    Task<TEntity> FindAsync(object id, CancellationToken token = default);
    Task<bool> AnyAsync(QueryFilter<TEntity> model = null);
    Task<int> CountAsync(QueryFilter<TEntity> model = null);
    Task<long> LongCountAsync(QueryFilter<TEntity> model = null);
    Task<TEntity> FirstOrDefaultAsync(QueryFilter<TEntity> model = null);
    Task<TResult> FirstOrDefaultAsync<TResult>(QueryFilterWithSelector<TEntity, TResult> model);
    Task<List<TEntity>> GetAsync(QueryFilter<TEntity> model = null);
    Task<List<TResult>> GetAsync<TResult>(QueryFilterWithSelector<TEntity, TResult> model);
    Task<PagingListDetails<TEntity>> GetPagingAsync(QueryFilter<TEntity> model = null);
    Task<PagingListDetails<TResult>> GetPagingAsync<TResult>(QueryFilterWithSelector<TEntity, TResult> model);

    Task<List<TEntity>> ExecuteQueryAsync(string sql, params object[] parameters);
}