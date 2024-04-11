using MediatR;
using static MediatoRPractice.MediatorPattern.MediatorPattern;

namespace MediatoRPractice.Product
{
    public class ProductHandlerQuery
    {
        public class GetProductDto
        {
            public string ProductName { get; set; }
            public string ProductDescription { get; set; }
        };
        public class GetProductQuery : IRequestCustom<GetProductDto>
        {
            public int Id { get; set; }
        }

        public class CreateProductCommandHandler : IRequestHandlerCustom<GetProductQuery, GetProductDto>
        {
            public Task<GetProductDto> Handle(GetProductQuery request, CancellationToken cancellationToken)
            {
                var response = new GetProductDto
                {
                    ProductName = "Product A",
                    ProductDescription = "Des A"
                };
                return Task.FromResult(response);
            }
        }
    }
}
