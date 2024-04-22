using Basket_Api.Models;
using Basket_Api.Repository;
using BuildingBlocks.CQRS;

namespace Basket_Api.Basket.StoreBasket
{

    public record StoreBasketResult(string Username) { }

    public record StoreBasketCommand(ShoppingCart shoppingCart) : ICommand<StoreBasketResult>
    {

    }
    public class StoreBasketHandler(IBasketRepository repository) : ICommandHandler<StoreBasketCommand, StoreBasketResult>
    {
        public async Task<StoreBasketResult> Handle(StoreBasketCommand request, CancellationToken cancellationToken)
        {
            await repository.StoreBasket(request.shoppingCart, cancellationToken);
            // Handle logic here
            // Store in DB
            // Update cache
            return new StoreBasketResult(request.shoppingCart.UserName);
        }
    }
}
