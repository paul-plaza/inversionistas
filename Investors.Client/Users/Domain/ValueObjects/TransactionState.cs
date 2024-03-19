namespace Investors.Client.Users.Domain.ValueObjects
{
    public class TransactionState : IValueObject
    {
        protected enum TransactionStateEnum
        {
            Requested = 1,
            Accepted = 2,
            Rejected = 3,
            Cancelled = 4,
        }

        public static readonly TransactionState Requested = new(TransactionStateEnum.Requested);
        public static readonly TransactionState Accepted = new(TransactionStateEnum.Accepted);
        public static readonly TransactionState Rejected = new(TransactionStateEnum.Rejected);
        public static readonly TransactionState Cancelled = new(TransactionStateEnum.Cancelled);

        private static readonly TransactionState[] AllTransactionState =
        {
            Requested, Accepted, Rejected, Cancelled,
        };

        protected TransactionState()
        {

        }
        protected TransactionState(TransactionStateEnum value)
        {
            Value = (int)value;
        }
        public int Value { get; }

        public static TransactionState From(int i)
        {
            return AllTransactionState.Single(x => x.Value == i);
        }

        public override string ToString()
        {
            switch (Value)
            {
                case (int)TransactionStateEnum.Requested:
                    return "Solicitado";
                case (int)TransactionStateEnum.Accepted:
                    return "Aceptado";
                case (int)TransactionStateEnum.Rejected:
                    return "Rechazado";
                case (int)TransactionStateEnum.Cancelled:
                    return "Cancelado";
                default:
                    return "No existe estado";
            }
        }
    }
}