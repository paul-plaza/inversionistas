namespace Investors.Shared.Domain.Events
{
    public interface IDomainTaskEvent : IDomainEvent
    {
        string NameEvent { get; }
    }
}