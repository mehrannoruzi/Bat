namespace Bat.Core;

public interface IMemoryCacheProvider : ISingletonInjection
{
    bool Set(string key, object value, DateTimeOffset expireTime);
    bool Set(string key, object value, DateTimeOffset expireTime, TimeSpan slidingTime);
    object Get(string key);
    object GetSet(string key, object value, DateTimeOffset expireTime);
    IDictionary<string, object> GetAll(IEnumerable<string> keys);
    bool Delete(string key);

    long GetCount();
    long GetSize();
    long GetMemoryLimit();
    object TrimMemory(int percentage);
}