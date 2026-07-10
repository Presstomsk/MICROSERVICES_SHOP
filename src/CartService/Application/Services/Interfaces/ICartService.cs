namespace CartService.Application.Services.Interfaces
{
    using global::CartService.Entities;
    using global::CartService.Entities.Interfaces;

    public interface ICartService
    {
        Task<ICart> AddItemToCartAsync(int userId, CartItem cartItem);

        Task<ICart> GetCartAsync(int userId);

        Task<ICart> RemoveItemFromCartAsync(int userId, int productId);

        Task<ICart> UpdateQuantityAsync(int userId, int productId, int quantity);

        Task ClearCartAsync(int userId);        
    }
}
