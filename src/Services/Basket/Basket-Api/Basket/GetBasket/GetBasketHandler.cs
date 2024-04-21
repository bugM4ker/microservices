using Basket_Api.Models;
using BuildingBlocks.CQRS;

namespace Basket_Api.Basket.GetBasket
{

    public record GetBasketReuslt(ShoppingCart ShoppingCart) { }

    public class GetBasketQuery(string UserName) : IQuery<GetBasketReuslt>
    {
    }

    public class GetBasketHandler : IQueryHandler<GetBasketQuery, GetBasketReuslt>
    {
        public async Task<GetBasketReuslt> Handle(GetBasketQuery request, CancellationToken cancellationToken)
        {
            return new GetBasketReuslt(new ShoppingCart("test"));
        }
    }
}
