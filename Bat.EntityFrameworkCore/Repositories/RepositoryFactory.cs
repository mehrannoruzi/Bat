using Microsoft.Extensions.DependencyInjection;

namespace Bat.EntityFrameworkCore;

public class RepositoryFactory
{
    private IServiceProvider _serviceProvider;

    public RepositoryFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }


    public IEFGenericRepo<T> GetRepository<T>() where T : class, IBaseEntity
        => _serviceProvider.GetService<IEFGenericRepo<T>>();
}