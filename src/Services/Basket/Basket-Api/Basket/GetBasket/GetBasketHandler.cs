using Basket_Api.Models;
using Basket_Api.Repository;
using BuildingBlocks.CQRS;

namespace Basket_Api.Basket.GetBasket
{

    public record GetBasketReuslt(ShoppingCart ShoppingCart) { }

    public record GetBasketQuery(string UserName) : IQuery<GetBasketReuslt>
    {
    }

    public class GetBasketHandler(IBasketRepository repository) : IQueryHandler<GetBasketQuery, GetBasketReuslt>
    {
        public async Task<GetBasketReuslt> Handle(GetBasketQuery request, CancellationToken cancellationToken)
        {
            var basket = await repository.GetBasket(request.UserName, cancellationToken);
            return new GetBasketReuslt(basket);
        }
    }
}
