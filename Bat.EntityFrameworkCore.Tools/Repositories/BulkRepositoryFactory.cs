namespace Bat.EntityFrameworkCore.Tools;

public class BulkRepositoryFactory(IServiceProvider serviceProvider) : IBulkRepositoryFactory
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;


    public void Dispose()
    {
        throw new NotImplementedException();
    }

    public ValueTask DisposeAsync()
    {
        throw new NotImplementedException();
    }

    public EFBulkGenericRepo<T> GetBulkRepository<T>() where T : class, IBaseEntity
        => (EFBulkGenericRepo<T>)_serviceProvider.GetService<IEFBulkGenericRepo<T>>();

}