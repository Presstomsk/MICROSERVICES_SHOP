namespace CartService.Entities.Interfaces
{
    public interface ICartRepository
    {
        Task AddItemToCartAsync(int userId, ICart cart);

        Task<string?> GetCartAsync(int userId);

        Task RemoveCartAsync(int userId);
    }
}