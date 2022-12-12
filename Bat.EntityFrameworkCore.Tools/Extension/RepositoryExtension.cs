namespace Bat.EntityFrameworkCore.Tools;

public static class RepositoryExtension
{
    public static EFGenericRepo<T> GetRepository<T>(this IServiceProvider serviceProvider) where T : class, IBaseEntity
        => (EFGenericRepo<T>)serviceProvider.GetService<IEFGenericRepo<T>>();

    public static EFBulkGenericRepo<T> GetBulkRepository<T>(this IServiceProvider serviceProvider) where T : class, IBaseEntity
        => (EFBulkGenericRepo<T>)serviceProvider.GetService<IEFBulkGenericRepo<T>>();

}