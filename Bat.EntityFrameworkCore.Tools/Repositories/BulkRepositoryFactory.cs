namespace Bat.EntityFrameworkCore.Tools;

public class BulkRepositoryFactory(IServiceProvider serviceProvider) : IBulkRepositoryFactory
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;


    public EFBulkGenericRepo<T> GetBulkRepository<T>() where T : class, IBaseEntity
        => (EFBulkGenericRepo<T>)_serviceProvider.GetService<IEFBulkGenericRepo<T>>();

    public void Dispose()
    {
        GC.SuppressFinalize(this);
        this.Dispose();
    }

    public ValueTask DisposeAsync()
    {
        GC.SuppressFinalize(this);
        return DisposeAsync();
    }
}