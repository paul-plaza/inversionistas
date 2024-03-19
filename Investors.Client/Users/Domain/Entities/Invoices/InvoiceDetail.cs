namespace Investors.Client.Users.Domain.Entities.Invoices
{
    public class InvoiceDetail : BaseEntity<int>
    {
        public string Group { get; set; }
        public string GroupDetail { get; set; }
        public double TotalValue { get; private set; }
        public Invoice Invoice { get; private set; }
        public int InvoiceId { get; private set; }
        public DateTime? CheckIn { get; private set; }
        public DateTime? CheckOut { get; private set; }
        public int? Nights { get; private set; }
        public int TransactionType { get; private set; }
        protected InvoiceDetail()
        {
        }

        private InvoiceDetail(string group,
            string groupDetail,
            double totalValue,
            DateTime? checkIn,
            DateTime? checkOut,
            int? nights,
            int transactionType,
            Guid createBy,
            DateTime createOn,
            Status status) : base(createBy, createOn, status)
        {
            Group = group;
            GroupDetail = groupDetail;
            TotalValue = totalValue;
            CheckIn = checkIn;
            CheckOut = checkOut;
            Nights = nights;
            TransactionType = transactionType;
        }
        public static Result<InvoiceDetail> Create(
            string group,
            string groupDetail,
            double totalValue,
            DateTime? checkIn,
            DateTime? checkOut,
            int nights,
            int transactionType,
            Guid userInSession)
        {

            int? nightsToSave = null;

            //Cashback
            if (transactionType == 1)
            {
                checkIn = null;
                checkOut = null;
            }

            //Noches
            if (transactionType == 2)
            {
                nightsToSave = nights;
            }

            return Result.Success(new InvoiceDetail(
                group,
                groupDetail,
                totalValue,
                checkIn,
                checkOut,
                nightsToSave,
                transactionType,
                userInSession,
                DateTime.Now,
                Status.Active));
        }
    }
}