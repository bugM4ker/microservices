

namespace Ordering.Domain.ValueObject
{
    public record ProductId
    {
        public Guid Value { get;}

        private ProductId(Guid value)
        {
            Value = value;
        }

        public static ProductId Of(Guid value)
        {
            ArgumentNullException.ThrowIfNull(value);
            return new ProductId(value);
        }
    }
}
