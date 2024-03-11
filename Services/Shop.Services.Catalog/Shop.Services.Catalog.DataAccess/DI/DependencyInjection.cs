using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shop.Services.Catalog.DataAccess.Abstractions;
using Shop.Services.Catalog.DataAccess.Common;
using Shop.Services.Catalog.DataAccess.Options;
using Shop.Services.Catalog.DataAccess.Repositories;

namespace Shop.Services.Catalog.DataAccess.DI;

public static class DependencyInjection
{
    public static void AddDataAccessDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions<DatabaseOptions>()
            .Bind(configuration.GetSection(DatabaseOptions.SectionName));
            // .Validate(x => !string.IsNullOrWhiteSpace(x.ConnectionString))
            // .Validate(x => !string.IsNullOrWhiteSpace(x.DatabaseName));

        services.AddScoped(typeof(IMongoRepository<>), typeof(MongoRepository<>));
        services.AddScoped<IMongoDbContext, MongoDbContext>();
    }
}