using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using Shop.Services.Catalog.DataAccess.Abstractions;
using Shop.Services.Catalog.DataAccess.Entities;

namespace Shop.Services.Catalog.DataAccess.Repositories;

public class CacheRepository(IDistributedCache cache) : ICacheRepository
{
    private readonly IDistributedCache _cache = cache ?? throw new ArgumentNullException(nameof(cache));

    public async Task<ShoppingCart?> Get(string userName, CancellationToken ct)
    {
        var result = await _cache.GetStringAsync(userName, ct);

        return string.IsNullOrEmpty(result)
            ? null
            : JsonConvert.DeserializeObject<ShoppingCart>(result);
    }

    public async Task<ShoppingCart?> Update(ShoppingCart cart, CancellationToken ct)
    {
        await _cache.SetStringAsync(cart.UserName, JsonConvert.SerializeObject(cart), ct);
        return await Get(cart.UserName, ct) ?? null;
    }

    public async Task Delete(string userName, CancellationToken ct)
    {
        await _cache.RemoveAsync(userName, ct);
    }
}