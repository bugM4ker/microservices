using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Catalog_Api.Products.CreateProduct
{

    [Route("api/product")]
    [ApiController]
    public class CreateProductEndpoint : ControllerBase
    {
        private readonly IMediator _mediator;

        public CreateProductEndpoint(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateProductAsync([FromBody] ProductForm productForm)
        {
            var command = new CreateProductCommand { ProductForm = productForm };
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
