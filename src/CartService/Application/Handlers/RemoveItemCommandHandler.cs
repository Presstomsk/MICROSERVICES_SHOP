namespace CartService.Application.Handlers
{
    using Dto;
    using Queries;    
    using MediatR;
    using CartService.Application.Services.Interfaces;

    public class RemoveItemCommandHandler(ICartService cartService) : IRequestHandler<RemoveItemCommand, CartDto>
    {
        public async Task<CartDto> Handle(RemoveItemCommand request, CancellationToken cancellationToken)
        {
            var cart = await cartService.RemoveItemFromCartAsync(request.userId, request.productId);

            var cartItems = cart.CartItems.Select(ci => new CartItemDto
                                {
                                    ProductId = ci.ProductId,
                                    ProductName = ci.ProductName,
                                    Quantity = ci.Quantity,
                                    Price = ci.Price
                                }).ToList();
            return new CartDto
            {
                UserId = cart.UserId,
                CartItems = cartItems,
                TotalCartItems = cart.TotalCartItems,
                TotalCartPrice = cart.TotalCartPrice,
                UpdatedAt = cart.UpdatedAt
            };
        }
    }
}