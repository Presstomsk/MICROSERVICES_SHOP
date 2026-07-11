namespace CatalogService.Controllers
{
    using CatalogService.Application.Dto;
    using CatalogService.Application.Queries;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("catalog/[controller]")]
    public class CategoryController(IMediator mediator, ILogger<CategoryController> logger) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<CategoryDto>>> GetCategoriesAsync()
        {
            logger.LogInformation("Запрос всех категорий");

            var categories = await mediator.Send(new GetCategoriesQuery());            
            return Ok(categories);
        }        
    }
}
