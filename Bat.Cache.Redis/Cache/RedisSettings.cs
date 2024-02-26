using System.Security.Authentication;

namespace Bat.Cache.Redis;

public class RedisSettings
{
    public string Server1 { get; set; }
    public int Port1 { get; set; }

    public string Server2 { get; set; }
    public int Port2 { get; set; }

    public string Server3 { get; set; }
    public int Port3 { get; set; }

    public string Username { get; set; } = null; //User for the redis server (for use with ACLs on redis 6 and above)

    public string Password { get; set; } = null; //Password for the redis server

    public string ClientName { get; set; } = null; //Identification for the connection within redis

    public int SyncTimeout { get; set; } = 5000; //Time (ms) to allow for synchronous operations

    public int ConnectRetry { get; set; } = 3; //The number of times to repeat connect attempts during initial Connect

    public int ConnectTimeout { get; set; } = 3000; //Timeout (ms) for connect operations

    public int DefaultDatabaseIndex { get; set; } = 0; //Default database index, from 0 to databases - 1

    public bool AllowAdminCommand { get; set; } = false; //Enables a range of commands that are considered risky

    public bool IncludeDetailInExceptions { get; set; } = false;

    public bool CheckCertificateRevocation { get; set; } = false; //A Boolean value that specifies whether the certificate revocation list is checked during authentication. 

    public RedisSslSettings SslSettings { get; set; }
}

public class RedisSslSettings
{
    public bool UseSsl { get; set; } = false; //Specifies that SSL encryption should be used
    public string Host { get; set; } = null; //Enforces a particular SSL host identity on the server’s certificate
    public SslProtocols Protocol { get; set; } = SslProtocols.None; //Ssl/Tls versions supported when using an encrypted connection. Use ‘|’ to provide multiple values.
}