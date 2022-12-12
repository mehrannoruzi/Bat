For use Bat.Di just do it :

1- Install Bat.Di on your project

2- Create extension methods for register services
This package support three IOC container :
- Microsoft
- Autofac
- DryIoc


for example :

    public static class MicrosoftContainerExtension
    {
        public static IServiceCollection AddTransient(this IServiceCollection services, IConfiguration _configuration)
        {
            services.AddBatDynamicTransient(typeof(IUserService).Assembly);

            services.AddTransient<IGenericRepo<Menu>, MenuRepo>();

            return services;
        }

        public static IServiceCollection AddScoped(this IServiceCollection services, IConfiguration _configuration)
        {
            services.AddBatDynamicScoped(typeof(IUserService).Assembly);

            return services;
        }

        public static IServiceCollection AddSingleton(this IServiceCollection services, IConfiguration _configuration)
        {
            services.AddBatDynamicSingleton(typeof(IUserService).Assembly);

            services.AddSingleton<IMemoryCacheProvider, MemoryCacheProvider>();

            return services;
        }
    }

3- Call this extensions method in configur service container
for example :

    builder.Services.AddSwaggerGen();
    builder.Host.ConfigureContainer<IContainer>(container => ConfigureContainer(container));

    var app = builder.Build();

    void ConfigureContainer(IContainer container)
    {
        container.RegisterTransient(configuration);
        container.RegisterScoped(configuration);
        container.RegisterSingleton(configuration);
    }
