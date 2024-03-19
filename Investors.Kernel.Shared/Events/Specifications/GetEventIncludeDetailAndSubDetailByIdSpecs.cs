using Ardalis.Specification;
using Investors.Kernel.Shared.Events.Application.Querys.Admin;
using Investors.Kernel.Shared.Events.Application.Querys.Admin.Events;
using Investors.Kernel.Shared.Events.Domain.Aggregate;
using Investors.Kernel.Shared.Events.Domain.Entities.EventDetails;
using Investors.Shared.Domain.ValueObjects;

namespace Investors.Kernel.Shared.Events.Specifications
{
    /// <summary>
    /// Obtengo un evento con un detalle y un subdetalle por id
    /// </summary>
    internal sealed class GetEventIncludeDetailAndSubDetailByIdSpecs : Specification<Event, Event>, ISingleResultSpecification<Event>
    {
        public GetEventIncludeDetailAndSubDetailByIdSpecs(int eventId, int eventDetailId, int subDetailId)
        {
            Query
                .Include(x =>
                    x.EventDetails
                        .Where(y => y.Status == Status.Active && y.Id == eventDetailId))
                .ThenInclude(x => x.EventSubDetails.Where(y => y.Id == subDetailId))
                .Where(x => x.Status == Status.Active && x.Id == eventId)
                .AsSplitQuery();
        }
    }
}