namespace CatalogService.Application.Queries
{
    using Dto;
    using MediatR;

    public record GetProductsByCategoryQuery(int categoryId) : IRequest<List<ProductDto>>;
}