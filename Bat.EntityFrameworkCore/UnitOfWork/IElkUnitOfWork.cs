using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Bat.EntityFrameworkCore
{
    public interface IBatUnitOfWork : IDisposable
    {
        public ChangeTracker ChangeTracker { get; }
        public DatabaseFacade Database { get; }

        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        Task<int> SaveChangesAsync(bool AcceptAllChangesOnSuccess, CancellationToken cancellationToken = default);
        SaveChangeResult BatSaveChanges();
        Task<SaveChangeResult> BatSaveChangesAsync(CancellationToken cancellationToken = default);
        SaveChangeResult BatSaveChangesWithValidation();
        Task<SaveChangeResult> BatSaveChangesWithValidationAsync(CancellationToken cancellationToken = default);
    }
}