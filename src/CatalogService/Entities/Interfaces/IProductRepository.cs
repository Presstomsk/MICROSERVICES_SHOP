
namespace CatalogService.Entities.Interfaces
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllProductsAsync();

        Task<Product?> GetProductAsync(int id);

        Task<List<Product>> GetProductsByCategoryAsync(int categoryId);

        Task<List<Product>> SearchProductsAsync(string searchText);
    }
}