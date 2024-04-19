using BuildingBlocks.CQRS;
using Catalog_Api.Models;
using Marten;
using MediatR;

namespace Catalog_Api.Products.CreateProduct
{
    public class CreateProductCommand : ICommand<CreateProductResult>
    {
        public ProductForm ProductForm { get; set; }
    }

    public class CreateProductHandler(IDocumentSession session) : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            // Handle logic here
            var p = new Product
            {
                Name = command.ProductForm.Name,
                Category = command.ProductForm.Category,
                Description = command.ProductForm.Description,
                ImageFile = command.ProductForm.ImageFile,
                Price = command.ProductForm.Price,
            };

            //Save to Db
            session.Store(p);
            await session.SaveChangesAsync(cancellationToken);
            return new CreateProductResult(p.Id);
        }
    }
}
