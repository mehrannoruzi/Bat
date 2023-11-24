using EFCore.BulkExtensions;

namespace Bat.EntityFrameworkCore.Tools;

public class EFBulkGenericRepo<TEntity>(DbContext context) : IEFBulkGenericRepo<TEntity> where TEntity : class, IBaseEntity
{
    public DbContext _context = context;

	public async Task BulkInsertAsync(IList<TEntity> entities, BulkConfig bulkConfig = null, CancellationToken cancellationToken = default)
        => await _context.BulkInsertAsync(entities: entities, bulkConfig: bulkConfig, cancellationToken: cancellationToken);

    public async Task BulkUpdateAsync(IList<TEntity> entities, BulkConfig bulkConfig = null, CancellationToken cancellationToken = default)
        => await _context.BulkUpdateAsync(entities: entities, bulkConfig: bulkConfig, cancellationToken: cancellationToken);

    public async Task BulkInsertOrUpdateAsync(IList<TEntity> entities, BulkConfig bulkConfig = null, CancellationToken cancellationToken = default)
        => await _context.BulkInsertOrUpdateAsync(entities: entities, bulkConfig: bulkConfig, cancellationToken: cancellationToken);

    public async Task BulkDeleteAsync(IList<TEntity> entities, BulkConfig bulkConfig = null, CancellationToken cancellationToken = default)
        => await _context.BulkDeleteAsync(entities: entities, bulkConfig: bulkConfig, cancellationToken: cancellationToken);

    public async Task BulkReadAsync(IList<TEntity> entities, BulkConfig bulkConfig = null, CancellationToken cancellationToken = default)
        => await _context.BulkReadAsync(entities: entities, bulkConfig: bulkConfig, cancellationToken: cancellationToken);

}