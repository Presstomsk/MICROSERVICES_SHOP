namespace CartService.Entities
{    
    using Interfaces;

    public class Cart : ICart
    {       
        public int UserId { get; set; }

        public List<CartItem> CartItems { get; set; } = [];

        public int TotalCartItems => CartItems.Sum(ci => ci.Quantity);

        public decimal TotalCartPrice => CartItems.Sum(i => i.Price * i.Quantity);

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow; 
    }
}