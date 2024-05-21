namespace Ordering.Domain.ValueObject
{
    public record OrderId
    {
        public Guid Value { get; }

        private OrderId(Guid value)
        {
            Value = value;
        }

        public static OrderId Of(Guid value)
        {
            ArgumentNullException.ThrowIfNull(value);
            return new OrderId(value);
        }

    }
}