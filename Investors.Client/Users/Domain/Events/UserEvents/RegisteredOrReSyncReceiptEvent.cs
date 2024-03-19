using Investors.Shared.Domain.Events;

namespace Investors.Client.Users.Domain.Events.UserEvents
{
    public class RegisterOrReSyncReceiptEvent : BaseDomainEvent, IDomainTransactionEvent
    {
        public Guid UserId { get; }
        public string UserName { get; }

        public string Email { get; }

        public string Identification { get; }

        public DateOnly DateInvoice { get; }

        public RegisterOrReSyncReceiptEvent(
            Guid userId,
            string userName,
            string email,
            string identification,
            DateOnly dateInvoice,
            Guid userInSession)
            : base("Investors.Client.Users.Domain.Events.ReceiptEvents", userInSession)
        {
            UserName = userName;
            Email = email;
            Identification = identification;
            DateInvoice = dateInvoice;
            UserId = userId;
        }

        public const string NameEvent = "Registro o re-sincronización de factura";
    }
}