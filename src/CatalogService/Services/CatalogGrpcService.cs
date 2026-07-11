namespace CatalogService.Services
{
    using CatalogService.Grpc;
    using MediatR;
    using global::Grpc.Core;
    using CatalogService.Application.Queries;
    using CatalogService.Application.Dto;

    public class CatalogGrpcService(IMediator mediator) : CatalogGrpc.CatalogGrpcBase
    {
        public override async Task GetProducts(GetProductsRequest request,
            IServerStreamWriter<GetProductResponse> responseStream,
            ServerCallContext context)
        {
            List<ProductDto> products = await mediator.Send(new GetProductsQuery([.. request.ProductIds]));

            foreach (ProductDto product in products)
            {
                await responseStream.WriteAsync(new GetProductResponse
                {
                    Id = product.Id,
                    Name = product.Title,
                    Price = product.Price.ToString("F2")                    
                });
            }                
        }
    }
}