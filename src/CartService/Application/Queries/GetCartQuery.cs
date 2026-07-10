namespace CartService.Application.Queries
{
    using Dto;
    using MediatR;

    public record GetCartQuery(int userId) : IRequest<CartDto>;
}