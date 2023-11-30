namespace Bat.EntityFrameworkCore.Tools;

public interface IBulkRepositoryFactory : IDisposable, IAsyncDisposable
{
    EFBulkGenericRepo<T> GetBulkRepository<T>() where T : class, IBaseEntity;
}