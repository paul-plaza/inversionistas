using Ardalis.Specification;
using Investors.Kernel.Shared.Events.Application.Querys.Admin;
using Investors.Kernel.Shared.Events.Application.Querys.Admin.Events;
using Investors.Kernel.Shared.Events.Domain.Aggregate;
using Investors.Shared.Domain.ValueObjects;

namespace Investors.Kernel.Shared.Events.Specifications
{
    internal sealed class GetEventsSpecs : Specification<Event, EventForAdminResponse>
    {
        public GetEventsSpecs()
        {
            Query
                .Select(x => new EventForAdminResponse(
                    x.Id,
                    x.Description,
                    x.Order,
                    x.ItemType.ToString()
                    ))
                .Where(x => x.Status == Status.Active)
                .AsSplitQuery();
        }
    }
}