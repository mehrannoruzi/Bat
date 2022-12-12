For use Bat.EntityFrameworkCore.Tools just do it :

1- Install Bat.EntityFrameworkCore.Tools on your project

2- Register IEFBulkGenericRepo<TEntity> to service container
for example :


    builder.Services.AddTransient<IEFBulkGenericRepo<User>, EFBulkGenericRepo<User>>();
    builder.Services.AddTransient<IEFBulkGenericRepo<Address>, EFBulkGenericRepo<Address>>();

    builder.Services.AddScoped<AppBulkUnitOfWork>();


3- Use it in bussiness logic
for example :

    // Create DbContext
    public class AppDbContext : BatDbContext
    {
        public AppDbContext() { }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(AppUnitOfWork).Assembly);

            base.OnModelCreating(builder);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Address> Addresses { get; set; }

    }


    // Create UnitOfWork
    public class AppBulkUnitOfWork
    {
        private readonly AppDbContext _appDbContext;
        private readonly IServiceProvider _serviceProvider;

        public AppBulkUnitOfWork(AppDbContext appDbContext, IServiceProvider serviceProvider)
        {
            _appDbContext = appDbContext;
            _serviceProvider = serviceProvider;
        }


        public EFBulkGenericRepo<User> UserBulkRepo => (EFBulkGenericRepo<User>)_serviceProvider.GetRequiredService<IEFBulkGenericRepo<User>>();
        public EFBulkGenericRepo<Address> AddressBulkRepo => (EFBulkGenericRepo<Address>)_serviceProvider.GetRequiredService<IEFBulkGenericRepo<Address>>();
        

        public EFBulkGenericRepo<T> GetBulkRepository<T>() where T : class, IBaseEntity
            => EFBulkGenericRepo<T>)_serviceProvider.GetRequiredService<IEFBulkGenericRepo<T>>();


        public void Dispose()
        {
            _appDbContext.Dispose();

            GC.SuppressFinalize(this);
        }
    }


    // Use power Off EFBulkGenericRepo
    public class UserService : IUserService
    {
        private readonly AppBulkUnitOfWork _appBulkUow;

        public UserService(AppBulkUnitOfWork appBulkUow)
        {
            _appBulkUow = appBulkUow;
        }

        public async Task ExecuteSample()
        {
            var users = new List<User>
            {
                new User { UserId = 1, FirstName = "Mehdi", LastName = "Norouzi" },
                new User { UserId = 2, FirstName = "Mehran", LastName = "Norouzi" },
                new User { UserId = 3, FirstName = "Kamran", LastName = "Norouzi" }
            };

            await _appBulkUow.UserBulkRepo.BulkInsertAsync(users);

            await _appBulkUow.GetBulkRepository<User>().BulkDeleteAsync(users);
        }
    }
