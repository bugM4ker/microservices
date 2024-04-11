using BuildingBlocks.CQRS;
using Catalog_Api.Models;
using Catalog_Api.Products.GetProductById.Catalog_Api.Products.GetProducts;
using Marten;
using Marten.Linq.QueryHandlers;

namespace Catalog_Api.Products.GetProductById
{

    public class GetProductByIdQuery : IQuery<GetProductByIdResult>
    {
        public Guid Id { get; set; }
    }





    public class GetProductByIdHandler(IDocumentSession session) : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
    {
        public async Task<GetProductByIdResult> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await session.Query<Product>().SingleOrDefaultAsync(p => p.Id == request.Id);
            var productDto = new ProductDto()
            {
                Name = product.Name,
                Category = product.Category,
                Description = product.Description,
                ImageFile = product.ImageFile,
                Price = product.Price
            };
            return new GetProductByIdResult(productDto);
        }
    }
}
