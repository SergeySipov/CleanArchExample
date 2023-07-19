using Application.Interfaces.Repositories;
using Application.Interfaces.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Constants;
using Persistence.DbContexts;
using Persistence.Repositories;

namespace Persistence;

public static class ConfigureServices
{
    public static IServiceCollection AddPersistentServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext(configuration);
        services.AddRepositories();

        return services;
    }

    private static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<UniconDbContext>(options =>
        {
            var connectionString = configuration.GetConnectionString(DbContextConstants.UniconDbConnectionStringName);
            options.UseSqlServer(connectionString,
                builder =>
                {
                    builder.MigrationsAssembly(typeof(UniconDbContext).Assembly.FullName);
                });

            options.UseCamelCaseNamingConvention();
            options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        });

        //services.AddIdentityCore<User>(options =>
        //    {
        //        options.SignIn.RequireConfirmedAccount = securitySettings.RequireConfirmedAccount;
        //        options.SignIn.RequireConfirmedEmail = securitySettings.RequireConfirmedEmail;

        //        options.User.RequireUniqueEmail = securitySettings.RequireUniqueEmail;

        //        options.Password.RequireDigit = securitySettings.RequireDigit;
        //        options.Password.RequiredLength = securitySettings.RequiredLength;
        //        options.Password.RequireNonAlphanumeric = securitySettings.RequireNonAlphanumeric;
        //        options.Password.RequireUppercase = securitySettings.RequireUppercase;
        //        options.Password.RequireLowercase = securitySettings.RequireLowercase;
        //    })
        //    .AddEntityFrameworkStores<FoodCorpDbContext>()
        //    .AddDefaultTokenProviders();

        //services.Configure<PasswordHasherOptions>(option =>
        //{
        //    option.IterationCount = securitySettings.PasswordHashIterationsCount;
        //});
    }

    private static void AddRepositories(this IServiceCollection services)
    {
        services
            .AddScoped<IUnitOfWork, UnitOfWork.UnitOfWork>()
            .AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>))
            .AddScoped(typeof(IPaginatedRepository<>), typeof(PaginatedRepository<>));
    }
}
