namespace CartService.Entities.Interfaces
{
    public interface ICartRepository
    {
        Task<Cart> AddToCartAsync(CartItem cartItem);

        Task<Cart> GetCartAsync();

        Task<Cart> DeleteFromCartAsync(CartItem item);
    }
}