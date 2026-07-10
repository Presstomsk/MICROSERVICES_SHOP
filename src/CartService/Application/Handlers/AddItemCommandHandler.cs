namespace CartService.Application.Handlers
{
    using Dto;
    using Queries;    
    using MediatR;
    using CartService.Application.Services.Interfaces;
    using CartService.Entities;

    public class AddItemCommandHandler(ICartService cartService) : IRequestHandler<AddItemCommand, CartDto>
    {
        public async Task<CartDto> Handle(AddItemCommand request, CancellationToken cancellationToken)
        {
            var cartItem = new CartItem
                {
                    ProductId = request.item.ProductId,
                    ProductName = request.item.ProductName,
                    Quantity = request.item.Quantity,
                    Price = request.item.Price
                };

            var cart = await cartService.AddItemToCartAsync(request.userId, cartItem);

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