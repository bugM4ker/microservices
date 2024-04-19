using BuildingBlocks.CQRS;
using Catalog_Api.Models;
using Catalog_Api.Products.CreateProduct;
using Marten;

namespace Catalog_Api.Products.UpdateProduct
{

    public class UpdateProductCommand : ICommand<CreateProductResult>
    {
        public ProductForm ProductForm { get; set; }
    }

    public class UpdateProductHandler(IDocumentSession session) : ICommandHandler<UpdateProductCommand, CreateProductResult>
    {
        public async Task<CreateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
        {
            var product = await session.Query<Product>().FirstOrDefaultAsync(x => x.Name == command.ProductForm.Name);
            if (product == null)
            {
                return new CreateProductResult(product.Id);
            }
            product.Name = command.ProductForm.Name;
            product.Category = command.ProductForm.Category;
            product.Description = command.ProductForm.Description;
            product.ImageFile = command.ProductForm.ImageFile;
            product.Price = command.ProductForm.Price;
            await session.SaveChangesAsync(cancellationToken);
            return new CreateProductResult(product.Id);
        }
    }
}