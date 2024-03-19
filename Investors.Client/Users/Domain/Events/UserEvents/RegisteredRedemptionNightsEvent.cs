using Investors.Shared.Domain.Events;

namespace Investors.Client.Users.Domain.Events.UserEvents
{
    internal class RegisteredRedemptionNightsEvent : BaseDomainEvent, IDomainTransactionEvent
    {
        public Guid MovementId { get; }
        public string UserName { get; }
        public Guid Id { get; }
        public string Email { get; }
        public string Identification { get; }
        public int OperationId { get; }
        public int RoomId { get; }
        public DateTime DateStart { get; }
        public DateTime DateEnd { get; }
        public RegisteredRedemptionNightsEvent(
            Guid id,
            string email,
            string identification,
            int operationId,
            int roomId,
            DateTime dateStart,
            DateTime dateEnd,
            Guid userInSession,
            string userName,
            Guid movementId)
            : base("Investors.Client.Users.Domain.Events.UserEvents", userInSession)
        {
            Id = id;
            Email = email;
            Identification = identification;
            OperationId = operationId;
            RoomId= roomId;
            DateStart = dateStart;
            DateEnd= dateEnd;
            UserName = userName;
            MovementId = movementId;
        }

        public string NameEvent => "Creación de orden de habitación";
    }
}
