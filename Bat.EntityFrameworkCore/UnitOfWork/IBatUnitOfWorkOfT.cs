using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Bat.EntityFrameworkCore
{
    public interface IBatUnitOfWorkOfT<TContext> : IBatUnitOfWork, IDisposable where TContext : BatDbContext
    {
        Task<int> SaveChangesAsync(bool ensureAutoHistory = false, params IBatUnitOfWork[] unitOfWorks);
    }
}