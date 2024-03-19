using Investors.Shared.Domain.Events;

namespace Investors.Client.Users.Domain.Events.ProfileEvents
{
    internal class CashbackAccumulatedEvent : BaseDomainEvent, IDomainTransactionEvent
    {
        public int TotalPoints { get; }
        public Guid UserId { get; }
        public string InvoiceDocument { get; }
        public int OperationId { get; }
        public CashbackAccumulatedEvent(
            Guid userId,
            int totalPoints,
            string invoiceDocument,
            int operationId,
            Guid userInSession)
            : base("Investors.Client.Users.Domain.Events.ProfileEvents", userInSession)
        {
            UserId = userId;
            TotalPoints = totalPoints;
            InvoiceDocument = invoiceDocument;
            OperationId = operationId;
        }

        public string NameEvent => "Registro de puntos acumulados";
    }
}