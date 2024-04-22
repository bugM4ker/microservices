using Basket_Api.Exceptions;
using Basket_Api.Models;
using Marten;

namespace Basket_Api.Repository
{
    public class BasketRepositoryBase(IDocumentSession session) : IBasketRepository
    {
        public async Task<bool> DeleteBasket(string userName, CancellationToken cancellationToken)
        {
            session.Delete<ShoppingCart>(userName);
            await session.SaveChangesAsync();
            return true;

        }

        public async Task<ShoppingCart> GetBasket(string userName, CancellationToken cancellationToken)
        {
            var basket = await session.LoadAsync<ShoppingCart>(userName, cancellationToken);
            return basket is null ? throw new NotFoundException(userName) : basket;
        }

        public async Task<ShoppingCart> StoreBasket(ShoppingCart cart, CancellationToken cancellationToken)
        {
            session.Store(cart);
            await session.SaveChangesAsync();
            return cart;
        }
    }
}
