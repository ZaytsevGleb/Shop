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
        services.AddStackExchangeRedisCache(opt =>
        {
            opt.Configuration = configuration[DataAccessConstants.RedisConnection]
                                ?? throw new ArgumentNullException("Configuration IS NULL!!!!!!!!!!!!!!!!!!");
        });

        services.AddScoped<ICacheRepository, CacheRepository>();
    }
}