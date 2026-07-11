namespace CartService.Application.Services
{
    using System.Text.Json;
    using global::CartService.Application.Dto;
    using global::CartService.Application.Interfaces;
    using global::CartService.Entities;
    using global::CartService.Entities.Interfaces;
    using Interfaces;

    public class CartService(ICartRepository cartRepository,
        ICatalogClient catalogClient,
        ILogger<CartService> logger) : ICartService
    {
        public async Task<ICart> GetCartAsync(int userId)
        {
            string? cartJson = await cartRepository.GetCartAsync(userId);

            Cart cart = !string.IsNullOrEmpty(cartJson) 
                            ? JsonSerializer.Deserialize<Cart>(cartJson)!
                            : new Cart { UserId = userId, CartItems = [] };
            int[] productIds = [.. cart.CartItems.Select(ci => ci.ProductId)];

            await foreach(ProductDto product in catalogClient.GetProductsAsync(productIds))
            {
                CartItem? cartItem = cart.CartItems.Find(ci => ci.ProductId == product.Id);

                if (cartItem is not null)
                {
                    cartItem.Price = product.Price;
                    cartItem.ProductName = product.Name;
                }
            }

            await cartRepository.AddItemToCartAsync(userId, cart);

            return cart;             
        }

        public async Task<ICart> AddItemToCartAsync(int userId, CartItem cartItem)
        {
            var cart = await GetCartAsync(userId);
            var sameItem = cart.CartItems.Find(ci => ci.ProductId == cartItem.ProductId);

            if (sameItem is not null)
            {
                sameItem.Quantity += cartItem.Quantity;
            }
            else
            {
                cart.CartItems.Add(cartItem);
            }

            cart.UpdatedAt = DateTime.UtcNow;
            await cartRepository.AddItemToCartAsync(userId, cart);
            logger.LogInformation("Товар {ProductId} добавлен в корзину {UserId}", cartItem.ProductId, userId);

            return cart;
        }

        public async Task<ICart> RemoveItemFromCartAsync(int userId, int productId)
        {
            var cart = await GetCartAsync(userId);
            var sameItem = cart.CartItems.Find(ci => ci.ProductId == productId);

            if (sameItem is null)
            {
                logger.LogInformation("Товар {ProductId} отсутствует в корзине {UserId}", productId, userId);

                return cart;
            }

            cart.CartItems.Remove(sameItem);
            cart.UpdatedAt = DateTime.UtcNow;
            await cartRepository.AddItemToCartAsync(userId, cart);
            logger.LogInformation("Товар {ProductId} удален из корзины {UserId}", productId, userId);

            return cart;
        }

        public async Task<ICart> UpdateQuantityAsync(int userId, int productId, int quantity)
        {
            var cart = await GetCartAsync(userId);
            var sameItem = cart.CartItems.Find(ci => ci.ProductId == productId);

            if (sameItem == null)
            {
                logger.LogInformation("Товара {ProductId} не найден в корзине {UserId}", productId, userId);

                return cart;               
            }
            
            if (quantity <= 0)
            {
                cart.CartItems.Remove(sameItem);
            }
            else
            {
                sameItem.Quantity = quantity;
            }
        
            cart.UpdatedAt = DateTime.UtcNow;
            await cartRepository.AddItemToCartAsync(userId, cart);
            logger.LogInformation("Количество товара {ProductId} обновлено в корзине {UserId}", productId, userId);

            return cart;
        }      

        public async Task ClearCartAsync(int userId)
        {
            await cartRepository.RemoveCartAsync(userId);
            logger.LogInformation("Корзина {UserId} очищена", userId);
        }        
    }
}

