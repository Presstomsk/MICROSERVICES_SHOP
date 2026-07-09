namespace CatalogService.Infrastructure.Database
{
    using CatalogService.Entities;
    using CatalogService.Entities.Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class ProductRepository(ProductContext context) : IProductRepository
    {
        public async Task<List<Product>> GetAllProductsAsync()
        {
            return await context.Products.AsNoTracking().Include(p => p.Category).ToListAsync();
        }

        public async Task<Product?> GetProductAsync(int id)
        {
            return await context.Products.AsNoTracking().Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<Product>> GetProductsByCategoryAsync(int categoryId)
        {
            return await context.Products.AsNoTracking().Where(p => p.CategoryId == categoryId).Include(p => p.Category).ToListAsync();
        }

        public async Task<List<Product>> SearchProductsAsync(string searchText)
        {
            return await context.Products.AsNoTracking().Where(product => 
                (product.Title != null && product.Title.ToLower().Contains(searchText.ToLower())) 
                || (product.Description != null && product.Description.ToLower().Contains(searchText.ToLower())))
                .ToListAsync(); 
        }
    }
}