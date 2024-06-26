﻿using Ordering.Domain.Abstractions;
using Ordering.Domain.ValueObject;

namespace Ordering.Domain.Models
{
    public class OrderItems : Entity<OrderItemId>
    {
        internal OrderItems(OrderId orderId, ProductId productId, int quantity, decimal price)
        {
            Id = OrderItemId.Of(Guid.NewGuid());
            OrderId = orderId;
            ProductId = productId;
            Quantity = quantity;
            Price = price;
        }

        public OrderId OrderId { get; private set; }
        public ProductId ProductId { get; private set; }
        public int Quantity { get; private set; } = default!;
        public decimal Price { get; private set; } = default!;
    }
}
