namespace Basket_Api.Models
{
    public class ShoppingCartItem
    {
        public Guid ProductId { get; set; }
        public string? Color { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string? ProductName { get; set; }
    }
}
