using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Bat.Core
{
    public interface IGenericRepo<TEntity> where TEntity : class, IBaseEntity
    {
        Task AddAsync(TEntity model, CancellationToken token = default);
        Task AddRangeAsync(IEnumerable<TEntity> models, CancellationToken token = default);
        void Update(TEntity model);
        void UpdateRange(IEnumerable<TEntity> models);
        void UpdateUnAttached(TEntity model);
        void Delete(TEntity model);
        void DeleteRange(IEnumerable<TEntity> models);
        void DeleteUnAttached(TEntity model);
        Task<TEntity> FindAsync(object id, CancellationToken token = default);
        Task<bool> AnyAsync(QueryFilter<TEntity> model = null);
        Task<int> CountAsync(QueryFilter<TEntity> model = null);
        Task<long> LongCountAsync(QueryFilter<TEntity> model = null);
        Task<TEntity> FirstOrDefaultAsync(QueryFilter<TEntity> model = null);
        Task<TResult> FirstOrDefaultAsync<TResult>(QueryFilterWithSelector<TEntity, TResult> model = null) where TResult : class, new();
        Task<List<TEntity>> GetAsync(QueryFilter<TEntity> model = null);
        Task<List<TResult>> GetAsync<TResult>(QueryFilterWithSelector<TEntity, TResult> model = null) where TResult : class, new();
        Task<PagingListDetails<TEntity>> GetPagingAsync(PagingQueryFilter<TEntity> model = null);
        Task<PagingListDetails<TResult>> GetPagingAsync<TResult>(PagingQueryFilterWithSelector<TEntity, TResult> model = null) where TResult : class, new();

        Task<List<TEntity>> ExecuteQueryListAsync(string sql, params object[] parameters);
    }
}