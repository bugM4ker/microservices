﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Domain.ValueObject
{
    public class OrderName
    {
        private const int DefaultLength = 5;
        public string Value { get;}



        private OrderName() { }
        private OrderName(string value)
        {
            Value = value;
        }

        public static OrderName Of(string value) 
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(value);
            ArgumentOutOfRangeException.ThrowIfNotEqual(value.Length, DefaultLength);

            return new OrderName(value);
        }
    }
}
