using Microsoft.Extensions.DependencyInjection;

namespace Bat.EntityFrameworkCore.Tools;

public static class RepositoryExtension
{
    public static IEFGenericRepo<T> GetRepository<T>(this IServiceProvider serviceProvider) where T : class, IBaseEntity
    {
        return serviceProvider.GetService<IEFGenericRepo<T>>();
    }
}