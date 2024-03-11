using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Shop.Services.Catalog.DataAccess.Constants;
using Shop.Services.Catalog.DataAccess.Context;

namespace Shop.Services.Catalog.BusinessLogic.Common;

public class DesignTimeApplicationDbContextFactory
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../News.Services.PostService.WebAPI/"))
            .AddJsonFile("appsettings.json")
            .Build();

        var options = new DbContextOptionsBuilder<ApplicationDbContext>();
        options.UseNpgsql(config.GetConnectionString(DataAccessConstants.DbConnection));
        options.EnableSensitiveDataLogging();

        return new ApplicationDbContext(options.Options);
    }
}