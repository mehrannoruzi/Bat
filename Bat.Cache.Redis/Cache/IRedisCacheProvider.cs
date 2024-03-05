namespace Bat.Cache.Redis;

public interface IRedisCacheProvider : ISingletonInjection
{
    public IDatabase _redisDb { get; set; }
    public ConnectionMultiplexer _redisServer { get; set; }

    IServer GetServer(string host = null, int port = 0);
    IEnumerable<string> GetAllKey(RedisValue[] command, CommandFlags flags = CommandFlags.None);
    IEnumerable<string> GetAllKey(PagingParameter pagingParameter, string pattern = null, CommandFlags flags = CommandFlags.None);

    public bool Set(string key, string value, TimeSpan? expiry = null, bool keepTTL = false);
    public bool Set(string key, object value, TimeSpan? expiry = null, bool keepTTl = false);
    public bool Set(KeyValuePair<string, string>[] values);
    public bool Set(KeyValuePair<string, object>[] values, CommandFlags flags = CommandFlags.None);
    public Task<bool> SetAsync(string key, string value, TimeSpan? expiry = null, bool keepTTL = false);
    public Task<bool> SetAsync(string key, object value, TimeSpan? expiry = null, bool keepTtl = false);
    public Task<bool> SetAsync(KeyValuePair<string, string>[] values);
    public Task<bool> SetAsync(KeyValuePair<string, object>[] values);

    public T Get<T>(string key) where T : class;
    public string Get(string key);
    public string[] Get(string[] keys);
    public string GetSet(string key, string value);
    public Task<T> GetAsync<T>(string key);
    public Task<string> GetAsync(string key);
    public Task<string[]> GetAsync(string[] keys);
    public Task<string> GetSetAsync(string key, string value);

    public bool Delete(string key);
    public Task<bool> DeleteAsync(string key);
    public void Rename(string oldKey, string newKey);
    public Task RenameAsync(string oldKey, string newKey);
    public bool Exists(string Key);
    public Task<bool> ExistsAsync(string Key);
    public long Length(string Key);
    public Task<long> LengthAsync(string Key);
    public bool Expire(string Key, TimeSpan? expiry = null);
    public Task<bool> ExpireAsync(string Key, TimeSpan? expiry = null);

    public bool Persist(string Key);
    public Task<bool> PersistAsync(string Key);
    public TimeSpan? Idle(string Key);
    public Task<TimeSpan?> IdleAsync(string Key);
    public long Increment(string Key);
    public Task<long> IncrementAsync(string Key);
    public long Decrement(string Key);
    public Task<long> DecrementAsync(string Key);
}