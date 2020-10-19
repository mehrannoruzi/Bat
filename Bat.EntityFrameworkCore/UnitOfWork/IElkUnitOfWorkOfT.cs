using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Bat.EntityFrameworkCore
{
    public interface IBatUnitOfWorkOfT<TContext> : IBatUnitOfWork where TContext : DbContext
    {
        Task<int> SaveChangesAsync(bool ensureAutoHistory = false, params IBatUnitOfWork[] unitOfWorks);
    }
}