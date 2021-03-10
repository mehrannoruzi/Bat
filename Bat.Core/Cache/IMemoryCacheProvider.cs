using System;
using System.Collections.Generic;

namespace Bat.Core
{
    public interface IMemoryCacheProvider : ISingletonInjection
    {
        bool Add(string key, object value, DateTimeOffset expireTime);
        //bool Add(string key, object value, CacheItemPolicy cachePolicy);
        bool Add(string key, object value, DateTimeOffset expireTime, TimeSpan slidingTime);
        object AddOrGetExisting(string key, object value, DateTimeOffset expireTime);
        //object AddOrGetExisting(string key, object value, CacheItemPolicy cachePolicy);
        bool Remove(string key);
        object Get(string key);
        //CacheItem GetCacheItem(string key);
        IDictionary<string, object> GetAllItem(IEnumerable<string> keys);
        long GetCount();
        long GetSize();
        long GetMemoryLimit();
        object TrimMemory(int percentage);
    }
}