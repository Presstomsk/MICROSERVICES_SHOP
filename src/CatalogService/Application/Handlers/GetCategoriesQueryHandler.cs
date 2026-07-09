namespace CatalogService.Application.Handlers
{
    using CatalogService.Application.Dto;
    using CatalogService.Application.Queries;
    using CatalogService.Entities;
    using CatalogService.Entities.Interfaces;
    using MediatR;

    public class GetCategoriesQueryHandler(ICategoryRepository categoryRepository) : IRequestHandler<GetCategoriesQuery, List<CategoryDto>>
    {
        public async Task<List<CategoryDto>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
        {
            List<Category> categories = await categoryRepository.GetCategoriesAsync();
            return [.. categories.Select(category => new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                Url = category.Url,
                Icon = category.Icon
            })];
        }
    }
}