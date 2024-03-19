using Ardalis.Specification;
using Investors.Kernel.Shared.Events.Application.Querys.Admin;
using Investors.Kernel.Shared.Events.Application.Querys.Admin.Events;
using Investors.Kernel.Shared.Events.Domain.Aggregate;
using Investors.Kernel.Shared.Events.Domain.Entities.EventDetails;
using Investors.Shared.Domain.ValueObjects;

namespace Investors.Kernel.Shared.Events.Specifications
{
    internal sealed class GetEventIncludeDetailByIdSpecs : Specification<Event, Event>, ISingleResultSpecification<Event>
    {
        public GetEventIncludeDetailByIdSpecs(int eventId, int eventDetailId)
        {
            Query
                .Include(x => x.EventDetails.Where(y => y.Status == Status.Active && y.Id == eventDetailId))
                .Where(x => x.Status == Status.Active && x.Id == eventId)
                .AsSplitQuery();
        }
    }
}