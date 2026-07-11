namespace CartService.Application.Interfaces
{
    using CartService.Application.Dto;

    public interface ICatalogClient
    {
        IAsyncEnumerable<ProductDto> GetProductsAsync(int[] productIds);
    }
}