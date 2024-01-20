For use Bat.Cache.Redis just do it :

1- Install Bat.Cache.Redis on your project

2- Set RedisSettings on appSettings.json file
for example :


    {
      "Logging": {
        "LogLevel": {
          "Default": "Information",
          "Microsoft": "Warning",
          "Microsoft.Hosting.Lifetime": "Information"
        }
      },
      "AllowedHosts": "*",
      "RedisSettings": {

        "Server1": "127.0.0.1",
        "Port1": 6379,
        "Server2": "127.0.0.1",
        "Port2": 6380,
        "Server3": "127.0.0.1",
        "Port3": 6381,

        "Username": "",
        "Password": "Norouzi",
        "ClientName": "Bat.Cache.Redis",
        "SyncTimeout": 5,
        "ConnectRetry": 3,
        "ConnectTimeout": 3000,
        "DefaultDatabaseIndex": 0,
        "AllowAdminCommand": false,
        "IncludeDetailInExceptions": false,
        "CheckCertificateRevocation": false,

        "SslSettings": {

          "UseSsl": false,
          "SslHost": ""
        }

      }
    }


3- Register RedisCacheProvider to service container
for example :


    builder.Services.AddDistributedMemoryCache();
    builder.Services.Configure<RedisSettings>(configuration.GetSection("RedisSettings"));
    builder.Services.AddSingleton<IRedisCacheProvider, RedisCacheProvider>();


4- Use it in bussiness logic
for example :


    public class BaseService : IBaseService
    {
        private readonly IRedisCacheProvider _cacheProvider;

        public BaseService(IRedisCacheProvider cacheProvider)
        {
            _cacheProvider = cacheProvider;
        }


        public List<string> GetRegions()
        {
            var data = new List<string> { "Tehran", "Esfahan", "Shiraz" };
            _cacheProvider.Set("Regions_Key", data.SerializeToJson(), TimeSpan.FromHours(24));

            return _cacheProvider.Get<List<Region>>("Regions_Key");
        }
    }
