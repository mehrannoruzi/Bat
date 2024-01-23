namespace Bat.EntityFrameworkCore;

public interface IBatUnitOfWork : IDisposable, IAsyncDisposable
{
	public DatabaseFacade Database { get; }
	public ChangeTracker ChangeTracker { get; }


	int SaveChanges();
	Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
	SaveChangeResult BatSaveChanges();
	Task<SaveChangeResult> BatSaveChangesAsync(CancellationToken cancellationToken = default);
	SaveChangeResult BatSaveChangesWithValidation();
	Task<SaveChangeResult> BatSaveChangesWithValidationAsync(CancellationToken cancellationToken = default);
}