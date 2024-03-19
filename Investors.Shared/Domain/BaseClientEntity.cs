using System;
using Investors.Shared.Domain.Exceptions;
using Investors.Shared.Domain.ValueObjects;

namespace Investors.Shared.Domain
{
    public abstract class BaseClientEntity<T> : BaseEntity<T> where T : struct
    {

        protected BaseClientEntity()
        {

        }
        protected BaseClientEntity(
            int clientId,
            Guid createdBy,
            DateTime createdOn,
            Status status)
            : base(createdBy, createdOn, status)
        {

            if (clientId == 0)
            {
                throw new ClientNullException(nameof(clientId));
            }

            ClientId = clientId;
        }
        public int ClientId { get; protected set; }
    }
}
