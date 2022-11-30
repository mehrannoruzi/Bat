namespace Bat.EntityFrameworkCore;

public interface IBatUnitOfWork : IRepositoryFactory, IDisposable
{
    public DatabaseFacade Database { get; }
    public ChangeTracker ChangeTracker { get; }


    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    Task<SaveChangeResult> BatSaveChangesAsync(CancellationToken cancellationToken = default);
    Task<SaveChangeResult> BatSaveChangesWithValidationAsync(CancellationToken cancellationToken = default);
}