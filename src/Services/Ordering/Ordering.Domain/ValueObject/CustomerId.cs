using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Domain.ValueObject
{
    public record CustomerId
    {
        public Guid Value { get;}

        private CustomerId(Guid value)
        {
            Value = value;
        }

        public static CustomerId Of(Guid value)
        {
            ArgumentNullException.ThrowIfNull(value);
            return new CustomerId(value);
        }
    }
}
