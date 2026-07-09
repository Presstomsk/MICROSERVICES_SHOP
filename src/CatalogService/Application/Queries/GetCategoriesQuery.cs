namespace CatalogService.Application.Queries
{
    using Dto;
    using MediatR;

    public record GetCategoriesQuery : IRequest<List<CategoryDto>>;
}