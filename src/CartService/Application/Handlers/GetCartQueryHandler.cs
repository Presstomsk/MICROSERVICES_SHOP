namespace CartService.Application.Handlers
{
    using Dto;
    using Queries;
    using MediatR;
    using Services.Interfaces;

    public class GetAllProductsQueryHandler(ICartService cartService) : IRequestHandler<GetCartQuery, CartDto>
    {
        public async Task<CartDto> Handle(GetCartQuery request, CancellationToken cancellationToken)
        {
            var cart = await cartService.GetCartAsync(request.userId);

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