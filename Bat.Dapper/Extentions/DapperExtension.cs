namespace Bat.Dapper;

public static class DapperExtension
{
    public static async Task<T> ExecuteQuerySingleAsync<T>(this SqlConnection sqlConnection, string sqlStatement, object parameters = null,
        IDbTransaction transaction = null, int? commandTimOut = null)
        => await sqlConnection.QueryFirstOrDefaultAsync<T>(sql: sqlStatement, param: parameters, transaction: transaction, commandTimeout: commandTimOut, commandType: CommandType.Text);

    public static async Task<IEnumerable<T>> ExecuteQueryAsync<T>(this SqlConnection sqlConnection, string sqlStatement, object parameters = null,
        IDbTransaction transaction = null, int? commandTimOut = null)
        => await sqlConnection.QueryAsync<T>(sql: sqlStatement, param: parameters, transaction: transaction, commandTimeout: commandTimOut, commandType: CommandType.Text);

    public static async Task<bool> ExecuteQueryCommandAsync(this SqlConnection sqlConnection, string sqlCommand, object parameters = null,
        IDbTransaction transaction = null, int? commandTimOut = null)
        => await sqlConnection.ExecuteAsync(sql: sqlCommand, param: parameters, transaction: transaction, commandTimeout: commandTimOut, commandType: CommandType.Text) > 0;



    /// <summary>
    /// var user = sqlConnection.ExecuteSp<User>("[Auth].[GetUser]", new { UserId = 1 });
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="sqlConnection"></param>
    /// <param name="spName"></param>
    /// <param name="parameters"></param>
    /// <param name="transaction"></param>
    /// <param name="commandTimOut"></param>
    /// <returns></returns>
    public static T ExecuteSpSingle<T>(this SqlConnection sqlConnection, string spName, object parameters = null,
        IDbTransaction transaction = null, int? commandTimOut = null)
        => sqlConnection.Query<T>(spName, parameters, transaction, false, commandTimOut, CommandType.StoredProcedure).FirstOrDefault();

    public static async Task<T> ExecuteSpSingleAsync<T>(this SqlConnection sqlConnection, string spName, object parameters = null,
        IDbTransaction transaction = null, int? commandTimOut = null)
    {
        var result = await sqlConnection.QueryAsync<T>(spName, parameters, transaction, commandTimOut, CommandType.StoredProcedure);
        return result.FirstOrDefault();
    }


    /// <summary>
    /// var category = sqlConnection.ExecuteSpList<Category>("[Content].[GetCategories]", new { CategoryId = 1 });
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="sqlConnection"></param>
    /// <param name="spName"></param>
    /// <param name="parameters"></param>
    /// <param name="transaction"></param>
    /// <param name="commandTimOut"></param>
    /// <returns></returns>
    public static IEnumerable<T> ExecuteSpList<T>(this SqlConnection sqlConnection, string spName, object parameters = null,
    IDbTransaction transaction = null, int? commandTimOut = null)
    => sqlConnection.Query<T>(spName, parameters, transaction, false, commandTimOut, CommandType.StoredProcedure);

    public static async Task<IEnumerable<T>> ExecuteSpListAsync<T>(this SqlConnection sqlConnection, string spName, object parameters = null,
        IDbTransaction transaction = null, int? commandTimOut = null)
        => await sqlConnection.QueryAsync<T>(spName, parameters, transaction, commandTimOut, CommandType.StoredProcedure);

    public static async Task<bool> ExecuteSpCommandAsync(this SqlConnection sqlConnection, string spName, object parameters = null,
        IDbTransaction transaction = null, int? commandTimOut = null)
        => await sqlConnection.ExecuteAsync(spName, parameters, transaction, commandTimOut, CommandType.StoredProcedure) > 0;



    /// <summary>
    /// var multi = sqlConnection.ExecuteQueryMultiple(sql, new {id=selectedId})
    /// var customer = multi.Read<Customer>().Single();
    /// var orders = multi.Read<Order>().ToList();
    /// var returns = multi.Read<Return>().ToList();
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="sqlConnection"></param>
    /// <param name="sqlStatement"></param>
    /// <param name="parameters"></param>
    /// <param name="transaction"></param>
    /// <param name="commandTimOut"></param>
    /// <returns></returns>
    public static async Task<SqlMapper.GridReader> ExecuteQueryMultipleAsync(this SqlConnection sqlConnection, string sqlStatement, object parameters = null,
        IDbTransaction transaction = null, int? commandTimOut = null)
        => await sqlConnection.QueryMultipleAsync(sql: sqlStatement, param: parameters, transaction: transaction, commandTimeout: commandTimOut, commandType: CommandType.Text);

    /// <summary>
    /// var data = sqlConnection.ExecuteQueryWithNavigation<Post, User, Post>(sql, (post, user) => { post.Owner = user; return post;});
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="sqlConnection"></param>
    /// <param name="sqlStatement"></param>
    /// <param name="map"></param>
    /// <param name="parameters"></param>
    /// <param name="transaction"></param>
    /// <param name="commandTimOut"></param>
    /// <returns></returns>
    public static IEnumerable<TResult> ExecuteQueryWithNavigation<TFirst, TSecond, TResult>(this SqlConnection sqlConnection, string sqlStatement, System.Func<TFirst, TSecond, TResult> map, object parameters = null,
        IDbTransaction transaction = null, string splitOn = "Id", int? commandTimOut = null)
        => sqlConnection.Query<TFirst, TSecond, TResult>(sql: sqlStatement, map: map, param: parameters, transaction: transaction, buffered: false, splitOn: splitOn, commandTimeout: commandTimOut);

}