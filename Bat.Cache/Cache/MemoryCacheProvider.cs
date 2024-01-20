namespace Bat.Cache;

public class MemoryCacheProvider : IMemoryCacheProvider
{
    private readonly MemoryCache _cache;
    public readonly string _cacheName = "Bat.MemoryCache";

    public MemoryCacheProvider()
    {
        _cache = new MemoryCache(_cacheName);
    }

    public MemoryCacheProvider(string cacheName)
    {
        _cacheName = cacheName;
        _cache = new MemoryCache(cacheName);
    }


    public bool Set(string key, object value, DateTimeOffset expireTime) => _cache.Add(key, value, expireTime);

    public bool Add(string key, object value, CacheItemPolicy cachePolicy) => _cache.Add(key, value, cachePolicy);

    public bool Set(string key, object value, DateTimeOffset expireTime, TimeSpan slidingTime) => _cache.Add(key, value, new CacheItemPolicy { AbsoluteExpiration = expireTime, SlidingExpiration = slidingTime });

    public object GetSet(string key, object value, DateTimeOffset expireTime) => _cache.AddOrGetExisting(key, value, expireTime);

    public object AddOrGetExisting(string key, object value, CacheItemPolicy cachePolicy) => _cache.AddOrGetExisting(key, value, cachePolicy);

    public bool Delete(string key) { var cacheItem = _cache.Remove(key); return cacheItem != null; }

    public object Get(string key) => _cache.Get(key);

    public CacheItem GetCacheItem(string key) => _cache.GetCacheItem(key);

    public IDictionary<string, object> GetAll(IEnumerable<string> keys) => _cache.GetValues(keys);

    public long GetCount() => _cache.GetCount();

    public long GetSize() => _cache.GetLastSize();

    public long GetMemoryLimit() => _cache.PhysicalMemoryLimit;

    public object TrimMemory(int percentage) => _cache.Trim(percentage);
}