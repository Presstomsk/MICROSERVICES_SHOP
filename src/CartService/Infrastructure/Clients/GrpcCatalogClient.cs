namespace CartService.Infrastructure.Clients
{
    using System.Globalization;
    using CartService.Application.Dto;
    using CartService.Application.Interfaces;
    using CatalogService.Grpc;
    using Grpc.Core;

    public class GrpcCatalogClient(CatalogGrpc.CatalogGrpcClient client) : ICatalogClient
    {
        public async IAsyncEnumerable<ProductDto> GetProductsAsync(int[] productIds)
        {
            var request = new GetProductsRequest();
            request.ProductIds.AddRange(productIds);

            using var call = client.GetProducts(request);

            await foreach (GetProductResponse productResponse in call.ResponseStream.ReadAllAsync())
            {
                yield return new ProductDto
                    {
                        Id = productResponse.Id,
                        Name = productResponse.Name,
                        Price = decimal.Parse(productResponse.Price, CultureInfo.InvariantCulture)
                    };
            }
        }
    }
}