namespace Bat.EntityFrameworkCore;

public interface IRepositoryFactory
{
    EFGenericRepo<T> GetRepository<T>() where T : class, IBaseEntity;
}