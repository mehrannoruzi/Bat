For use Bat.Cache just do it :

1- Install Bat.Cache on your project

2- Register MemoryCacheProvider to service container
for example :


    builder.Services.AddMemoryCache();
    builder.Services.AddSingleton<IMemoryCacheProvider, MemoryCacheProvider>();


3- Use it in bussiness logic
for example :


    public class BaseService : IBaseService
    {
        private readonly IMemoryCacheProvider _cacheProvider;

        public BaseService(IMemoryCacheProvider cacheProvider)
        {
            _cacheProvider = cacheProvider;
        }


        public List<string> GetRegions()
        {
            var data = new List<string> { "Tehran", "Esfahan", "Shiraz" };
            _cacheProvider.Add("Regions_Key", data, DateTime.Now.AddHours(24));

            return (List<Region>)_cacheProvider.Get("Regions_Key");
        }
    }
