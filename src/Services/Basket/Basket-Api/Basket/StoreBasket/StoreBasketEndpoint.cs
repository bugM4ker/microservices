using Basket_Api.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Basket_Api.Basket.StoreBasket
{
    [ApiController]
    [Route("api/basket")]


    public class StoreBasketEndpoint : ControllerBase
    {
        private readonly IMediator _mediator;

        public StoreBasketEndpoint(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        public async Task<IActionResult> StoreBasket([FromBody] ShoppingCart shoppingCart)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var command = new StoreBasketCommand(shoppingCart);
                var result = await _mediator.Send(command);
                return Ok("Shopping cart stored successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}
