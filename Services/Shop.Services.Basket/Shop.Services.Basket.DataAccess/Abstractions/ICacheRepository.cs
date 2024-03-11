using Shop.Services.Catalog.DataAccess.Entities;

namespace Shop.Services.Catalog.DataAccess.Abstractions;

public interface ICacheRepository
{

    Task<ShoppingCart?> Get(string userName, CancellationToken ct);
    Task<ShoppingCart?> Update(ShoppingCart cart, CancellationToken ct);
    Task Delete(string userName, CancellationToken ct);
}