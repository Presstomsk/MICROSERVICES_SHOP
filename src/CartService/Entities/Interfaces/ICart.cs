namespace CartService.Entities.Interfaces
{
    public interface ICart
    {
        int UserId { get; set; }

        List<CartItem> CartItems { get; set; } 
    }
}