using Ardalis.Specification;
using Investors.Kernel.Shared.Events.Application.Querys.ShowEvents.Response;
using Investors.Kernel.Shared.Events.Domain.Aggregate;
using Investors.Kernel.Shared.Events.Domain.Entities.EventDetails;
using Investors.Kernel.Shared.Events.Domain.Entities.EventSubDetails;
using Investors.Shared.Domain.ValueObjects;

namespace Investors.Kernel.Shared.Events.Specifications
{
    internal sealed class EventsSpecs : Specification<Event, EventResponse>
    {
        public EventsSpecs(bool isNoTracking = false)
        {
            Query.Select(x => new EventResponse(
                x.Id,
                x.Description,
                x.Order,
                x.ItemType.ToString(),
                x.EventDetails
                    .Where(y => y.Status == Status.Active)
                    .Select(eventDetail => new EventDetailResponse(
                        eventDetail.Id,
                        eventDetail.OperationId,
                        eventDetail.Description,
                        eventDetail.Title,
                        eventDetail.UrlLogo,
                        eventDetail.UrlToOpen,
                        eventDetail.CreatedOn,
                        eventDetail
                            .EventSubDetails
                            .Where(y => y.Status == Status.Active)
                            .Select(profitDetail => new EventSubDetailResponse(
                                profitDetail.Id,
                                profitDetail.Title,
                                profitDetail.Description,
                                profitDetail.Image
                                )).ToList()
                        ))
                    .ToList()
                ));

            if (isNoTracking)
            {
                Query.AsNoTracking();
            }

            Query
                .Where(x => x.Status == Status.Active)
                .AsSplitQuery();
        }
    }
}