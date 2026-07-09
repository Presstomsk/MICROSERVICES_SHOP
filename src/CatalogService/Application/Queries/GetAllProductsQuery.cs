namespace CatalogService.Application.Queries
{
    using Dto;
    using MediatR;

    public record GetAllProductsQuery : IRequest<List<ProductDto>>;
}