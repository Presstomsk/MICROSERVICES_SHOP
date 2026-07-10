namespace CartService.Application.Queries
{
    using Dto;
    using MediatR;

    public record UpdateQuantityCommand(int userId, int productId, int quantity) : IRequest<CartDto>;
}