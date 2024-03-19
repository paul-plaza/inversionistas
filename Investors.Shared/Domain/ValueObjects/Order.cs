using System.Text.RegularExpressions;
using Ardalis.GuardClauses;
using CSharpFunctionalExtensions;

namespace Investors.Shared.Domain.ValueObjects
{
    public class Order : IValueObject
    {
        private const int MinLength = 1;

        private const int MaxLength = 50;

        private Order(int value)
        {
            Value = value;
        }
        public int Value { get; }

        public static Result<Order> Create(int input)
        {
            Guard.Against.OutOfRange(input, nameof(Order), MinLength, MaxLength);

            return Result.Success(new Order(input));
        }

        public static explicit operator Order(int order)
        {
            return Create(order).Value;
        }

        public static implicit operator int(Order order)
        {
            return order.Value;
        }
    }
}