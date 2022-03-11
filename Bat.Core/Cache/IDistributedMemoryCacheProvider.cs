namespace Bat.Core;

public interface IDistributedMemoryCacheProvider : ISingletonInjection
{
    public Task<bool> SetAsync(string key, string value, TimeSpan? expiry = null);
    public Task<string> GetAsync(string key);
    public Task<bool> DeleteAsync(string key);
    public Task RenameAsync(string oldKey, string newKey);
    public Task<bool> ExistsAsync(string Key);
    public Task<bool> ExpireAsync(string Key, TimeSpan? expiry = null);
    public Task<bool> PersistAsync(string Key);
    public Task<TimeSpan?> IdleAsync(string Key);
    public Task<long> IncrementAsync(string Key);
    public Task<long> DecrementAsync(string Key);
}