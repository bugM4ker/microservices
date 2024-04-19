using Catalog_Api.Products.CreateProduct;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Catalog_Api.Products.UpdateProduct
{
    [Route("api/product")]
    [ApiController]
    public class UpdateProductEndpoint : ControllerBase
    {
        private readonly IMediator _mediator;

        public UpdateProductEndpoint(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPut]
        public async Task<IActionResult> CreateProductAsync([FromBody] ProductForm productForm)
        {
            var command = new UpdateProductCommand() { ProductForm = productForm};
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
