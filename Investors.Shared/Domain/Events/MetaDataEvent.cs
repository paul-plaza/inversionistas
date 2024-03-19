using System;
namespace Investors.Shared.Domain.Events
{
    public class MetaDataEvent
    {
        public MetaDataEvent()
        {
            Host = Environment.MachineName;
            CorrelationId = Guid.NewGuid().ToString();
        }
        public string Host { get; }
        public string CorrelationId { get; }
    }
}

