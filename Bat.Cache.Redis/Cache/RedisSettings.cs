namespace Bat.Cache.Redis;

public class RedisSettings
{
    public string Server1 { get; set; }
    public int Port1 { get; set; }

    public string Server2 { get; set; }
    public int Port2 { get; set; }

    public string Server3 { get; set; }
    public int Port3 { get; set; }

    public string Username { get; set; } = null;

    public string Password { get; set; } = null;

    public string ClientName { get; set; } = null;

    public int SyncTimeout { get; set; } = 5;

    public int ConnectRetry { get; set; } = 3;

    public int ConnectTimeout { get; set; } = 3000;

    public int DefaultDatabaseIndex { get; set; } = 0;

    public bool AllowAdminCommand { get; set; } = false;

    public bool IncludeDetailInExceptions { get; set; } = false;

    public bool CheckCertificateRevocation { get; set; } = false;

    public RedisSslSettings SslSettings { get; set; }
}

public class RedisSslSettings
{
    public bool UseSsl { get; set; } = false;
    public string SslHost { get; set; } = null;
}