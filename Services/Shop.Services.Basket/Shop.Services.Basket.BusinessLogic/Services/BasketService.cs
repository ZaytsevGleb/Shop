using AutoMapper;
using Shop.Services.Catalog.BusinessLogic.Abstractions;
using Shop.Services.Catalog.BusinessLogic.Models;
using Shop.Services.Catalog.DataAccess.Abstractions;
using Shop.Services.Catalog.DataAccess.Entities;

namespace Shop.Services.Catalog.BusinessLogic.Services;

internal class BasketService(
    ICacheRepository cacheRepository,
    IMapper mapper)
    : IBasketService
{
    public async Task<BasketCheckoutModel> Get(string userName, CancellationToken ct)
        => mapper.Map<BasketCheckoutModel>(await cacheRepository.Get(userName, ct));

    public async Task<BasketCheckoutModel> Update(ShoppingCartModel cart, CancellationToken ct)
        => mapper.Map<BasketCheckoutModel>(await cacheRepository.Update(mapper.Map<ShoppingCart>(cart), ct));

    public async Task Delete(string userName, CancellationToken ct)
        => await cacheRepository.Delete(userName, ct);
}