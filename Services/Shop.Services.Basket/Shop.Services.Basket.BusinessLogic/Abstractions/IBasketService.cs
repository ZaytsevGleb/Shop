using Shop.Services.Catalog.BusinessLogic.Models;

namespace Shop.Services.Catalog.BusinessLogic.Abstractions;

public interface IBasketService
{
    Task<BasketCheckoutModel> Get(string userName, CancellationToken ct);
    Task<BasketCheckoutModel> Update(ShoppingCartModel model, CancellationToken ct);
    Task Delete(string userName, CancellationToken ct);
}