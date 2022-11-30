using EFCore.BulkExtensions;

namespace Bat.EntityFrameworkCore.Tools;

public class EfBulkGenericRepo<TEntity> : IEfBulkGenericRepo<TEntity> where TEntity : class, IBaseEntity
{
    public DbContext _context;

    public EfBulkGenericRepo(DbContext context)
        => _context = context;


    public async Task BulkInsertAsync(IList<TEntity> entities, BulkConfig bulkConfig = null, CancellationToken token = default)
        => await _context.BulkInsertAsync(entities: entities, bulkConfig: bulkConfig, cancellationToken: token);

    public async Task BulkUpdateAsync(IList<TEntity> entities, BulkConfig bulkConfig = null, CancellationToken token = default)
        => await _context.BulkUpdateAsync(entities: entities, bulkConfig: bulkConfig, cancellationToken: token);

    public async Task BulkInsertOrUpdateAsync(IList<TEntity> entities, BulkConfig bulkConfig = null, CancellationToken token = default)
        => await _context.BulkInsertOrUpdateAsync(entities: entities, bulkConfig: bulkConfig, cancellationToken: token);

    public async Task BulkDeleteAsync(IList<TEntity> entities, BulkConfig bulkConfig = null, CancellationToken token = default)
        => await _context.BulkDeleteAsync(entities: entities, bulkConfig: bulkConfig, cancellationToken: token);

    public async Task BulkReadAsync(IList<TEntity> entities, BulkConfig bulkConfig = null, CancellationToken token = default)
        => await _context.BulkReadAsync(entities: entities, bulkConfig: bulkConfig, cancellationToken: token);

}