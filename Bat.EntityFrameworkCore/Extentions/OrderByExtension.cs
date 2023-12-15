namespace Bat.EntityFrameworkCore;

public static class OrderByExtension
{
    private static readonly char[] separator = [' '];
    private static readonly char[] separatorArray = [','];

    private static Expression OrderBy(this Expression source, string orderBy)
    {
        if (!string.IsNullOrWhiteSpace(orderBy))
        {
            var orderBys = orderBy.Split(separatorArray, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < orderBys.Length; i++)
                source = AddOrderBy(source, orderBys[i], i);
        }

        return source;
    }

    private static Expression AddOrderBy(Expression source, string orderBy, int index)
    {
        var orderByParams = orderBy.Trim().Split(separator, StringSplitOptions.RemoveEmptyEntries);
        string orderByMethodName = index == 0 ? "OrderBy" : "ThenBy";
        string parameterPath = orderByParams[0];
        if (orderByParams.Length > 1 && orderByParams[1].Equals("desc", StringComparison.OrdinalIgnoreCase))
            orderByMethodName += "Descending";

        var sourceType = source.Type.GetGenericArguments().First();
        var parameterExpression = Expression.Parameter(sourceType, "p");
        var orderByExpression = BuildPropertyPathExpression(parameterExpression, parameterPath);
        var orderByFuncType = typeof(Func<,>).MakeGenericType(sourceType, orderByExpression.Type);
        var orderByLambda = Expression.Lambda(orderByFuncType, orderByExpression, new ParameterExpression[] { parameterExpression });

        source = Expression.Call(typeof(Queryable), orderByMethodName, new Type[] { sourceType, orderByExpression.Type }, source, orderByLambda);
        return source;
    }

    private static Expression BuildPropertyPathExpression(this Expression rootExpression, string propertyPath)
    {
        var parts = propertyPath.Split(['.'], 2);
        var currentProperty = parts[0];
        var propertyDescription = rootExpression.Type.GetProperty(currentProperty,
            BindingFlags.IgnoreCase
            | BindingFlags.Instance
            | BindingFlags.Public);
        if (propertyDescription is null)
            throw new KeyNotFoundException($"Cannot find property {rootExpression.Type.Name}.{currentProperty}. The root expression is {rootExpression} and the full path would be {propertyPath}.");

        var propExpr = Expression.Property(rootExpression, propertyDescription);
        if (parts.Length > 1)
            return BuildPropertyPathExpression(propExpr, parts[1]);

        return propExpr;
    }

    private static Expression<Func<T, object>> ToLambda<T>(string propertyName)
    {
        var parameter = Expression.Parameter(typeof(T));
        var property = Expression.Property(parameter, propertyName);
        var propAsObject = Expression.Convert(property, typeof(object));

        return Expression.Lambda<Func<T, object>>(propAsObject, parameter);
    }

    public static IOrderedQueryable<T> OrderBy<T>(this IQueryable<T> source, string propertyName)
    {
        return source.OrderBy(ToLambda<T>(propertyName));
    }

    public static IOrderedQueryable<T> OrderByDescending<T>(this IQueryable<T> source, string propertyName)
    {
        return source.OrderByDescending(ToLambda<T>(propertyName));
    }
}