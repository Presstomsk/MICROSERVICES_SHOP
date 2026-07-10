namespace CartService.Application.Handlers
{    
    using Queries;    
    using MediatR;
    using CartService.Application.Services.Interfaces;

    public class ClearCartCommandHandler(ICartService cartService) : IRequestHandler<ClearCartCommand, Unit>
    {
        public async Task<Unit> Handle(ClearCartCommand request, CancellationToken cancellationToken)
        {
            await cartService.ClearCartAsync(request.userId);

            return Unit.Value;            
        }
    }
}