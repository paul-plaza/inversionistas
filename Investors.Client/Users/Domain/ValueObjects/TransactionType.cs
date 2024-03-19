using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Investors.Client.Users.Domain.ValueObjects
{
    public class TransactionType : IValueObject
    {
        protected enum TransactionTypeEnum
        {
            Cashback = 1,
            Nights = 2,
        }

        public static readonly TransactionType Cashback = new(TransactionTypeEnum.Cashback);
        public static readonly TransactionType Nights = new(TransactionTypeEnum.Nights);

        private static readonly TransactionType[] AllTransactionState =
        {
            Cashback, Nights,
        };

        protected TransactionType()
        {

        }
        protected TransactionType(TransactionTypeEnum value)
        {
            Value = (int)value;
        }
        public int Value { get; }

        public static TransactionType From(int i)
        {
            return AllTransactionState.Single(x => x.Value == i);
        }

        public override string ToString()
        {
            switch (Value)
            {
                case (int)TransactionTypeEnum.Cashback:
                    return "Cashback";
                case (int)TransactionTypeEnum.Nights:
                    return "Nights";
                default:
                    return "No existe tipo";
            }
        }
    }
}