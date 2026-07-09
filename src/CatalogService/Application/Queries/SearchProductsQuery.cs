namespace CatalogService.Application.Queries
{
    using Dto;
    using MediatR;

    public record SearchProductsQuery(string searchText) : IRequest<List<ProductDto>>;
}