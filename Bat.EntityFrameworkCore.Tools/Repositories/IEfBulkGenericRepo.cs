using Bat.Core;
using System.Threading;
using EFCore.BulkExtensions;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Bat.EntityFrameworkCore.Tools
{
    public interface IBulkGenericRepo<TEntity> : ITransientInjection where TEntity : class, IBaseEntity
    {
        Task BulkInsertAsync(IList<TEntity> entities, BulkConfig bulkConfig = null, CancellationToken token = default);
        Task BulkUpdateAsync(IList<TEntity> entities, BulkConfig bulkConfig = null, CancellationToken token = default);
        Task BulkInsertOrUpdateAsync(IList<TEntity> entities, BulkConfig bulkConfig = null, CancellationToken token = default);
        Task BulkDeleteAsync(IList<TEntity> entities, BulkConfig bulkConfig = null, CancellationToken token = default);
        Task BulkReadAsync(IList<TEntity> entities, BulkConfig bulkConfig = null, CancellationToken token = default);
    }
}