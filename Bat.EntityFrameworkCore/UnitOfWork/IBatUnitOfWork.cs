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

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        Task<SaveChangeResult> BatSaveChangesAsync(CancellationToken cancellationToken = default);
        Task<SaveChangeResult> BatSaveChangesWithValidationAsync(CancellationToken cancellationToken = default);
    }
}