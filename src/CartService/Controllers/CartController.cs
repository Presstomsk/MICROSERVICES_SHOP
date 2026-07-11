namespace CartService.Controllers
{
    using CartService.Application.Dto;
    using CartService.Application.Queries;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("cart/[controller]")]
    public class CartController(IMediator mediator, ILogger<CartController> logger) : ControllerBase
    {
        [HttpGet("{userId}")]
        public async Task<ActionResult<CartDto>> GetCartAsync(int userId)
        {
            logger.LogInformation("Запрос корзины {UserId}", userId);

            var cart = await mediator.Send(new GetCartQuery(userId));            
            return Ok(cart);
        }

        [HttpPost("{userId}/items")]
        public async Task<ActionResult<CartDto>> AddItemToCartAsync(int userId, [FromBody] CartItemDto item)
        {
            logger.LogInformation("Добавление {UserId} в корзину продукта {ProductId}", userId, item.ProductId);

            var cart = await mediator.Send(new AddItemCommand(userId, item));            
            return Ok(cart);
        }

        [HttpPut("{userId}/items/{productId}")]
        public async Task<IActionResult> UpdateQuantity(int userId, int productId, [FromBody] int quantity)
        {
            logger.LogInformation("Обновление количества товара {ProductId} в корзине {UserId}", productId, userId);

            var cart = await mediator.Send(new UpdateQuantityCommand(userId, productId, quantity));
            return Ok(cart);
        }

        [HttpDelete("{userId}/items/{productId}")]
        public async Task<ActionResult<CartDto>> RemoveItemFromCartAsync(int userId, int productId)
        {
            logger.LogInformation("Удаление {UserId} из корзины продукта {ProductId}", userId, productId);

            var cart = await mediator.Send(new RemoveItemCommand(userId, productId));            
            return Ok(cart);
        }

        [HttpDelete("{userId}")]
        public async Task<ActionResult> ClearCartAsync(int userId)
        {
            logger.LogInformation("Очистка корзины {UserId}", userId);

            await mediator.Send(new ClearCartCommand(userId));            
            return Ok();
        }
    }
}
