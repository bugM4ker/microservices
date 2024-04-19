using BuildingBlocks.CQRS;
using Catalog_Api.Models;
using Marten;
using Marten.Linq.QueryHandlers;

namespace Catalog_Api.Products.GetProducts
{
    public class GetProductsQuery : IQuery<GetProductsResult>
    {
    }

    public class GetProductsHandler(IDocumentSession session) : IQueryHandler<GetProductsQuery, GetProductsResult>
    {
        public async Task<GetProductsResult> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            var produts = await session.Query<Product>().ToListAsync(cancellationToken);
            var productDto = new List<ProductDto>();
            foreach (var product in produts)
            {
                productDto.Add(new ProductDto
                {
                    Name = product.Name,
                    Category = product.Category,
                    Description = product.Description,
                    ImageFile = product.ImageFile,
                    Price = product.Price
                });
            }
            return new GetProductsResult(productDto);
        }
    }
}
