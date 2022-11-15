namespace Bat.EntityFrameworkCore;

public class RepositoryFactory : IRepositoryFactory
{
    private readonly IServiceProvider _serviceProvider;

    public RepositoryFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }


    public EFGenericRepo<T> GetRepository<T>() where T : class, IBaseEntity
        => (EFGenericRepo<T>)_serviceProvider.GetService<IEFGenericRepo<T>>();
        // => _serviceProvider.GetRequiredService<EFGenericRepo<T>>();

}