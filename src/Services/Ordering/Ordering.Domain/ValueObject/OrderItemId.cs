using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Domain.ValueObject
{
    public record OrderItemId
    {
        public Guid Value { get;}

        private OrderItemId(Guid value)
        {
            Value = value;
        }

        public static OrderItemId Of(Guid value)
        {
            ArgumentNullException.ThrowIfNull(value);
            return new OrderItemId(value);
        }
    }
}
