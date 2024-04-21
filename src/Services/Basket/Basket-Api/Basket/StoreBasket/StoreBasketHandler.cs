using Basket_Api.Models;
using BuildingBlocks.CQRS;

namespace Basket_Api.Basket.StoreBasket
{

    public record StoreBasketResult(bool isStored) { }

    public class StoreBasketCommand(ShoppingCart shoppingCart) : ICommand<StoreBasketResult>
    {

    }
    public class StoreBasketHandler() : ICommandHandler<StoreBasketCommand, StoreBasketResult>
    {
        public async Task<StoreBasketResult> Handle(StoreBasketCommand request, CancellationToken cancellationToken)
        {
            // Handle logic here
            // Store in DB
            // Update cache
            return new StoreBasketResult(true);
        }
    }
}
