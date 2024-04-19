using Catalog_Api.Products.GetProducts;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Catalog_Api.Products.GetProductById
{
    [Route("api/product")]
    [ApiController]
    public class GetProductByIdEndpoint : ControllerBase
    {
        private readonly IMediator _mediator;

        public GetProductByIdEndpoint(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductByIdAsync(Guid id)
        {
            var query = new GetProductByIdQuery { Id = id };
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}
