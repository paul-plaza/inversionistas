using Investors.Shared.Domain.Events;

namespace Investors.Client.Users.Domain.Events.UserEvents
{
    internal class RegisteredUserEvent : BaseDomainEvent, IDomainTransactionEvent
    {
        public Guid Id { get; }
        public string Identification { get; }
        public string Email { get; }
        public string Name { get; }
        public RegisteredUserEvent(
            Guid id,
            string email,
            string name,
            Guid userInSession,
            string identification)
            : base("Investors.Client.Users.Domain.Events.UserEvents", userInSession)
        {
            Id = id;
            Identification = identification;
            Email = email;
            Name = name;
        }

        public string NameEvent => "Usuario Registrado";
    }
}