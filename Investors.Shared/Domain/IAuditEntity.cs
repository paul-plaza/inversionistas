using System;
using System.Net.NetworkInformation;
using Investors.Shared.Domain.ValueObjects;

namespace Investors.Shared.Domain
{
    public interface IAuditEntity
    {
        Guid CreatedBy { get; }
        DateTime CreatedOn { get; }
        Guid? UpdatedBy { get; }
        DateTime? UpdatedOn { get; }
        Status Status { get; }
    }
}
