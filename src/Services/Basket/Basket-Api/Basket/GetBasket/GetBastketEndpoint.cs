using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Basket_Api.Basket.GetBasket
{
    [ApiController]
    [Route("api/basket")]
    public class GetBastketEndpoint : ControllerBase
    {
        private readonly IMediator _mediator;

        public GetBastketEndpoint(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetBasket(string userName)
        {
            try
            {
                var query = new GetBasketQuery(userName);
                var res = await _mediator.Send(query);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
