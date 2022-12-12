For use Bat.EntityFrameworkCore just do it :

1- Install Bat.EntityFrameworkCore on your project

2- Register IEFGenericRepo<TEntity> to service container
for example :


    builder.Services.AddTransient<IEFGenericRepo<User>, EFGenericRepo<User>>();
    builder.Services.AddTransient<IEFGenericRepo<Address>, EFGenericRepo<Address>>();

    builder.Services.AddScoped<AppUnitOfWork>();


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
    public class AppUnitOfWork : IBatUnitOfWork
    {
        private readonly AppDbContext _appDbContext;
        private readonly IServiceProvider _serviceProvider;

        public AppUnitOfWork(AppDbContext appDbContext, IServiceProvider serviceProvider)
        {
            _appDbContext = appDbContext;
            _serviceProvider = serviceProvider;
        }

        public EFGenericRepo<User> UserRepo => (EFGenericRepo<User>)_serviceProvider.GetRequiredService<IEFGenericRepo<User>>();
        public EFGenericRepo<Address> AddressRepo => (EFGenericRepo<Address>)_serviceProvider.GetRequiredService<IEFGenericRepo<Address>>();

        
        public DatabaseFacade Database { get => _appDbContext.Database; }
        public ChangeTracker ChangeTracker { get => _appDbContext.ChangeTracker; }


        public EFGenericRepo<T> GetRepository<T>() where T : class, IBaseEntity
            => EFGenericRepo<T>)_serviceProvider.GetRequiredService<IEFGenericRepo<T>>();

        public async Task<IEnumerable<T>> ExecuteProcedure<T>(string sqlQuery, params object[] parameter) where T : class
            => await _appDbContext.ExecuteProcedure<T>(sqlQuery, parameter);


        public Task<SaveChangeResult> BatSaveChangesAsync(CancellationToken cancellationToken = default)
            => _appDbContext.BatSaveChangesAsync(cancellationToken);


        public void Dispose()
        {
            _appDbContext.Dispose();

            GC.SuppressFinalize(this);
        }
    }


    // Use power Off EFGenericRepo
    public class UserService : IUserService
    {
        private readonly AppUnitOfWork _appUow;

        public UserService(AppUnitOfWork appUnitOfWork)
        {
            _appUow = appUnitOfWork;
        }

        public async Task ExecuteSample()
        {
            var x0 = await _appUow.GetRepository<User>().AnyAsync();

            var x1 = await _appUow.AddressRepo.CountAsync();
            var x2 = await _appUow.UserRepo
                .Include(x => x.Families)
                .Select(x => new GetUserDto { UserId = x.UserId, Name = x.Name, Families = x.Families })
                .FirstOrDefaultAsync(x => x.UserId > 1);

            var xxx1 = await _appUow.UserRepo
                .Where(x => x.UserId > 1).ToListAsync();

            var xxx2 = await _appUow.UserRepo
                .Include(x => x.Families).ToListAsync();

            var xxx3 = await _appUow.UserRepo
                .Where(x => x.UserId > 1)
                .Include(x => x.Families.Where(x => x.FamilyId > 0))
                .ThenInclude(x => x.Addresses)
                .OrderByDescending(x => x.UserId)
                .ToListAsync();

            var xxx4 = await _appUow.UserRepo
                .Select(x => new { x.UserId, x.Name }).ToListAsync();

            var xxx5 = await _appUow.UserRepo
                .Include(x => x.Families)
                .Select(x => new { x.UserId, x.Name, x.Families }).ToListAsync();

            var xxx6 = await _appUow.UserRepo
                .Include(x => x.Families)
                .Where(x => x.UserId > 1)
                .Select(x => new { x.UserId, x.Name, x.Families }).ToListAsync();

            var xxx7 = await _appUow.UserRepo
                .Include(x => x.Families)
                .ThenInclude(x => x.Addresses)
                .OrderByDescending(x => x.UserId)
                .Select(x => new { x.UserId, x.Name, x.Families })
                .FirstOrDefaultAsync();

            var xxx8 = await _appUow.UserRepo.AsNoTracking().FirstOrDefaultAsync(x => x.UserId > 1);

            var xxx9 = await _appUow.UserRepo.OrderBy(x => x.UserId).FirstOrDefaultAsync(x => x.UserId > 1);

            var xxx10 = await _appUow.UserRepo.Where(x => x.UserId > 1).ToPagingListDetailsAsync(pagingParameter);
            var xxx11 = await _appUow.UserRepo.Include(x => x.Families).Where(x => x.UserId > 1).ToPagingListDetailsAsync(pagingParameter);
            var xxx12 = await _appUow.UserRepo.Select(x => new { x.UserId, x.Name }).OrderByDescending(x => x.UserId).ToPagingListDetailsAsync(pagingParameter);

            var xxx13 = await _appUow.UserRepo.AnyAsync(x => x.UserId > 1);
            var xxx14 = await _appUow.UserRepo.Include(x => x.Families).AnyAsync();
            var xxx15 = await _appUow.UserRepo.Include(x => x.Families).AnyAsync(x => x.UserId > 1);

            var xxx16 = await _appUow.UserRepo.CountAsync();
            var xxx17 = await _appUow.UserRepo.Include(x => x.Families).CountAsync();
            var xxx18 = await _appUow.UserRepo.Include(x => x.Families).CountAsync(x => x.UserId > 1);



            var user0 = await _appUow.UserRepo.FirstOrDefaultAsync(
                new QueryFilterWithSelector<User, int>
                {
                    Conditions = x => x.UserId > 0,
                    ThenIncludeProperties = a => a.Include(a => a.Families).ThenInclude(a => a.Addresses),
                    Selector = x => x.UserId
                });

            var user00 = await _appUow.UserRepo.FirstOrDefaultAsync(
                new QueryFilterWithSelector<User, string>
                {
                    Conditions = x => x.UserId > 0,
                    ThenIncludeProperties = a => a.Include(a => a.Families).ThenInclude(a => a.Addresses),
                    Selector = x => x.Name
                });

            var user001 = await _appUow.UserRepo.FirstOrDefaultAsync(
                new QueryFilter<User>
                {
                    Conditions = x => x.UserId > 1,
                    ThenIncludeProperties = a => a.Include(a => a.Families).ThenInclude(a => a.Addresses),
                });

            var user002 = await _appUow.UserRepo.GetPagingAsync(
                new QueryFilter<User>
                {
                    Conditions = x => x.UserId > 0,
                    PagingParameter = new PagingParameter(2, 2),
                    ThenIncludeProperties = a => a.Include(a => a.Families).ThenInclude(a => a.Addresses)
                });


            var user003 = await _appUow.UserRepo.GetAsync(
                new QueryFilter<User>
                {
                    Conditions = x => x.UserId > 0,
                    ThenIncludeProperties = a => a.Include(a => a.Families).ThenInclude(a => a.Addresses)
                });


            var query = "EXEC [Auth].[GetUserMenu] @UserId, @Type";
            var userMenu = await _appUow.ExecuteProcedure<MenuModel>(query, 
                new SqlParameter("@UserId", userId), 
                new SqlParameter("@Type", RoleType.Online));

        }
    }
