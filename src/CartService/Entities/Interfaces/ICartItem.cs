namespace CartService.Entities.Interfaces
{
    public interface ICartItem
    {        
        int ProductId { get; set; }

        string ProductName { get; set; }

        int Quantity { get; set; }

        decimal Price { get; set; }
    }
}