namespace Bat.EntityFrameworkCore;

public interface IRepositoryFactory
{
    IEFGenericRepo<T> GetRepository<T>() where T : class, IBaseEntity;
}