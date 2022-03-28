namespace Bat.Dapper;

public interface IDapperGenericRepo<T>
{
    Task<int> AddAsync(string sqlStatement, object parameters = null, IDbTransaction transaction = null, int? commandTimOut = null);
    Task<bool> UpdateAsync(string sqlStatement, object parameters = null, IDbTransaction transaction = null, int? commandTimOut = null);
    Task<bool> DeleteAsync(string sqlStatement, object parameters = null, IDbTransaction transaction = null, int? commandTimOut = null);
    Task<T> FirstOrDefaultAsync(string sqlStatement, object parameters = null, IDbTransaction transaction = null, int? commandTimOut = null);
    Task<bool> AnyAsync(string sqlStatement, object parameters = null, IDbTransaction transaction = null, int? commandTimOut = null);
    List<T> Get(string sqlStatement, object parameters = null, IDbTransaction transaction = null, bool buffered = true, int? commandTimOut = null);
    PagingListDetails<T> GetPaging(string sqlStatement, PagingParameter pagingParameter, object parameters = null, IDbTransaction transaction = null, bool buffered = false, int? commandTimOut = null);
}