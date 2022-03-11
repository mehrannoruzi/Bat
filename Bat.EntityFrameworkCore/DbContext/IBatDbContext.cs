namespace Bat.EntityFrameworkCore;

public interface IBatDbContext : IDisposable
{
    void ApplyPersianYK();
    void ApplyEnglishNumber();

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    Task<SaveChangeResult> BatSaveChangesAsync(CancellationToken cancellationToken = default);
    Task<SaveChangeResult> BatSaveChangesWithValidationAsync(CancellationToken cancellationToken = default);
}