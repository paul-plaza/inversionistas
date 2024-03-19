using System;
using Investors.Shared.Domain.Events;
using Investors.Shared.Domain.Exceptions;
using Investors.Shared.Domain.ValueObjects;

namespace Investors.Shared.Domain
{
    public abstract class BaseEntity<T> : IAuditEntity, IBaseEntityEvents where T : struct
    {
        private readonly List<IDomainEvent> _domainEvents = new();

        protected BaseEntity()
        {

        }

        protected BaseEntity(
            Guid createdBy,
            DateTime createdOn,
            Status status) : this()
        {
            CreatedBy = createdBy;
            CreatedOn = createdOn;
            Status = status;
        }

        public T Id { get; protected set; }

        protected static List<string> Errors { get; set; } = new();

        public Guid CreatedBy { get; protected set; }
        public DateTime CreatedOn { get; protected set; }
        public Guid? UpdatedBy { get; protected set; }
        public DateTime? UpdatedOn { get; protected set; }
        public Status Status { get; protected set; }
        public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents;
        public void ClearDomainTransactionEvents()
        {
            //elimino los eventos de dominio que son de transaccion
            _domainEvents.RemoveAll(x => x is IDomainTransactionEvent);
        }
        public void ClearDomainTaskEvents()
        {
            //elimino los eventos de dominio que son de tarea
            _domainEvents.RemoveAll(x => x is IDomainTaskEvent);
        }

        protected void RaiseDomainEvent(IDomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }

        protected virtual void Update(Guid userInSession)
        {
            if (userInSession == Guid.Empty)
            {
                throw new NotUserInSessionException(nameof(userInSession));
            }

            Status = Status.Active;
            UpdatedBy = userInSession;
            UpdatedOn = DateTime.Now;
        }

        protected virtual void Delete(Guid userInSession)
        {
            if (userInSession == Guid.Empty)
            {
                throw new NotUserInSessionException(nameof(userInSession));
            }
            Status = Status.Deleted;
            UpdatedBy = userInSession;
            UpdatedOn = DateTime.Now;
        }

        protected virtual void InActivate(Guid userInSession)
        {
            if (userInSession == Guid.Empty)
            {
                throw new NotUserInSessionException(nameof(userInSession));
            }
            Status = Status.Inactive;
            UpdatedBy = userInSession;
            UpdatedOn = DateTime.Now;
        }

        public override bool Equals(object? obj)
        {
            var other = obj as BaseEntity<T>;

            if (ReferenceEquals(other, null))
                return false;

            if (ReferenceEquals(this, other))
                return true;

            if (Id.Equals(default(T)) || other.Id.Equals(default(T)))
                return false;

            return EqualityComparer<T>.Default.Equals(Id, other.Id);
        }

        public static bool operator ==(BaseEntity<T>? a, BaseEntity<T>? b)
        {
            if (a is null && b is null)
                return true;

            if (a is null || b is null)
                return false;

            return a.Equals(b);
        }

        public static bool operator !=(BaseEntity<T> a, BaseEntity<T> b)
        {
            return !(a == b);
        }

        public override int GetHashCode()
        {
            return (GetType().ToString() + Id.GetHashCode()).GetHashCode();
        }

        protected static string GetErrors()
        {
            string error = string.Join(Environment.NewLine, Errors);
            Errors.Clear();
            return error;
        }
    }
}