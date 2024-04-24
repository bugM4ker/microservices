using Basket_Api.Models;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Basket_Api.Repository
{
    public class BasketRepositoryCachingDecorator : IBasketRepository
    {
        private readonly string CACHE_KEY_PREFIX = "basket_";

        private readonly IBasketRepository repository;
        private readonly IDistributedCache cache;
        private readonly ILogger<BasketRepositoryCachingDecorator> logger;

        public BasketRepositoryCachingDecorator(IBasketRepository _repository, IDistributedCache _cache, ILogger<BasketRepositoryCachingDecorator> _logger)
        {
            repository = _repository;
            cache = _cache;
            logger = _logger;
        }

        public async Task<bool> DeleteBasket(string userName, CancellationToken cancellationToken)
        {
            var result = await repository.DeleteBasket(userName, cancellationToken);
            if (result)
            {
                var cacheKey = $"{CACHE_KEY_PREFIX}{userName}";
                await cache.RemoveAsync(cacheKey, cancellationToken);
            }
            return result;
        }

        public async Task<ShoppingCart> GetBasket(string userName, CancellationToken cancellationToken)
        {
            var cacheKey = $"{CACHE_KEY_PREFIX}{userName}";
            var basketSerialized = await cache.GetStringAsync(cacheKey, cancellationToken);
            if (!string.IsNullOrEmpty(basketSerialized))
            {
                return JsonSerializer.Deserialize<ShoppingCart>(basketSerialized);
            }

            var basket = await repository.GetBasket(userName, cancellationToken);
            if (basket != null)
            {
                await cache.SetStringAsync(cacheKey, JsonSerializer.Serialize(basket), new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30) // Cache for 30 minutes
                }, cancellationToken);
            }
            return basket;
        }

        public async Task<ShoppingCart> StoreBasket(ShoppingCart cart, CancellationToken cancellationToken)
        {
            try
            {
                logger.LogWarning("Store Basket...");
                var cartStored = await repository.StoreBasket(cart, cancellationToken);
                var cacheKey = $"{CACHE_KEY_PREFIX}{cart.UserName}";
                await cache.SetStringAsync(cacheKey, JsonSerializer.Serialize(cart), new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30)
                }, cancellationToken);

                return cartStored;
            }
            catch(Exception ex)
            {
                logger.LogError(ex, "Error store basket");
                throw;
            }
        }
    }
}
