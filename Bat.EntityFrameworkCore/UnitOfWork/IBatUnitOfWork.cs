using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Bat.EntityFrameworkCore;

public interface IBatUnitOfWork : IRepositoryFactory, IDisposable
{
    public DatabaseFacade Database { get; }
    public ChangeTracker ChangeTracker { get; }

    //public IGenericRepo<TEntity> GetRepository<TEntity>() where TEntity : class, IBaseEntity;
    //Task<List<TResult>> ExecuteProcedure<TResult>(string sqlQuery, params object[] parameters) where TResult : class;

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    Task<SaveChangeResult> BatSaveChangesAsync(CancellationToken cancellationToken = default);
    Task<SaveChangeResult> BatSaveChangesWithValidationAsync(CancellationToken cancellationToken = default);
}