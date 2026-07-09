namespace CatalogService.Application.Handlers
{
    using CatalogService.Application.Dto;
    using CatalogService.Application.Queries;
    using CatalogService.Entities;
    using CatalogService.Entities.Interfaces;
    using MediatR;

    public class GetProductQueryHandler(IProductRepository productRepository) : IRequestHandler<GetProductQuery, ProductDto?>
    {
        public async Task<ProductDto?> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            Product? product = await productRepository.GetProductAsync(request.id);
            return product is not null 
            ? new ProductDto
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
            }
            : null;
        }
    }
}