namespace CartService.Application.Queries
{
    using Dto;
    using MediatR;

    public record AddItemCommand(int userId, CartItemDto item) : IRequest<CartDto>;
}