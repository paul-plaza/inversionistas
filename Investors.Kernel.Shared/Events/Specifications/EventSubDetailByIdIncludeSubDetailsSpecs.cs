using Ardalis.Specification;
using Investors.Kernel.Shared.Events.Domain.Entities.EventDetails;
using Investors.Shared.Domain.ValueObjects;

namespace Investors.Kernel.Shared.Events.Specifications
{
    internal sealed class EventSubDetailByIdIncludeSubDetailsSpecs : Specification<EventDetail>,
        ISingleResultSpecification<EventDetail>
    {
        public EventSubDetailByIdIncludeSubDetailsSpecs(int eventDetailId, bool isNoTracking = false)
        {
            Query.Where(x => x.Id == eventDetailId && x.Status == Status.Active);
            Query.Include(x => x.EventSubDetails);

            if (isNoTracking)
            {
                Query.AsNoTracking();
            }
        }
    }
}