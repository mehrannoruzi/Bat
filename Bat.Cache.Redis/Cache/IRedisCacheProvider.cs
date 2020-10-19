using System;
using Bat.Core;
using System.Threading.Tasks;

namespace Bat.Cache.Redis
{
    public interface IRedisCacheProvider : ISingletonInjection
    {
        public bool Set(string key, string value, TimeSpan? expiry = null);
        public Task<bool> SetAsync(string key, string value, TimeSpan? expiry = null);
        public string Get(string key);
        public Task<string> GetAsync(string key);
        public bool Delete(string key);
        public Task<bool> DeleteAsync(string key);
        public void Rename(string oldKey, string newKey);
        public Task RenameAsync(string oldKey, string newKey);
        public bool Exists(string Key);
        public Task<bool> ExistsAsync(string Key);
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
}
