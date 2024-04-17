using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Catalog_Api.Products.DeleteProductById
{
    [Route("api/product")]
    [ApiController]
    public class DeleteProducByIdEndpoint : ControllerBase
    {
        private readonly IMediator _mediator;

        public DeleteProducByIdEndpoint(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteProductById(Guid Id)
        {
            var command = new DeleteProductByIdCommand() { Id = Id };
            var result = await _mediator.Send(command);
            return Ok(result);
        }

    }

}
