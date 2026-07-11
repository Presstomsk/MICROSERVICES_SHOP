namespace CartService.Infrastructure.Repositories
{
    using System.Text.Json;
    using CartService.Entities.Interfaces;
    using Microsoft.Extensions.Caching.Distributed;

    public class CartRepository(IDistributedCache cache) : ICartRepository
    {
        private static readonly DistributedCacheEntryOptions _cartOptions = new()
            { 
                SlidingExpiration = TimeSpan.FromDays(7)
            };

        public async Task AddItemToCartAsync(int userId, ICart cart)
        {
            var key = $"user:{userId}";
            await cache.SetStringAsync(key, JsonSerializer.Serialize(cart), _cartOptions);
        }

        public async Task<string?> GetCartAsync(int userId)
        {
            var key = $"user:{userId}";
            return await cache.GetStringAsync(key);
        }

        public async Task RemoveCartAsync(int userId)
        {
            var key = $"user:{userId}";
            await cache.RemoveAsync(key);
        }
    }
}