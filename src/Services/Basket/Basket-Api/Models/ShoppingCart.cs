using System.ComponentModel.DataAnnotations;

namespace Basket_Api.Models
{
    public class ShoppingCart
    {
        [MinLength(1, ErrorMessage = "User Name is not Empty")]
        public string UserName { get; set; } = default!;
        [Required(ErrorMessage = "Items is not null")]
        public List<ShoppingCartItem> Items { get; set; } = new List<ShoppingCartItem>();
        public decimal TotalPrice => Items.Sum(x => x.Price * x.Quantity);

        public ShoppingCart(string userName)
        {
            UserName = userName;
        }

        public ShoppingCart() { }
    }
}
