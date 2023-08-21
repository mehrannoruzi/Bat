namespace Bat.EntityFrameworkCore;

public class EfGenericRepo<TEntity> : IEFGenericRepo<TEntity> where TEntity : class, IBaseEntity
{
	public DbSet<TEntity> _dbSet;
	private readonly DbContext _context;

	public EfGenericRepo(DbContext context)
    {
        _context = context;
        _dbSet = context.Set<TEntity>();
    }


    public async Task AddAsync(TEntity model, CancellationToken token = default)
        => await _dbSet.AddAsync(model, token);

    public async Task AddRangeAsync(IEnumerable<TEntity> models, CancellationToken token = default)
        => await _dbSet.AddRangeAsync(models, token);

    public void Update(TEntity model)
        => _dbSet.Update(model);

	public void UpdateRange(IEnumerable<TEntity> models)
        => _dbSet.UpdateRange(models);

	public void UpdateSpecificProperties(TEntity entity, List<string> updatedProperties)
	{
		foreach (var property in updatedProperties)
			_context.Entry(entity).Property(property).IsModified = true;
	}

	public void UpdateSpecificProperties(TEntity entity, params Expression<Func<TEntity, object>>[] updatedProperties)
	{
		foreach (var property in updatedProperties)
			_context.Entry(entity).Property(property).IsModified = true;
	}

	public void UpdateUnAttached(TEntity model)
    {
        _dbSet.Attach(model);
        _dbSet.Update(model);
    }

    public void Delete(TEntity model)
        => _dbSet.Remove(model);

    public void DeleteRange(IEnumerable<TEntity> models)
        => _dbSet.RemoveRange(models);

    public void DeleteUnAttached(TEntity model)
    {
        _dbSet.Attach(model);
        _dbSet.Remove(model);
    }

    public async Task<TEntity> FindAsync(object id, CancellationToken token = default)
        => await _dbSet.FindAsync(new object[] { id }, token);

    public async Task<bool> AnyAsync(QueryFilter<TEntity> model = null)
    {
        if (model.IsNull()) return await _dbSet.AnyAsync();

        IQueryable<TEntity> query = model.AsNoTracking ? _dbSet.AsNoTracking() : _dbSet.AsQueryable();
        if (model.IncludeProperties != null) model.IncludeProperties.ForEach(i => { query = query.Include(i); });
        if (model.ThenIncludeProperties != null) query = model.ThenIncludeProperties(query);
        if (model.Conditions != null) query = query.Where(model.Conditions);
        return await query.AnyAsync(model.Token);
    }

    public async Task<int> CountAsync(QueryFilter<TEntity> model = null)
    {
        if (model.IsNull()) return await _dbSet.CountAsync();

        IQueryable<TEntity> query = model.AsNoTracking ? _dbSet.AsNoTracking() : _dbSet.AsQueryable();
        if (model.IncludeProperties != null) model.IncludeProperties.ForEach(i => { query = query.Include(i); });
        if (model.ThenIncludeProperties != null) query = model.ThenIncludeProperties(query);
        if (model.Conditions != null) query = query.Where(model.Conditions);
        return await query.CountAsync(model.Token);
    }

    public async Task<long> LongCountAsync(QueryFilter<TEntity> model = null)
    {
        if (model.IsNull()) return await _dbSet.LongCountAsync();

        IQueryable<TEntity> query = model.AsNoTracking ? _dbSet.AsNoTracking() : _dbSet.AsQueryable();
        if (model.IncludeProperties != null) model.IncludeProperties.ForEach(i => { query = query.Include(i); });
        if (model.ThenIncludeProperties != null) query = model.ThenIncludeProperties(query);
        if (model.Conditions != null) query = query.Where(model.Conditions);
        return await query.LongCountAsync(model.Token);
    }

    public async Task<TEntity> FirstOrDefaultAsync(QueryFilter<TEntity> model = null)
    {
        if (model.IsNull()) return await _dbSet.FirstOrDefaultAsync();

        IQueryable<TEntity> query = model.AsNoTracking ? _dbSet.AsNoTracking() : _dbSet.AsQueryable();
        if (model.IncludeProperties != null) model.IncludeProperties.ForEach(i => { query = query.Include(i); });
        if (model.ThenIncludeProperties != null) query = model.ThenIncludeProperties(query);
        if (model.Conditions != null) query = query.Where(model.Conditions);
        if (model.OrderBy != null) query = model.OrderBy(query);
        return await query.FirstOrDefaultAsync(model.Token);
    }

    public async Task<TResult> FirstOrDefaultAsync<TResult>(QueryFilterWithSelector<TEntity, TResult> model)
    {
        if (model.Selector.IsNull()) throw new Exception(Strings.SelectorNotAssigned);

        IQueryable<TEntity> query = model.AsNoTracking ? _dbSet.AsNoTracking() : _dbSet.AsQueryable();
        if (model.IncludeProperties != null) model.IncludeProperties.ForEach(i => { query = query.Include(i); });
        if (model.ThenIncludeProperties != null) query = model.ThenIncludeProperties(query);
        if (model.Conditions != null) query = query.Where(model.Conditions);
        if (model.OrderBy != null) query = model.OrderBy(query);
        return await query.Select(model.Selector).FirstOrDefaultAsync(model.Token);
    }

    public async Task<List<TEntity>> GetAsync(QueryFilter<TEntity> model = null)
    {
        if (model.IsNull()) return await _dbSet.ToListAsync();

        IQueryable<TEntity> query = model.AsNoTracking ? _dbSet.AsNoTracking() : _dbSet.AsQueryable();
        if (model.Conditions != null) query = query.Where(model.Conditions);
        if (model.IncludeProperties != null) model.IncludeProperties.ForEach(i => { query = query.Include(i); });
        if (model.ThenIncludeProperties != null) query = model.ThenIncludeProperties(query);
        if (model.OrderBy != null) query = model.OrderBy(query);
        if (model.PagingParameter != null) query = query.Skip((model.PagingParameter.PageNumber - 1) * model.PagingParameter.PageSize).Take(model.PagingParameter.PageSize);
        return await query.ToListAsync();
    }

    public async Task<List<TResult>> GetAsync<TResult>(QueryFilterWithSelector<TEntity, TResult> model)
    {
        if (model.Selector.IsNull()) throw new Exception(Strings.SelectorNotAssigned);

        IQueryable<TEntity> query = model.AsNoTracking ? _dbSet.AsNoTracking() : _dbSet.AsQueryable();
        if (model.Conditions != null) query = query.Where(model.Conditions);
        if (model.IncludeProperties != null) model.IncludeProperties.ForEach(i => { query = query.Include(i); });
        if (model.ThenIncludeProperties != null) query = model.ThenIncludeProperties(query);
        if (model.OrderBy != null) query = model.OrderBy(query);
        if (model.PagingParameter != null) query = query.Skip((model.PagingParameter.PageNumber - 1) * model.PagingParameter.PageSize).Take(model.PagingParameter.PageSize);
        return await query.Select(model.Selector).ToListAsync();
    }

    public async Task<PagingListDetails<TEntity>> GetPagingAsync(QueryFilter<TEntity> model = null)
    {
        if (model.IsNull()) return await _dbSet.ToPagingListDetailsAsync(new PagingParameter { PageNumber = 1, PageSize = 100 });

        IQueryable<TEntity> query = model.AsNoTracking ? _dbSet.AsNoTracking() : _dbSet.AsQueryable();
        if (model.Conditions != null) query = query.Where(model.Conditions);
        if (model.IncludeProperties != null) model.IncludeProperties.ForEach(i => { query = query.Include(i); });
        if (model.ThenIncludeProperties != null) query = model.ThenIncludeProperties(query);
        if (model.OrderBy != null) query = model.OrderBy(query);
        return await query.ToPagingListDetailsAsync(model.PagingParameter ?? new PagingParameter { PageNumber = 1, PageSize = 100 });
    }

    public async Task<PagingListDetails<TResult>> GetPagingAsync<TResult>(QueryFilterWithSelector<TEntity, TResult> model)
    {
        if (model.Selector.IsNull()) throw new Exception(Strings.SelectorNotAssigned);

        IQueryable<TEntity> query = model.AsNoTracking ? _dbSet.AsNoTracking() : _dbSet.AsQueryable();
        if (model.Conditions != null) query = query.Where(model.Conditions);
        if (model.IncludeProperties != null) model.IncludeProperties.ForEach(i => { query = query.Include(i); });
        if (model.ThenIncludeProperties != null) query = model.ThenIncludeProperties(query);
        if (model.OrderBy != null) query = model.OrderBy(query);
        return await query.Select(model.Selector).ToPagingListDetailsAsync(model.PagingParameter ?? new PagingParameter { PageNumber = 1, PageSize = 100 });
    }



    public async Task<List<TEntity>> ExecuteQueryAsync(string sql, params object[] parameters)
        => await _dbSet.FromSqlRaw(sql, parameters).ToListAsync();
}