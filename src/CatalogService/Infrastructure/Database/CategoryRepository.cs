namespace CatalogService.Infrastructure.Database
{
    using CatalogService.Entities;
    using CatalogService.Entities.Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class CategoryRepository(ProductContext context) : ICategoryRepository
    {
        public async Task<List<Category>> GetCategoriesAsync()
        {
            return await context.Categories.AsNoTracking().ToListAsync();
        }        
    }
}