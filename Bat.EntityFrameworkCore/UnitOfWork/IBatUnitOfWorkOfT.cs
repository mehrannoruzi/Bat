﻿namespace Bat.EntityFrameworkCore;

public interface IBatUnitOfWork<TContext> : IBatUnitOfWork, IDisposable, IAsyncDisposable where TContext : BatDbContext
{
    Task<int> SaveChangesAsync(bool ensureAutoHistory = false, params IBatUnitOfWork[] unitOfWorks);
}