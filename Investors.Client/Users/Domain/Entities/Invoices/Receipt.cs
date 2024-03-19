using Investors.Client.Users.Domain.Aggregate;
using Investors.Client.Users.Domain.Events.UserEvents;

namespace Investors.Client.Users.Domain.Entities.Invoices
{
    public class Receipt : BaseEntity<int>
    {
        protected Receipt()
        {

        }
        public DateOnly Date { get; private set; }
        public string Identification { get; private set; } = string.Empty;
        public bool IsSync { get; private set; }
        public Guid UserId { get; private set; }
        public virtual User User { get; private set; }

        /// <summary>
        /// Integridad de entidad y agregar valor a entidades
        /// </summary>
        private readonly List<Invoice> _invoices = new();

        /// <summary>
        /// Lista de facturas
        /// </summary>
        public virtual IReadOnlyList<Invoice> Invoices => _invoices;


        private Receipt(Guid userId,
            string identification,
            DateOnly date,
            string investorName,
            string email,
            bool isSync,
            Guid createBy,
            DateTime createOn,
            Status status) : base(createBy, createOn, status)
        {
            UserId = userId;
            Identification = identification;
            Date = date;
            IsSync = isSync;
            RaiseDomainEvent(new RegisterOrReSyncReceiptEvent(userId, investorName, email, Identification, date, createBy));
        }

        public static Result<Receipt> Create(
            Guid userId,
            DateOnly userCreatedOn,
            string identification,
            DateOnly invoiceDate,
            string investorName,
            string email,
            Guid userInSession)
        {
            var currentDate = DateOnly.FromDateTime(DateTime.Now);
            var minDate = DateOnly.FromDateTime(DateTime.Now.AddMonths(-3));

            if (invoiceDate > currentDate &&
                invoiceDate < userCreatedOn &&
                invoiceDate >= minDate)
            {
                Errors.Add(ValidationConstants.ValidateMaxAndMinLength("Fecha de factura", minDate, currentDate));
            }

            if (Errors.Any())
                return Result.Failure<Receipt>(GetErrors());

            return Result.Success(new Receipt(
                userId,
                identification,
                invoiceDate,
                investorName,
                email,
                false,
                userInSession,
                DateTime.Now,
                Status.Active));
        }

        /// <summary>
        /// Registra una factura dentro de este recibo
        /// </summary>
        /// <param name="listInvoice"></param>
        /// <param name="userInSession"></param>
        /// <returns></returns>
        public Result RegisterInvoice(List<Invoice> listInvoice, Guid userInSession)
        {
            IsSync = true;
            _invoices.AddRange(listInvoice);
            Update(userInSession);
            return Result.Success();
        }

        public Result SyncReceipt(Guid userInSession)
        {
            IsSync = true;
            Update(userInSession);
            return Result.Success();
        }

        public Result ReSyncReceipt(Guid userInSession)
        {
            IsSync = false;
            Update(userInSession);
            RaiseDomainEvent(new RegisterOrReSyncReceiptEvent(
                UserId,
                $"{User.Name} {User.SurName}",
                User.Email, Identification, Date, userInSession));
            return Result.Success();
        }
    }
}