namespace CartService.Entities
{
    using System.ComponentModel.DataAnnotations;
    using Interfaces;

    public class Cart : ICart
    {
        [Key]
        public int UserId { get; set; }

        public List<CartItem> CartItems { get; set; } = []; 
    }
}