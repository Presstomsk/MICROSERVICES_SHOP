namespace CatalogService.Entities.Interfaces
{
    using System.ComponentModel.DataAnnotations.Schema;

    public interface IProduct
    {
        int Id { get; set; }
        string? Title { get; set; }
        string? Description { get; set; }
        string Image { get; set; }        
        Category? Category { get; set; }
        int CategoryId { get; set; }        
        DateTime DateCreated { get; set; }
        DateTime? DateUpdated { get; set; }
        int Views { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal OriginalPrice { get; set; }
    }
}