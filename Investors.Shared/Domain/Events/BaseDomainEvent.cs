namespace Investors.Shared.Domain.Events
{
    public abstract class BaseDomainEvent
    {
        public BaseDomainEvent(string nameSpace, Guid userInSession)
        {
            NameSpace = nameSpace;
            UserInSession = userInSession;
            Meta = new MetaDataEvent();
        }

        public MetaDataEvent Meta { get;}
        public DateTimeOffset DateOcurred { get; protected set; } = DateTime.UtcNow;
        public string NameSpace { get; protected set; }
        public Guid UserInSession { get; protected set; }
    }
}
