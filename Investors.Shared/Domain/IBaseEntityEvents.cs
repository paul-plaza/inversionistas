using System;
using Investors.Shared.Domain.Events;

namespace Investors.Shared.Domain
{
    public interface IBaseEntityEvents
    {
        IReadOnlyList<IDomainEvent> DomainEvents { get; }
        void ClearDomainTransactionEvents();

        void ClearDomainTaskEvents();
    }
}