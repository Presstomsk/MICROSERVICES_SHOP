namespace CatalogService.Application.Handlers
{
    using CatalogService.Application.Dto;
    using CatalogService.Application.Queries;
    using CatalogService.Entities;
    using CatalogService.Entities.Interfaces;
    using MediatR;

    public class GetProductsByCategoryQueryHandler(IProductRepository productRepository) : IRequestHandler<GetProductsByCategoryQuery, List<ProductDto>>
    {
        public async Task<List<ProductDto>> Handle(GetProductsByCategoryQuery request, CancellationToken cancellationToken)
        {
            List<Product> products = await productRepository.GetProductsByCategoryAsync(request.categoryId);
                        
            return [.. products.Select(product => new ProductDto
            {
                Id = product.Id,
                Title = product.Title,
                Description = product.Description,
                Image = product.Image,
                Category = product.Category is not null 
                ? new CategoryDto {
                    Id = product.Category.Id,
                    Name = product.Category.Name,
                    Url = product.Category.Url,
                    Icon = product.Category.Icon }                 
                : null,
                DateCreated = product.DateCreated,
                DateUpdated = product.DateUpdated,
                Views = product.Views,
                Price = product.Price,
                OriginalPrice = product.OriginalPrice
            })];
        }
    }
}