namespace CartService.Application.Dto
{
    public class CartDto
    {
        public int UserId { get; set; }

        public List<CartItemDto> CartItems { get; set; } = [];

        public int TotalCartItems { get; set; }

        public decimal TotalCartPrice { get; set; }
        
        public DateTime UpdatedAt { get; set; } 
    }
}