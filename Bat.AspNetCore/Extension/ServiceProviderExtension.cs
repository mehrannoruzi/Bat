namespace Bat.AspNetCore;

public static class ServiceProviderExtension
{
    public static IGenericRepo<T> GetRepository<T>(this IServiceProvider serviceProvider) where T : class, IBaseEntity
    {
        return serviceProvider.GetService<IGenericRepo<T>>();
    }
}