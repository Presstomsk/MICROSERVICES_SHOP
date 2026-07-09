namespace CartService.Entities
{
    using System.ComponentModel.DataAnnotations;
    using CartService.Entities.Interfaces;

    public class CartItem : ICartItem
    {
        [Key]
        public int ProductId { get; set; }
        
        public int Quantity { get; set; }
    }
}