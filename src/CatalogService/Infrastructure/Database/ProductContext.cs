namespace CatalogService.Infrastructure.Database
{
    using CatalogService.Entities;
    using Microsoft.EntityFrameworkCore;

    public class ProductContext(DbContextOptions<ProductContext> options) : DbContext(options)
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }            
    }
}