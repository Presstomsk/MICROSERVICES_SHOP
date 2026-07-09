namespace CatalogService.Controllers
{
    using CatalogService.Application.Dto;
    using CatalogService.Application.Queries;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("[controller]")]
    public class ProductController(IMediator mediator, ILogger<ProductController> logger) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<ProductDto>>> GetAllProductsAsync()
        {
            logger.LogInformation("Запрос каталога");

            var products = await mediator.Send(new GetAllProductsQuery());            
            return Ok(products);
        }

        [HttpGet("category/{categoryId}")]
        public async Task<ActionResult<List<ProductDto>>> GetProductsByCategoryAsync(int categoryId)
        {
            logger.LogInformation("Запрос каталога категории {CategotyId}", categoryId);

            var products = await mediator.Send(new GetProductsByCategoryQuery(categoryId));            
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto?>> GetProductAsync(int id)
        {
            logger.LogInformation("Запрос продукта {ProductId}", id);

            var product = await mediator.Send(new GetProductQuery(id));            
            return Ok(product);
        }

        [HttpGet("search/{searchText}")]
        public async Task<ActionResult<List<ProductDto>>> SearchProductsAsync(string searchText)
        {
            logger.LogInformation("Поиск продукта по строке => {SearchText}", searchText);

            var products = await mediator.Send(new SearchProductsQuery(searchText));            
            return Ok(products);
        }
    }
}
