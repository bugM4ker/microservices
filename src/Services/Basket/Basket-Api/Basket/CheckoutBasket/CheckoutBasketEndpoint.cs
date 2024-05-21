using Basket.API.Basket.CheckoutBasket;
using Basket_Api.Basket.StoreBasket;
using Basket_Api.Dtos;
using Basket_Api.Models;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Basket_Api.Basket.CheckoutBasket
{

    [ApiController]
    [Route("api/basket/checkout")]


    public class CheckoutBasketEndpoint : ControllerBase
    {
        private readonly IMediator _mediator;

        public record CheckoutBasketRequest(BasketCheckoutDto BasketCheckoutDto);
        public record CheckoutBasketResponse(bool IsSuccess);

        public CheckoutBasketEndpoint(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        public async Task<IActionResult> StoreBasket([FromBody] CheckoutBasketRequest basketCheckoutDto)
        {
            try
            {
                var command = basketCheckoutDto.Adapt<CheckoutBasketCommand>();
                var result = await _mediator.Send(command);
                var response = result.Adapt<CheckoutBasketResponse>();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}
