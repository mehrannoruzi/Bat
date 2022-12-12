namespace Bat.EntityFrameworkCore.Tools;

public class BulkRepositoryFactory : IBulkRepositoryFactory
{
    private readonly IServiceProvider _serviceProvider;

    public BulkRepositoryFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }


    public EFBulkGenericRepo<T> GetBulkRepository<T>() where T : class, IBaseEntity
        => (EFBulkGenericRepo<T>)_serviceProvider.GetService<IEFBulkGenericRepo<T>>();

}