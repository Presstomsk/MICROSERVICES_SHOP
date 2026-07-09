namespace CatalogService.Application.Queries
{
    using Dto;
    using MediatR;

    public record GetProductQuery(int id) : IRequest<ProductDto?>;
}