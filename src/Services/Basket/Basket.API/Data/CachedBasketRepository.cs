using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;

namespace Basket.API.Data;

public class CachedBasketRepository
    (IBasketRepository repository, IDistributedCache cache)
    : IBasketRepository
{
    public async Task<ShoppingCart> GetBasket(string username, CancellationToken cancellationToken = default)
    {
        var cachedBacked = await cache.GetStringAsync(username, cancellationToken);
        if (!string.IsNullOrEmpty(cachedBacked))
        {
            return JsonSerializer.Deserialize<ShoppingCart>(cachedBacked)!;
        }

        var basket = await repository.GetBasket(username, cancellationToken);
        await cache.SetStringAsync(username, JsonSerializer.Serialize(basket), cancellationToken);
        return basket;
    }

    public async Task<ShoppingCart> StoreBasket(ShoppingCart basket, CancellationToken cancellationToken = default)
    {
        await repository.StoreBasket(basket, cancellationToken);
        await cache.SetStringAsync(basket.Username, JsonSerializer.Serialize(basket), cancellationToken);
        return basket;
    }

    public async Task<bool> DeleteBasket(string username, CancellationToken cancellationToken = default)
    {
        await repository.DeleteBasket(username, cancellationToken);
        await cache.RemoveAsync(username, cancellationToken);
        return true;
    }
}