using Investors.Client.Users.Domain.Entities.Transactions;
using Investors.Client.Users.Domain.ValueObjects;
using Investors.Shared.Domain.Events;

namespace Investors.Client.Users.Domain.Events.UserEvents
{
    internal class CashbackRedeemedEvent : BaseDomainEvent, IDomainTransactionEvent
    {
        public Movement Movement { get; }
        public string UserName { get; }
        public Guid Id { get; }
        TransactionState TransactionState { get; }
        public CashbackRedeemedEvent(
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

        public string NameEvent => string.Format("Redención de cashback ", TransactionState.ToString());
        public string State => TransactionState.ToString();
    }
}
