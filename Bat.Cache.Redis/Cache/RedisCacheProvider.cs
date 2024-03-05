namespace Bat.Cache.Redis;

public class RedisCacheProvider : IRedisCacheProvider
{
    private readonly RedisSettings _redisSettings;
    public IDatabase _redisDb { get; set; }
    public ConnectionMultiplexer _redisServer { get; set; }

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
        config.IncludeDetailInExceptions = _redisSettings.IncludeDetailInExceptions;
        config.CheckCertificateRevocation = _redisSettings.CheckCertificateRevocation;
        if (_redisSettings.DefaultDatabaseIndex > 0) config.DefaultDatabase = _redisSettings.DefaultDatabaseIndex;

        config.Ssl = _redisSettings.SslSettings?.UseSsl ?? false;
        config.SslHost = _redisSettings.SslSettings.Host ?? null;
        config.SslProtocols = _redisSettings.SslSettings.Protocol;

        _redisServer = ConnectionMultiplexer.Connect(config);
        _redisDb = _redisServer.GetDatabase();
    }

    public RedisCacheProvider(ConnectionMultiplexer redisClient, IDatabase database = null)
    {
        _redisServer = redisClient;
        if (database is null)
            _redisDb = _redisServer.GetDatabase();
        else
            _redisDb = database;
    }

    public RedisCacheProvider(ConfigurationOptions configurationOptions, IDatabase database = null)
    {
        _redisServer = ConnectionMultiplexer.Connect(configurationOptions);
        if (database is null)
            _redisDb = _redisServer.GetDatabase();
        else
            _redisDb = database;
    }


    public IServer GetServer(string host = null, int port = 0)
        => _redisServer.GetServer(
            host: host.IsNullOrWhiteSpace() ? _redisSettings.Server1 : host,
            port: port > 0 ? _redisSettings.Port1 : port);

    public IEnumerable<string> GetAllKey(RedisValue[] command, CommandFlags flags = CommandFlags.None)
    {
        var server = _redisServer.GetServer(_redisSettings.Server1, _redisSettings.Port1);
        var keys = server.CommandGetKeys(
            command: command,
            flags: flags);

        foreach (var key in keys)
            yield return key.ToString();
    }

    public IEnumerable<string> GetAllKey(PagingParameter pagingParameter = null, string pattern = null, CommandFlags flags = CommandFlags.None)
    {
        var server = _redisServer.GetServer(_redisSettings.Server1, _redisSettings.Port1);
        var keys = server.Keys(
            database: _redisSettings.DefaultDatabaseIndex > 0 ? _redisSettings.DefaultDatabaseIndex : 0,
            pattern: pattern.IsNullOrWhiteSpace() ? default : pattern,
            pageSize: pagingParameter is null ? 250 : (pagingParameter.PageSize > 250 ? 250 : pagingParameter.PageSize),
            pageOffset: pagingParameter is null ? 0 : ((pagingParameter.PageNumber - 1) * pagingParameter.PageSize),
            flags: flags);

        foreach (var key in keys)
            yield return key.ToString();
    }


    public bool Set(string key, string value, TimeSpan? expiry = null, bool keepTTL = false)
        => _redisDb.StringSet(key, value, expiry, keepTTL);

    public bool Set(string key, object value, TimeSpan? expiry = null, bool keepTTl = false)
       => _redisDb.StringSet(key, value.SerializeToJson(), expiry, keepTTl);

    public bool Set(KeyValuePair<string, string>[] values)
    {
        var list = new KeyValuePair<RedisKey, RedisValue>[values.Length];
        values.CopyTo(list, 0);
        return _redisDb.StringSet(list);
    }

    public bool Set(KeyValuePair<string, object>[] values, CommandFlags flags = CommandFlags.None)
    {
        var list = values.Select(x => new 
            KeyValuePair<RedisKey, RedisValue>(x.Key, x.Value.SerializeToJson()))
            .ToArray();
        return _redisDb.StringSet(list);
    }

    public async Task<bool> SetAsync(string key, string value, TimeSpan? expiry = null, bool keepTTL = false)
        => await _redisDb.StringSetAsync(key, value, expiry, keepTTL);

    public async Task<bool> SetAsync(string key, object value, TimeSpan? expiry = null, bool keepTtl = false)
        => await _redisDb.StringSetAsync(key, value.SerializeToJson(), expiry, keepTtl);

    public async Task<bool> SetAsync(KeyValuePair<string, string>[] values)
    {
        var list = new KeyValuePair<RedisKey, RedisValue>[values.Length];
        values.CopyTo(list, 0);
        return await _redisDb.StringSetAsync(list);
    }

    public async Task<bool> SetAsync(KeyValuePair<string, object>[] values)
    {
        var list = values.Select(x => new 
            KeyValuePair<RedisKey, RedisValue>(x.Key, x.Value.SerializeToJson()))
            .ToArray();
        return await _redisDb.StringSetAsync(list);
    }


    public T Get<T>(string key) where T : class
    {
        var value = _redisDb.StringGet(key);
        if (value.IsNullOrEmpty)
            return default;

        return value.ToString().DeSerializeJson<T>();
    }

    public async Task<T> GetAsync<T>(string key)
    {
        var value = await _redisDb.StringGetAsync(key);
        if (value.IsNullOrEmpty)
            return default;

        return value.ToString().DeSerializeJson<T>();
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