namespace Bat.EntityFrameworkCore.Tools;

public static class DbContextFactory
{
    private static object _lock = new();
    private static SqlConnection _sqlConnection = null;
    public static string _connectionString = string.Empty;
    private static readonly Dictionary<string, object> _contextPool = new Dictionary<string, object>();

    public static BatDbContext GetInstance<TDbContext>() where TDbContext : BatDbContext, new()
    {
        lock (_lock)
        {
            if (_sqlConnection == null) _sqlConnection = new SqlConnection(_connectionString);

            var connectioOptions = new DbContextOptionsBuilder<TDbContext>()
                         .UseSqlServer(_sqlConnection).Options;
            var context = (TDbContext)Activator.CreateInstance(typeof(TDbContext), connectioOptions);

            if (_contextPool.TryGetValue(context.ToString(), out object poolledContext)) return (TDbContext)poolledContext;
            else
            {
                _contextPool.Add(context.ToString(), context);
                return context;
            }
        }
    }

    public static BatDbContext GetInstance<TDbContext>(string connectionString) where TDbContext : BatDbContext, new()
    {
        lock (_lock)
        {
            if (_sqlConnection == null) _sqlConnection = new SqlConnection(connectionString);

            var connectioOptions = new DbContextOptionsBuilder<TDbContext>()
                         .UseSqlServer(_sqlConnection).Options;
            var context = (TDbContext)Activator.CreateInstance(typeof(TDbContext), connectioOptions);

            if (_contextPool.TryGetValue(context.ToString(), out object poolledContext)) return (TDbContext)poolledContext;
            else
            {
                _contextPool.Add(context.ToString(), context);
                return context;
            }
        }
    }
}