namespace Bat.EntityFrameworkCore.Tools;

public interface IBulkRepositoryFactory
{
    EFBulkGenericRepo<T> GetBulkRepository<T>() where T : class, IBaseEntity;
}