using EFCore.BulkExtensions;

namespace Bat.EntityFrameworkCore.Tools;

public interface IEFBulkGenericRepo<TEntity> : ITransientInjection where TEntity : class, IBaseEntity
{
    Task BulkInsertAsync(IList<TEntity> entities, BulkConfig bulkConfig = null, CancellationToken cancellationToken = default);
    Task BulkUpdateAsync(IList<TEntity> entities, BulkConfig bulkConfig = null, CancellationToken cancellationToken = default);
    Task BulkInsertOrUpdateAsync(IList<TEntity> entities, BulkConfig bulkConfig = null, CancellationToken cancellationToken = default);
    Task BulkDeleteAsync(IList<TEntity> entities, BulkConfig bulkConfig = null, CancellationToken cancellationToken = default);
    Task BulkReadAsync(IList<TEntity> entities, BulkConfig bulkConfig = null, CancellationToken cancellationToken = default);
}