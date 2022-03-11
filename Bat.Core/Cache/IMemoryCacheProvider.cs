namespace Bat.Core;

public interface IMemoryCacheProvider : ISingletonInjection
{
    bool Add(string key, object value, DateTimeOffset expireTime);
    bool Add(string key, object value, DateTimeOffset expireTime, TimeSpan slidingTime);
    object AddOrGetExisting(string key, object value, DateTimeOffset expireTime);
    bool Remove(string key);
    object Get(string key);
    IDictionary<string, object> GetAllItem(IEnumerable<string> keys);
    long GetCount();
    long GetSize();
    long GetMemoryLimit();
    object TrimMemory(int percentage);
}