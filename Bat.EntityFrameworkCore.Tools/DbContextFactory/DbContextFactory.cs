namespace Bat.EntityFrameworkCore.Tools;

public static class DbContextFactory
{
    private static readonly object _lock = new();
    public static readonly string _connectionString;
    private static SqlConnection _sqlConnection = null;
    private static readonly Dictionary<string, object> _contextPool = [];

    public static BatDbContext GetInstance<TDbContext>() where TDbContext : BatDbContext, new()
    {
        lock (_lock)
        {
            _sqlConnection ??= new SqlConnection(_connectionString);

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
            _sqlConnection ??= new SqlConnection(connectionString);

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