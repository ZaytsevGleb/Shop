using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Shop.Services.Catalog.DataAccess.Entities;

namespace Shop.Services.Catalog.DataAccess.Common;

public sealed class EntityDateTrackingInterceptor : SaveChangesInterceptor
{
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        var entries = eventData.Context!.ChangeTracker
            .Entries()
            .Where(x => x.Entity is BaseEntity)
            .ToList();

        foreach (var entry in entries)
        {
            var now = DateTime.UtcNow;
            var auditable = entry.Entity as BaseEntity;

            if (entry.State == EntityState.Modified)
            {
                auditable!.UpdatedDate = now;
            }
            else
            {
                auditable!.CreatedDate = now;
                auditable!.UpdatedDate = now;
            }
        }

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}