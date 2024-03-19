using Investors.Kernel.Shared.Operations.Domain.Entities.Menus;
using Investors.Shared.Domain.Events;

namespace Investors.Client.Users.Domain.Events.UserEvents
{
    internal class RegisteredRedemptionCashbackEvent : BaseDomainEvent, IDomainTransactionEvent
    {
        public Guid MovementId { get; }
        public string UserName { get;}
        public string Identification { get; }
        public Guid Id { get; }
        public string Email { get; }
        public int OperationId { get; }
        public IEnumerable<MenuResponse> Menu { get; }
        public int RestaurantId { get; }
        public RegisteredRedemptionCashbackEvent(
            Guid id,
            string email,
            string identification,
            int operationId,
            IEnumerable<MenuResponse> menu,
            int restaurantId, 
            Guid userInSession,
            string userName,
            Guid movementId)
            : base("Investors.Client.Users.Domain.Events.UserEvents", userInSession)
        {
            Id = id;
            Email = email;
            Identification = identification;
            OperationId = operationId;
            Menu = menu;
            RestaurantId = restaurantId;
            UserName = userName;
            MovementId = movementId;
        }

        public string NameEvent => "Creación de orden para cashback";
    }
}
