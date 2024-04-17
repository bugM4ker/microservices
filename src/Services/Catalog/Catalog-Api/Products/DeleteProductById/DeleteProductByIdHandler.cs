using BuildingBlocks.CQRS;
using Catalog_Api.Models;
using Marten;

namespace Catalog_Api.Products.DeleteProductById
{


    public class DeleteProductByIdCommand : ICommand<DeleteProductByIdResult>
    {
        public Guid Id { get; set; }
    }
    public class DeleteProductByIdHandler(IDocumentSession session) : ICommandHandler<DeleteProductByIdCommand, DeleteProductByIdResult>
    {
        public async Task<DeleteProductByIdResult> Handle(DeleteProductByIdCommand request, CancellationToken cancellationToken)
        {
            var product = await session.Query<Product>().SingleOrDefaultAsync(p => p.Id == request.Id);
            if (product == null)
            {
                return new DeleteProductByIdResult()
                {
                    IsSuccess = false,
                    Message = "Not Found"
                };
            }
            session.Delete<Product>(request.Id);
            await session.SaveChangesAsync(cancellationToken);

            return new DeleteProductByIdResult()
            {
                IsSuccess = true,
                Message = "Deleted"
            };
        }
    }

    public class DeleteProductByIdResult
    {
        public bool IsSuccess { get; set; }
        public string? Message { get; set; }
    }
}
