using Investors.Shared.Domain.Events;

namespace Investors.Client.Users.Domain.Events.ProfileEvents
{
    internal class NightsAccumulatedEvent : BaseDomainEvent, IDomainTransactionEvent
    {
        public int NumberNights { get; }
        public string InvoiceDocument { get; }
        public Guid UserId { get; }
        public int OperationId { get; }

        public NightsAccumulatedEvent(
            Guid userId,
            int numberNights,
            string invoiceDocument,
            int operationId,
            Guid userInSession)
            : base("Investors.Client.Users.Domain.Events.ProfileEvents", userInSession)
        {
            UserId = userId;
            NumberNights = numberNights;
            InvoiceDocument = invoiceDocument;
            OperationId = operationId;
        }

        public string NameEvent => "Registro de noches acumuladas";
    }
}