using Microsoft.Extensions.DependencyInjection;

namespace Bat.EntityFrameworkCore.Tools;

public static class RepositoryExtension
{
    public static EFGenericRepo<T> GetRepository<T>(this IServiceProvider serviceProvider) where T : class, IBaseEntity
    {
        return (EFGenericRepo<T>)serviceProvider.GetService<IEFGenericRepo<T>>();
    }
}