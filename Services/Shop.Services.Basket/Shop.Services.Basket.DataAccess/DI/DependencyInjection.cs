using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shop.Services.Catalog.DataAccess.Abstractions;
using Shop.Services.Catalog.DataAccess.Constants;
using Shop.Services.Catalog.DataAccess.Repositories;

namespace Shop.Services.Catalog.DataAccess.DI;

public static class DependencyInjection
{
    public static void AddDataAccessDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        // services.AddDbContext<ApplicationDbContext>(opt =>
        // {
        //     opt.UseNpgsql(configuration.GetConnectionString(DataAccessConstants.DbConnection));
        //     opt.EnableSensitiveDataLogging();
        //     opt.AddInterceptors(new EntityDateTrackingInterceptor());
        // });

        services.AddStackExchangeRedisCache(opt =>
        {
            opt.Configuration = configuration.GetConnectionString(DataAccessConstants.RedisConnection);
        });
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<ICacheRepository, CacheRepository>();
    }
}