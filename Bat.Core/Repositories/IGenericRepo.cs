using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Bat.Core
{
    public interface IGenericRepo<TEntity> where TEntity : class
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
        Task<bool> AnyAsync(QueryFilter<TEntity> model);
        Task<int> CountAsync(QueryFilter<TEntity> model);
        Task<long> LongCountAsync(QueryFilter<TEntity> model);
        Task<TEntity> FirstOrDefaultAsync(QueryFilter<TEntity> model);
        Task<TResult> FirstOrDefaultAsync<TResult>(QueryFilterWithSelector<TEntity, TResult> model);
        Task<List<TEntity>> GetAsync(QueryFilter<TEntity> model);
        Task<List<TResult>> GetAsync<TResult>(QueryFilterWithSelector<TEntity, TResult> model);
        Task<PagingListDetails<TEntity>> GetPagingAsync(PagingQueryFilter<TEntity> model);
        Task<PagingListDetails<TResult>> GetPagingAsync<TResult>(PagingQueryFilterWithSelector<TEntity, TResult> model);

        Task<IEnumerable<TEntity>> ExecuteQueryListAsync(string sql, params object[] parameters);
    }
}