using BuildingBlocks.CQRS;
using Catalog_Api.Models;
using Catalog_Api.Products.GetProductById.Catalog_Api.Products.GetProducts;
using Marten;

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
            var productDto = new ProductDto();
            if (product == null)
            {
                productDto.Name = "";
                productDto.Category = new List<string>();
                productDto.Description = "";
                productDto.ImageFile = "";
                productDto.Price = 0;
                return new GetProductByIdResult(productDto);
            }
            productDto.Name = product.Name;
            productDto.Category = product.Category;
            productDto.Description = product.Description;
            productDto.ImageFile = product.ImageFile;
            productDto.Price = product.Price;
            return new GetProductByIdResult(productDto);
        }
    }
}
