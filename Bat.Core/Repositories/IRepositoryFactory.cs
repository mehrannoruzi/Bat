namespace Bat.Core;

public interface IRepositoryFactory
{
    IGenericRepo<T> GetRepository<T>() where T : class, IBaseEntity;
}