namespace CartService.Application.Queries
{
    using Dto;
    using MediatR;

    public record RemoveItemCommand(int userId, int productId) : IRequest<CartDto>;
}