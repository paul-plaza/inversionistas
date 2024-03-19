using Investors.Client.Users.Domain.Entities.Transactions;
using Investors.Client.Users.Domain.ValueObjects;
using Investors.Shared.Domain.Events;

namespace Investors.Client.Users.Domain.Events.UserEvents
{
    internal class NightsRedeemedEvent : BaseDomainEvent, IDomainTransactionEvent
    {
        public Movement Movement { get; }
        public string UserName { get; }
        public Guid Id { get; }
        TransactionState TransactionState { get; }
        public NightsRedeemedEvent(
            Guid id,
            string userName,
            TransactionState transactionState,
            Movement movement,
            Guid userInSession)
            : base("Investors.Client.Users.Domain.Events.UserEvents", userInSession)
        {
            Id = id;
            Movement = movement;
            UserName = userName;
            TransactionState = transactionState;
        }

        public string NameEvent => string.Format("Redención de noches ", TransactionState.ToString());
        public string State => TransactionState.ToString();
    }
}
