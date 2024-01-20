namespace Bat.Core;

public interface IRedisCacheProvider : ISingletonInjection
{
    public bool Set(string key, string value, TimeSpan? expiry = null, bool keepTTL = false);
    bool Set(KeyValuePair<string, string>[] values);
    public Task<bool> SetAsync(string key, string value, TimeSpan? expiry = null, bool keepTTL = false);
    Task<bool> SetAsync(KeyValuePair<string, string>[] values);
    public string Get(string key);
    string[] Get(string[] keys);
    public Task<string> GetAsync(string key);
    Task<string[]> GetAsync(string[] keys);
    public string GetSet(string key, string value);
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