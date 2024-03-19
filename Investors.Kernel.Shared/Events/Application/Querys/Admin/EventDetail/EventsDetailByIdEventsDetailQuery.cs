using CSharpFunctionalExtensions;
using Investors.Kernel.Shared.Events.Application.Querys.ShowEvents.Response;
using Investors.Kernel.Shared.Events.Specifications;
using Investors.Kernel.Shared.Shared.Infrastructure;
using MediatR;

namespace Investors.Kernel.Shared.Events.Application.Querys.Admin.EventDetail
{
    public record EventsDetailByIdEventsDetailQuery(int EventId) : IRequest<Result<IReadOnlyCollection<EventDetailResponse>>>;

    public class EventsDetailByIdEventsDetailHandler : IRequestHandler<EventsDetailByIdEventsDetailQuery, Result<IReadOnlyCollection<EventDetailResponse>>>
    {
        private readonly IRepositorySharedForReadManager _repositoryClient;

        public EventsDetailByIdEventsDetailHandler(IRepositorySharedForReadManager repositoryClient)
        {
            _repositoryClient = repositoryClient;
        }

        public async Task<Result<IReadOnlyCollection<EventDetailResponse>>> Handle(EventsDetailByIdEventsDetailQuery request, CancellationToken cancellationToken)
        {
            var eventGroup = await _repositoryClient
                .Events
                .SingleOrDefaultAsync(new EventsDetailIncludeEventSubDetailByIdEventDetailSpecs(request.EventId), cancellationToken);

            if (eventGroup is null)
            {
                return Result.Failure<IReadOnlyCollection<EventDetailResponse>>("No existen eventos registrados");
            }

            return Result.Success<IReadOnlyCollection<EventDetailResponse>>(
                eventGroup.EventDetails.Select(eventDetail => new EventDetailResponse(
                    eventDetail.Id,
                    eventDetail.OperationId,
                    eventDetail.Description,
                    eventDetail.Title,
                    eventDetail.UrlLogo,
                    eventDetail.UrlToOpen,
                    eventDetail.CreatedOn,
                    eventDetail
                        .EventSubDetails
                        .Select(profitDetail => new EventSubDetailResponse(
                            profitDetail.Id,
                            profitDetail.Title,
                            profitDetail.Description,
                            profitDetail.Image
                            )).ToList()
                    )).ToList());
        }
    }
}