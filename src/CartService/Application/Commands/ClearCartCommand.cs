namespace CartService.Application.Queries
{    
    using MediatR;

    public record ClearCartCommand(int userId) : IRequest<Unit>;
}