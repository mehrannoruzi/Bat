using System;
using Bat.Core;
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

        public IGenericRepo<TEntity> GetRepository<TEntity>() where TEntity : class, IBaseEntity;

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        Task<SaveChangeResult> BatSaveChangesAsync(CancellationToken cancellationToken = default);
        Task<SaveChangeResult> BatSaveChangesWithValidationAsync(CancellationToken cancellationToken = default);
    }
}