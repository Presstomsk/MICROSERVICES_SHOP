namespace CartService.Entities.Interfaces
{
    public interface ICartItem
    {        
        int ProductId { get; set; }

        int Quantity { get; set; }
    }
}