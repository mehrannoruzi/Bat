namespace Bat.EntityFrameworkCore;

public class EFGenericRepo<TEntity>(DbContext context) where TEntity : class, IBaseEntity
{
    private readonly DbContext _context = context;
    public DbSet<TEntity> _dbSet = context.Set<TEntity>();


    public void Add(TEntity model)
        => _dbSet.Add(model);

    public async Task AddAsync(TEntity model, CancellationToken cancellationToken = default)
        => await _dbSet.AddAsync(model, cancellationToken);

    public void AddRange(IEnumerable<TEntity> models)
        => _dbSet.AddRange(models);

    public async Task AddRangeAsync(IEnumerable<TEntity> models, CancellationToken cancellationToken = default)
        => await _dbSet.AddRangeAsync(models, cancellationToken);

    public void Update(TEntity model)
        => _dbSet.Update(model);

    public void UpdateSpecificProperties(TEntity entity, List<string> updatedProperties)
    {
        foreach (var property in updatedProperties)
            _context.Entry(entity).Property(property).IsModified = true;
    }

    public void UpdateSpecificProperties(TEntity entity, params Expression<Func<TEntity, object>>[] updatedProperties)
    {
        if (_context.Entry(entity) is not null)
            _context.Entry(entity).State = EntityState.Detached;

        foreach (var property in updatedProperties)
            _context.Entry(entity).Property(property).IsModified = true;
    }

    public void UpdateRange(IEnumerable<TEntity> models)
        => _dbSet.UpdateRange(models);

    public void UpdateRange(Expression<Func<SetPropertyCalls<TEntity>, SetPropertyCalls<TEntity>>> setProperties, CancellationToken cancellationToken = default)
        => _dbSet.ExecuteUpdateAsync(setProperties, cancellationToken);

    public void Delete(TEntity model)
        => _dbSet.Remove(model);

    public void DeleteUnAttached(TEntity model)
    {
        _dbSet.Attach(model);
        _dbSet.Remove(model);
    }

    public void DeleteRange(IEnumerable<TEntity> models)
        => _dbSet.RemoveRange(models);

    public async Task DeleteRange(Expression<Func<TEntity, bool>> Conditions, CancellationToken cancellationToken = default)
        => await _dbSet.Where(Conditions).ExecuteDeleteAsync(cancellationToken);


    public async Task<TEntity> FindAsync(object id, CancellationToken cancellationToken = default)
        => await _dbSet.FindAsync(new object[] { id }, cancellationToken);

    public async Task<bool> AnyAsync(QueryFilter<TEntity> model = null)
    {
        if (model.IsNull()) return await _dbSet.AnyAsync();

        IQueryable<TEntity> query = model.AsNoTracking ? _dbSet.AsNoTracking() : _dbSet.AsQueryable();
        model.IncludeProperties?.ForEach(i => { query = query.Include(i); });
        if (model.ThenIncludeProperties is not null) query = model.ThenIncludeProperties(query);
        if (model.Conditions is not null) query = query.Where(model.Conditions);
        return await query.AnyAsync(model.CancellationToken);
    }

    public async Task<int> CountAsync(QueryFilter<TEntity> model = null)
    {
        if (model.IsNull()) return await _dbSet.CountAsync();

        IQueryable<TEntity> query = model.AsNoTracking ? _dbSet.AsNoTracking() : _dbSet.AsQueryable();
        model.IncludeProperties?.ForEach(i => { query = query.Include(i); });
        if (model.ThenIncludeProperties is not null) query = model.ThenIncludeProperties(query);
        if (model.Conditions is not null) query = query.Where(model.Conditions);
        return await query.CountAsync(model.CancellationToken);
    }

    public async Task<long> LongCountAsync(QueryFilter<TEntity> model = null)
    {
        if (model.IsNull()) return await _dbSet.LongCountAsync();

        IQueryable<TEntity> query = model.AsNoTracking ? _dbSet.AsNoTracking() : _dbSet.AsQueryable();
        model.IncludeProperties?.ForEach(i => { query = query.Include(i); });
        if (model.ThenIncludeProperties is not null) query = model.ThenIncludeProperties(query);
        if (model.Conditions is not null) query = query.Where(model.Conditions);
        return await query.LongCountAsync(model.CancellationToken);
    }

    public async Task<TEntity> FirstOrDefaultAsync(QueryFilter<TEntity> model = null)
    {
        if (model.IsNull()) return await _dbSet.FirstOrDefaultAsync();

        IQueryable<TEntity> query = model.AsNoTracking ? _dbSet.AsNoTracking() : _dbSet.AsQueryable();
        model.IncludeProperties?.ForEach(i => { query = query.Include(i); });
        if (model.ThenIncludeProperties is not null) query = model.ThenIncludeProperties(query);
        if (model.Conditions is not null) query = query.Where(model.Conditions);
        if (model.OrderBy is not null) query = model.OrderBy(query);
        return await query.FirstOrDefaultAsync(model.CancellationToken);
    }

    public async Task<TResult> FirstOrDefaultAsync<TResult>(QueryFilterWithSelector<TEntity, TResult> model)
    {
        if (model.Selector.IsNull()) throw new Exception("Selector Not Assigned");

        IQueryable<TEntity> query = model.AsNoTracking ? _dbSet.AsNoTracking() : _dbSet.AsQueryable();
        model.IncludeProperties?.ForEach(i => { query = query.Include(i); });
        if (model.ThenIncludeProperties is not null) query = model.ThenIncludeProperties(query);
        if (model.Conditions is not null) query = query.Where(model.Conditions);
        if (model.OrderBy is not null) query = model.OrderBy(query);
        return await query.Select(model.Selector).FirstOrDefaultAsync(model.CancellationToken);
    }

    public async Task<List<TEntity>> GetAsync(QueryFilter<TEntity> model = null)
    {
        if (model.IsNull()) return await _dbSet.ToListAsync();

        IQueryable<TEntity> query = model.AsNoTracking ? _dbSet.AsNoTracking() : _dbSet.AsQueryable();
        if (model.Conditions is not null) query = query.Where(model.Conditions);
        model.IncludeProperties?.ForEach(i => { query = query.Include(i); });
        if (model.ThenIncludeProperties is not null) query = model.ThenIncludeProperties(query);
        if (model.OrderBy is not null) query = model.OrderBy(query);
        if (model.PagingParameter is not null) query = query.Skip((model.PagingParameter.PageNumber - 1) * model.PagingParameter.PageSize).Take(model.PagingParameter.PageSize);
        return await query.ToListAsync(model.CancellationToken);
    }

    public async Task<List<TResult>> GetAsync<TResult>(QueryFilterWithSelector<TEntity, TResult> model)
    {
        if (model.Selector.IsNull()) throw new Exception("Selector Not Assigned");

        IQueryable<TEntity> query = model.AsNoTracking ? _dbSet.AsNoTracking() : _dbSet.AsQueryable();
        if (model.Conditions is not null) query = query.Where(model.Conditions);
        model.IncludeProperties?.ForEach(i => { query = query.Include(i); });
        if (model.ThenIncludeProperties is not null) query = model.ThenIncludeProperties(query);
        if (model.OrderBy is not null) query = model.OrderBy(query);
        if (model.PagingParameter is not null) query = query.Skip((model.PagingParameter.PageNumber - 1) * model.PagingParameter.PageSize).Take(model.PagingParameter.PageSize);
        return await query.Select(model.Selector).ToListAsync(model.CancellationToken);
    }

    public async Task<PagingListDetails<TEntity>> GetPagingAsync(QueryFilter<TEntity> model = null)
    {
        if (model.IsNull()) return await _dbSet.ToPagingListDetailsAsync(new PagingParameter { PageNumber = 1, PageSize = 10 });

        IQueryable<TEntity> query = model.AsNoTracking ? _dbSet.AsNoTracking() : _dbSet.AsQueryable();
        if (model.Conditions is not null) query = query.Where(model.Conditions);
        model.IncludeProperties?.ForEach(i => { query = query.Include(i); });
        if (model.ThenIncludeProperties is not null) query = model.ThenIncludeProperties(query);
        if (model.OrderBy is not null) query = model.OrderBy(query);
        return await query.ToPagingListDetailsAsync(model.PagingParameter ?? new PagingParameter { PageNumber = 1, PageSize = 10 }, model.CancellationToken);
    }

    public async Task<PagingListDetails<TResult>> GetPagingAsync<TResult>(QueryFilterWithSelector<TEntity, TResult> model)
    {
        if (model.Selector.IsNull()) throw new Exception("Selector Not Assigned");

        IQueryable<TEntity> query = model.AsNoTracking ? _dbSet.AsNoTracking() : _dbSet.AsQueryable();
        if (model.Conditions is not null) query = query.Where(model.Conditions);
        model.IncludeProperties?.ForEach(i => { query = query.Include(i); });
        if (model.ThenIncludeProperties is not null) query = model.ThenIncludeProperties(query);
        if (model.OrderBy is not null) query = model.OrderBy(query);
        return await query.Select(model.Selector).ToPagingListDetailsAsync(model.PagingParameter ?? new PagingParameter { PageNumber = 1, PageSize = 10 }, model.CancellationToken);
    }


    public async Task<List<TEntity>> ExecuteQueryAsync(string sql, CancellationToken cancellationToken = default, params object[] parameters)
    => await _dbSet.FromSqlRaw(sql, parameters).ToListAsync(cancellationToken);
}