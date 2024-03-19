using Ardalis.Specification;
using Investors.Kernel.Shared.Events.Domain.Aggregate;
using Investors.Shared.Domain.ValueObjects;

namespace Investors.Kernel.Shared.Events.Specifications
{
    internal sealed class EventsDetailIncludeEventSubDetailByIdEventDetailSpecs : Specification<Event, Event>, ISingleResultSpecification<Event>
    {
        public EventsDetailIncludeEventSubDetailByIdEventDetailSpecs(int eventId, bool isNoTracking = false)
        {
            if (isNoTracking)
            {
                Query.AsNoTracking();
            }

            Query
                .Include(x => x.EventDetails.Where(y => y.Status == Status.Active))
                .ThenInclude(x => x.EventSubDetails.Where(y => y.Status == Status.Active))
                .Where(x => x.Id == eventId && x.Status == Status.Active)
                .AsSplitQuery();
        }
    }
}