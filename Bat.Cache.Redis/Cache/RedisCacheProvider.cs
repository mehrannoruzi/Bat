using System;
using Bat.Core;
using StackExchange.Redis;
using System.Threading.Tasks;

namespace Bat.Cache.Redis
{
    public class RedisCacheProvider : IDistributedMemoryCacheProvider
    {
        public IDatabase _redisDb;
        public ConnectionMultiplexer _redisClient;

        public RedisCacheProvider()
        {
            ConfigurationOptions config = new ConfigurationOptions
            {
                EndPoints =
                {
                    { "127.0.0.1", 6379 },
                    { "127.0.0.1", 6380 }
                },
                //CommandMap = CommandMap.Create(new HashSet<string>
                //    { 
                //        // EXCLUDE a few commands
                //        "INFO", "CONFIG", "CLUSTER",
                //        "PING", "ECHO", "CLIENT"
                //    },
                //    available: false),
                //KeepAlive = 180,
                //DefaultVersion = new Version(2, 8, 8),
                //Password = "changeme"
            };


            _redisClient = ConnectionMultiplexer.Connect(config);
            _redisDb = _redisClient.GetDatabase();
        }

        public RedisCacheProvider(ConnectionMultiplexer redisClient)
        {
            _redisClient = redisClient;
            _redisDb = _redisClient.GetDatabase();
        }

        public RedisCacheProvider(ConnectionMultiplexer redisClient, IDatabase database)
        {
            _redisClient = redisClient;
            _redisDb = database;
        }



        public bool Set(string key, string value, TimeSpan? expiry = null) => _redisDb.StringSet(key, value, expiry);
        public async Task<bool> SetAsync(string key, string value, TimeSpan? expiry = null) => await _redisDb.StringSetAsync(key, value, expiry);
        public string Get(string key) => _redisDb.StringGet(key);
        public async Task<string> GetAsync(string key) => await _redisDb.StringGetAsync(key);
        public bool Delete(string key) => _redisDb.KeyDelete(key);
        public async Task<bool> DeleteAsync(string key) => await _redisDb.KeyDeleteAsync(key);
        public void Rename(string oldKey, string newKey) => _redisDb.KeyRename(oldKey, newKey);
        public async Task RenameAsync(string oldKey, string newKey) => await _redisDb.KeyRenameAsync(oldKey, newKey);
        public bool Exists(string Key) => _redisDb.KeyExists(Key);
        public async Task<bool> ExistsAsync(string Key) => await _redisDb.KeyExistsAsync(Key);
        public bool Expire(string Key, TimeSpan? expiry = null) => _redisDb.KeyExpire(Key, expiry);
        public async Task<bool> ExpireAsync(string Key, TimeSpan? expiry = null) => await _redisDb.KeyExpireAsync(Key, expiry);
        public bool Persist(string Key) => _redisDb.KeyPersist(Key);
        public async Task<bool> PersistAsync(string Key) => await _redisDb.KeyPersistAsync(Key);
        public TimeSpan? Idle(string Key) => _redisDb.KeyIdleTime(Key);
        public async Task<TimeSpan?> IdleAsync(string Key) => await _redisDb.KeyIdleTimeAsync(Key);
        public long Increment(string Key) => _redisDb.StringIncrement(Key);
        public async Task<long> IncrementAsync(string Key) => await _redisDb.StringIncrementAsync(Key);
        public long Decrement(string Key) => _redisDb.StringDecrement(Key);
        public async Task<long> DecrementAsync(string Key) => await _redisDb.StringDecrementAsync(Key);
    }
}