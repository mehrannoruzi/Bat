using EFCore.BulkExtensions;

namespace Bat.EntityFrameworkCore.Tools;

public interface IEfBulkGenericRepo<TEntity> : ITransientInjection where TEntity : class, IBaseEntity
{
    Task BulkInsertAsync(IList<TEntity> entities, BulkConfig bulkConfig = null, CancellationToken token = default);
    Task BulkUpdateAsync(IList<TEntity> entities, BulkConfig bulkConfig = null, CancellationToken token = default);
    Task BulkInsertOrUpdateAsync(IList<TEntity> entities, BulkConfig bulkConfig = null, CancellationToken token = default);
    Task BulkDeleteAsync(IList<TEntity> entities, BulkConfig bulkConfig = null, CancellationToken token = default);
    Task BulkReadAsync(IList<TEntity> entities, BulkConfig bulkConfig = null, CancellationToken token = default);
}