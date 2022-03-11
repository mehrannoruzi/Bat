namespace Bat.Dapper;

public class DapperGenericRepo<T> : IDapperGenericRepo<T> where T : class
{
    public SqlConnection _sqlConnection;

    public DapperGenericRepo(string connectionString)
        => _sqlConnection = new SqlConnection(connectionString);

    public async Task<int> AddAsync(string sqlStatement, object parameters = null, IDbTransaction transaction = null, int? commandTimOut = null)
        => await _sqlConnection.ExecuteAsync(sqlStatement, parameters, transaction, commandTimOut);

    public async Task<bool> UpdateAsync(string sqlStatement, object parameters = null, IDbTransaction transaction = null, int? commandTimOut = null)
        => await _sqlConnection.ExecuteAsync(sqlStatement, parameters, transaction, commandTimOut) > 0;

    public async Task<bool> DeleteAsync(string sqlStatement, object parameters = null, IDbTransaction transaction = null, int? commandTimOut = null)
        => await _sqlConnection.ExecuteAsync(sqlStatement, parameters, transaction, commandTimOut) > 0;

    public async Task<T> FirstOrDefaultAsync(string sqlStatement, object parameters = null, IDbTransaction transaction = null, int? commandTimOut = null)
        => await _sqlConnection.QueryFirstOrDefaultAsync<T>(sqlStatement, parameters, transaction, commandTimOut);

    public async Task<bool> AnyAsync(string sqlStatement, object parameters = null, IDbTransaction transaction = null, int? commandTimOut = null)
        => await _sqlConnection.QueryFirstOrDefaultAsync<T>(sqlStatement, parameters, transaction, commandTimOut) != null;

    public List<T> Get(string sqlStatement, object parameters = null, IDbTransaction transaction = null, bool buffered = true, int? commandTimOut = null)
        => _sqlConnection.Query<T>(sqlStatement, parameters, transaction, buffered, commandTimOut).ToList();

    public PagingListDetails<T> GetPaging(string sqlStatement, PagingParameter pagingParameter, object parameters = null, IDbTransaction transaction = null, bool buffered = false, int? commandTimOut = null)
        => _sqlConnection.Query<T>(sqlStatement, parameters, transaction, buffered, commandTimOut).AsQueryable().ToPagingListDetails(pagingParameter);
}