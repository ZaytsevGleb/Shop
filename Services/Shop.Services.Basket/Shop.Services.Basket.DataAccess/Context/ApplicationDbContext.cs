using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Shop.Services.Catalog.DataAccess.Entities;

namespace Shop.Services.Catalog.DataAccess.Context;

public sealed class ApplicationDbContext: IdentityDbContext<User, Role, Guid>
{
    public DbSet<User> Users { get; set; }
    
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
        if (Database.IsRelational())
        {
            Console.WriteLine("Before 'Migrate'");

            Database.Migrate();

            Console.WriteLine("After 'Migrate'");
        }
    }
    
    // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //     => optionsBuilder.AddInterceptors(new EntityDateTrackingInterceptor());
}