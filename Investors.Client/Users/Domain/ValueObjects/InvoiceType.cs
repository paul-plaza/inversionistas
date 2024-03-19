namespace Investors.Client.Users.Domain.ValueObjects
{
    public record InvoiceType : IValueObject
    {
        public enum InvoiceTypeEnum
        {
            Cashback = 1,
            Nights = 2,
            Mixed = 3,
        }

        public static readonly InvoiceType Cashback = new(InvoiceTypeEnum.Cashback);
        public static readonly InvoiceType Night = new(InvoiceTypeEnum.Nights);
        public static readonly InvoiceType Mixed = new(InvoiceTypeEnum.Mixed);

        public static readonly InvoiceType[] AllInvoiceTypes =
        {
            Cashback, Night, Mixed
        };

        protected InvoiceType()
        {

        }
        protected InvoiceType(InvoiceTypeEnum value)
        {
            Value = (int)value;
        }
        public int Value { get; }

        public static InvoiceType From(int i)
        {
            return AllInvoiceTypes.Single(x => x.Value == i);
        }

        public override string ToString()
        {
            switch (Value)
            {
                case (int)InvoiceTypeEnum.Cashback:
                    return "Cashback";
                case (int)InvoiceTypeEnum.Nights:
                    return "Nights";
                case (int)InvoiceTypeEnum.Mixed:
                    return "Mixed";
                default:
                    return "Sin tipo de factura";
            }
        }
    }
}