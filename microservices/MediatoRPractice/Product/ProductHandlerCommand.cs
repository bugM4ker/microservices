using MediatR;

namespace MediatoRPractice.Product
{
    public class ProductHandlerCommand
    {
        public record CreateProductResponse(int id);
        public class CreateProductCommand : IRequest<CreateProductResponse>
        {
            public string ProductName { get; set; }
            public string ProductDescription { get; set; } = string.Empty;
        }

        public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, CreateProductResponse>
        {
            public Task<CreateProductResponse> Handle(CreateProductCommand request, CancellationToken cancellationToken)
            {
                var response = new CreateProductResponse(100);
                return Task.FromResult(response);
            }
        }

    }
}
