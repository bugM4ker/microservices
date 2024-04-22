using Basket_Api.Models;

namespace Basket_Api.Repository
{
    public interface IBasketRepository
    {
        Task<ShoppingCart> GetBasket(string userName, CancellationToken cancellationToken);
        Task<ShoppingCart> StoreBasket(ShoppingCart cart, CancellationToken cancellationToken);
        Task<bool> DeleteBasket(string userName, CancellationToken cancellationToken);
    }
}
