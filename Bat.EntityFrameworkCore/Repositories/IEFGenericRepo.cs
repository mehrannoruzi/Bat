namespace Bat.EntityFrameworkCore;

public interface IEFGenericRepo<TEntity> : ITransientInjection where TEntity : class, IBaseEntity
{
    void Add(TEntity model);
    Task AddAsync(TEntity model, CancellationToken token = default);
    void AddRange(IEnumerable<TEntity> models);
    Task AddRangeAsync(IEnumerable<TEntity> models, CancellationToken token = default);
    void Update(TEntity model);
    void UpdateUnAttached(TEntity model);
    void UpdateRange(IEnumerable<TEntity> models);
    void UpdateRange(Expression<Func<SetPropertyCalls<TEntity>, SetPropertyCalls<TEntity>>> setProperties);
    void Delete(TEntity model);
    void DeleteUnAttached(TEntity model);
    void DeleteRange(IEnumerable<TEntity> models);
    Task DeleteRange(Expression<Func<TEntity, bool>> Conditions);
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