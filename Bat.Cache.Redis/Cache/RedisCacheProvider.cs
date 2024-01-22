namespace Bat.Cache.Redis;

public class RedisCacheProvider : IRedisCacheProvider
{
    public IDatabase _redisDb;
    public ConnectionMultiplexer _redisClient;
    private readonly RedisSettings _redisSettings;

    public RedisCacheProvider(IOptions<RedisSettings> redisSettings)
    {
        _redisSettings = redisSettings.Value;
        if (_redisSettings.Server1.IsNullOrWhiteSpace() ||
            _redisSettings.Server1.IsNullOrWhiteSpace() ||
            _redisSettings.Server1.IsNullOrWhiteSpace())
            throw new Exception("redisSettings not configured properly !");

        ConfigurationOptions config = new();
        if (_redisSettings.Server1.IsNullOrWhiteSpace() is false)
            config.EndPoints.Add(_redisSettings.Server1, _redisSettings.Port1);
        if (_redisSettings.Server2.IsNullOrWhiteSpace() is false)
            config.EndPoints.Add(_redisSettings.Server2, _redisSettings.Port2);
        if (_redisSettings.Server3.IsNullOrWhiteSpace() is false)
            config.EndPoints.Add(_redisSettings.Server3, _redisSettings.Port3);

        config.AllowAdmin = _redisSettings.AllowAdminCommand;
        config.User = _redisSettings.Username ?? null;
        config.Password = _redisSettings.Password ?? null;
        config.ConnectRetry = _redisSettings.ConnectRetry;
        config.SyncTimeout = _redisSettings.SyncTimeout;
        config.ConnectTimeout = _redisSettings.ConnectTimeout;
        config.ClientName = _redisSettings.ClientName ?? null;
        config.DefaultDatabase = _redisSettings.DefaultDatabaseIndex;
        config.IncludeDetailInExceptions = _redisSettings.IncludeDetailInExceptions;
        config.CheckCertificateRevocation = _redisSettings.CheckCertificateRevocation;

        config.Ssl = _redisSettings.SslSettings.UseSsl;
        config.SslHost = _redisSettings.SslSettings.SslHost ?? null;
        //config.SslClientAuthenticationOptions = (sslTargetHost) =>
        //{
        //    return new SslClientAuthenticationOptions
        //    {
        //        TargetHost = sslTargetHost,
        //        AllowRenegotiation = true,
        //        AllowTlsResume = true,
        //    };
        //};

        _redisClient = ConnectionMultiplexer.Connect(config);
        _redisDb = _redisClient.GetDatabase();
    }

    public RedisCacheProvider(ConnectionMultiplexer redisClient, IDatabase database = null)
    {
        _redisClient = redisClient;
        if (database is null)
            _redisDb = _redisClient.GetDatabase();
        else
            _redisDb = database;
    }

    public RedisCacheProvider(ConfigurationOptions configurationOptions, IDatabase database = null)
    {
        _redisClient = ConnectionMultiplexer.Connect(configurationOptions);
        if (database is null)
            _redisDb = _redisClient.GetDatabase();
        else
            _redisDb = database;
    }


    public bool Set(string key, string value, TimeSpan? expiry = null, bool keepTTL = false)
        => _redisDb.StringSet(key, value, expiry, keepTTL);

    public bool Set(KeyValuePair<string, string>[] values)
    {
        var list = new KeyValuePair<RedisKey, RedisValue>[values.Length];
        values.CopyTo(list, 0);
        return _redisDb.StringSet(list);
    }

    public async Task<bool> SetAsync(string key, string value, TimeSpan? expiry = null, bool keepTTL = false)
        => await _redisDb.StringSetAsync(key, value, expiry, keepTTL);

    public async Task<bool> SetAsync(KeyValuePair<string, string>[] values)
    {
        var list = new KeyValuePair<RedisKey, RedisValue>[values.Length];
        values.CopyTo(list, 0);
        return await _redisDb.StringSetAsync(list);
    }

    public string Get(string key)
        => _redisDb.StringGet(key);

    public string[] Get(string[] keys)
    {
        var list = new RedisKey[keys.Length];
        keys.CopyTo(list, 0);
        return _redisDb.StringGet(list).ToStringArray();
    }

    public async Task<string> GetAsync(string key)
        => await _redisDb.StringGetAsync(key);

    public async Task<string[]> GetAsync(string[] keys)
    {
        var list = new RedisKey[keys.Length];
        keys.CopyTo(list, 0);
        var result = await _redisDb.StringGetAsync(list);
        return result.ToStringArray();
    }

    public string GetSet(string key, string value)
        => _redisDb.StringGetSet(key, value);

    public async Task<string> GetSetAsync(string key, string value)
        => await _redisDb.StringGetSetAsync(key, value);

    public bool Delete(string key)
        => _redisDb.KeyDelete(key);

    public async Task<bool> DeleteAsync(string key)
        => await _redisDb.KeyDeleteAsync(key);

    public void Rename(string oldKey, string newKey)
        => _redisDb.KeyRename(oldKey, newKey);

    public async Task RenameAsync(string oldKey, string newKey)
        => await _redisDb.KeyRenameAsync(oldKey, newKey);

    public bool Exists(string Key)
        => _redisDb.KeyExists(Key);

    public async Task<bool> ExistsAsync(string Key)
        => await _redisDb.KeyExistsAsync(Key);

    public long Length(string Key)
        => _redisDb.StringLength(Key);

    public async Task<long> LengthAsync(string Key)
        => await _redisDb.StringLengthAsync(Key);

    public bool Expire(string Key, TimeSpan? expiry = null)
        => _redisDb.KeyExpire(Key, expiry);

    public async Task<bool> ExpireAsync(string Key, TimeSpan? expiry = null)
        => await _redisDb.KeyExpireAsync(Key, expiry);


    public bool Persist(string Key) =>
        _redisDb.KeyPersist(Key);

    public async Task<bool> PersistAsync(string Key)
        => await _redisDb.KeyPersistAsync(Key);

    public TimeSpan? Idle(string Key)
        => _redisDb.KeyIdleTime(Key);

    public async Task<TimeSpan?> IdleAsync(string Key)
        => await _redisDb.KeyIdleTimeAsync(Key);

    public long Increment(string Key)
        => _redisDb.StringIncrement(Key);

    public async Task<long> IncrementAsync(string Key)
        => await _redisDb.StringIncrementAsync(Key);

    public long Decrement(string Key)
        => _redisDb.StringDecrement(Key);

    public async Task<long> DecrementAsync(string Key)
        => await _redisDb.StringDecrementAsync(Key);
}