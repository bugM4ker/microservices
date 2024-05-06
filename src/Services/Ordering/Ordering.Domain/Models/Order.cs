using Ordering.Domain.Abstractions;
using Ordering.Domain.Enum;
using Ordering.Domain.Events;
using Ordering.Domain.ValueObject;

namespace Ordering.Domain.Models
{
    public class Order : Aggregate<OrderId>
    {
        private List<OrderItems> _orderItems = new List<OrderItems>();
        public IReadOnlyList<OrderItems> OrderItems => _orderItems.AsReadOnly();
        public CustomerId CustomerId { get; private set; }
        public OrderName OrderName { get; private set; } = default!;
        public Address ShippingAdress { get; private set; } = default!;
        public Address BillingAddress { get; private set; } = default!;
        public Payment Payment { get; private set; } = default!;
        public OrderStatus OrderStatus { get; private set; } = OrderStatus.Pending;

        public decimal TotalPrice
        {
            get => OrderItems.Sum(x => x.Price * x.Quantity);
            private set { }
        }

        public static Order Create(OrderId id, CustomerId customerId, OrderName orderName, Address shippingAdress, Address billingAdress, Payment payment)
        {
            var order = new Order
            {
                Id = id,
                CustomerId = customerId,
                OrderName = orderName,
                ShippingAdress = shippingAdress,
                BillingAddress = billingAdress,
                Payment = payment,
                OrderStatus = OrderStatus.Pending,
            };

            order.AddDomainEvent(new OrderCreatedEvent(order));

            return order;
        }

        public void Update(OrderName orderName, Address shippingAdress, Address billingAdress, Payment payment, OrderStatus orderStatus)
        {
            orderName = orderName;
            ShippingAdress = shippingAdress;
            BillingAddress = billingAdress;
            Payment = payment;
            OrderStatus = orderStatus;

            AddDomainEvent(new OrderUpdatedEvent(this));
        }

        public void Add(ProductId productId, int quantity, decimal price)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(quantity);
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(price);

            var OrderItem = new OrderItems(Id, productId, quantity, price);
            _orderItems.Add(OrderItem);
            
        }

        public void Remove(ProductId productId)
        {
            var orderItem = _orderItems.FirstOrDefault(o => o.ProductId == productId);
        }
    }
}
