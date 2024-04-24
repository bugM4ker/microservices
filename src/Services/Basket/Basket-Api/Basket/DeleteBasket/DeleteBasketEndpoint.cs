using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Basket_Api.Basket.DeleteBasket
{

    [ApiController]
    [Route("api/basket")]
    public class DeleteBasketEndpoint : ControllerBase
    {
        private readonly IMediator _mediator;

        public DeleteBasketEndpoint(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteBasket(string userName)
        {
            if (userName == null)
            {
                return BadRequest("User name is not null");
            }
            try
            {
                var command = new DeleteBasketCommand(userName);
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
