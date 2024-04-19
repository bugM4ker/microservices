using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Catalog_Api.Products.GetProducts
{
    [Route("api/product")]
    [ApiController]
    public class GetProductsEndpoint : ControllerBase
    {
        private readonly IMediator _mediator;

        public GetProductsEndpoint(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetProductAsync()
        {
            var result = await _mediator.Send(new GetProductsQuery());
            return Ok(result);
        }

    }
}
