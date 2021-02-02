using System;
using System.Threading;
using System.Threading.Tasks;

namespace Bat.EntityFrameworkCore
{
    public interface IBatDbContext : IDisposable
    {
        void ApplyPersianYK();
        void ApplyEnglishNumber();
        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        Task<int> SaveChangesAsync(bool AcceptAllChangesOnSuccess, CancellationToken cancellationToken = default);
        Task<SaveChangeResult> BatSaveChangesAsync(CancellationToken cancellationToken = default);
        Task<SaveChangeResult> BatSaveChangesWithValidationAsync(CancellationToken cancellationToken = default);
    }
}