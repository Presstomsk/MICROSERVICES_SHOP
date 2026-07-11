namespace CatalogService.Application.Queries
{
    using Dto;
    using MediatR;

    public record GetProductsQuery(int[] ids) : IRequest<List<ProductDto>>;
}