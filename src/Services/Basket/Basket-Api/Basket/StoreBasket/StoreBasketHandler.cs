using Basket_Api.Models;
using Basket_Api.Repository;
using BuildingBlocks.CQRS;
using Discount_gRPC.Protos;

namespace Basket_Api.Basket.StoreBasket
{

    public record StoreBasketResult(string Username) { }

    public record StoreBasketCommand(ShoppingCart shoppingCart) : ICommand<StoreBasketResult>
    {

    }
    public class StoreBasketHandler(IBasketRepository repository, DisCountProtoService.DisCountProtoServiceClient discountProto) : ICommandHandler<StoreBasketCommand, StoreBasketResult>
    {
        public async Task<StoreBasketResult> Handle(StoreBasketCommand request, CancellationToken cancellationToken)
        {
            foreach (var item in request.shoppingCart.Items)
            {
                var discountResponse = await discountProto.GetDiscountAsync(new GetDiscountRequest { ProductName = item.ProductName }, cancellationToken: cancellationToken);
                item.Price -= discountResponse.Amount;
            }
            await repository.StoreBasket(request.shoppingCart, cancellationToken);
            return new StoreBasketResult(request.shoppingCart.UserName);
        }
    }
}
