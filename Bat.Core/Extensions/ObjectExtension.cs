namespace Bat.Core;

public static class ObjectExtension
{
    public static T GetInstance<T>(this T obj) where T : new() => obj == null ? new T() : obj;


    /// <summary>
    /// Copy Given Object To New Object
    /// </summary>
    /// <typeparam name="TDestination">This Object</typeparam>
    /// <param name="sourceObject">Given Object</param>
    public static TDestination CopyFrom<TDestination>(object sourceObject) where TDestination : class, new()
    {
        var result = new TDestination();

        var sourceProperties = sourceObject.GetType().GetProperties();
        var destinationProperties = result.GetType().GetProperties();
        System.Reflection.PropertyInfo tempProperty;
        foreach (var property in destinationProperties)
        {
            try
            {
                tempProperty = sourceProperties.FirstOrDefault(x => x.Name == property.Name && x.CanWrite);
                if (tempProperty != null) property.SetValue(result, tempProperty.GetValue(sourceObject));
            }
            catch { }
        }

        return result;
    }


    /// <summary>
    /// Copy Given Object To This Object
    /// </summary>
    /// <typeparam name="TDestination">This Object</typeparam>
    /// <typeparam name="TSource">Given Object</typeparam>
    /// <param name="destinationObject">This Object</param>
    /// <param name="sourceObject">Given Object</param>
    public static TDestination CopyFrom<TDestination, TSource>(this TDestination destinationObject, TSource sourceObject) where TDestination : class where TSource : class
    {
        var sourceProperties = sourceObject.GetType().GetProperties();
        var destinationProperties = destinationObject.GetType().GetProperties();
        System.Reflection.PropertyInfo tempProperty;
        foreach (var property in destinationProperties)
        {
            try
            {
                tempProperty = sourceProperties.FirstOrDefault(x => x.Name == property.Name && x.CanWrite);
                if (tempProperty != null) property.SetValue(destinationObject, tempProperty.GetValue(sourceObject));
            }
            catch { }
        }

        return destinationObject;
    }


    /// <summary>
    /// Update This Object with Given Object 
    /// </summary>
    /// <typeparam name="TDestination">This Object</typeparam>
    /// <typeparam name="TSource">Given Object</typeparam>
    /// <param name="destinationObject">This Object</param>
    /// <param name="sourceObject">Given Object</param>
    public static void UpdateWith<TDestination, TSource>(this TDestination destinationObject, TSource sourceObject) where TDestination : class where TSource : class
    {
        var sourceProperties = sourceObject.GetType().GetProperties();
        var destinationProperties = destinationObject.GetType().GetProperties();
        System.Reflection.PropertyInfo tempProperty;
        foreach (var property in destinationProperties)
        {
            try
            {
                tempProperty = sourceProperties.FirstOrDefault(x => x.Name == property.Name);
                if (tempProperty != null) property.SetValue(destinationObject, tempProperty.GetValue(sourceObject));
            }
            catch { }
        }
    }


    public static void SetProperty<TEntity, TItem>(this TEntity objec, string name, TItem value)
        => typeof(TEntity).GetProperty(name).SetValue(objec, value, null);

    public static TItem GetProperty<TEntity, TItem>(this TEntity objec, string name) where TEntity : class
    {
        if (objec != null)
        {
            var value = typeof(TEntity).GetProperty(name).GetValue(objec, null);
            if (value != null)
            {
                return (TItem)(value);
            }
        }
        return default;
    }

    public static object GetProperty(this object objec, string name)
        => objec == null ? objec.GetType().GetProperty(name).GetValue(objec, null) : null;

}