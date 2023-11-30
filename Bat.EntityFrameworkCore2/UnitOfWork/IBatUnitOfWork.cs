namespace Bat.EntityFrameworkCore;

public interface IBatUnitOfWork : IDisposable
{
	public DatabaseFacade Database { get; }
	public ChangeTracker ChangeTracker { get; }


	int SaveChangesAsync();
	Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
	SaveChangeResult BatSaveChangesAsync();
	Task<SaveChangeResult> BatSaveChangesAsync(CancellationToken cancellationToken = default);
	SaveChangeResult BatSaveChangesWithValidationAsync();
	Task<SaveChangeResult> BatSaveChangesWithValidationAsync(CancellationToken cancellationToken = default);
}