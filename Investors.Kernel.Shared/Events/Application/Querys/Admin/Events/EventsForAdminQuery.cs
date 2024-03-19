using CSharpFunctionalExtensions;
using Investors.Kernel.Shared.Events.Specifications;
using Investors.Kernel.Shared.Shared.Infrastructure;
using MediatR;

namespace Investors.Kernel.Shared.Events.Application.Querys.Admin.Events
{
    public record EventsForAdminQuery : IRequest<Result<IReadOnlyCollection<EventForAdminResponse>>>;

    public class EventsAdminHandler : IRequestHandler<EventsForAdminQuery, Result<IReadOnlyCollection<EventForAdminResponse>>>
    {
        private readonly IRepositorySharedForReadManager _repositoryClient;

        public EventsAdminHandler(IRepositorySharedForReadManager repositoryClient)
        {
            _repositoryClient = repositoryClient;
        }

        public async Task<Result<IReadOnlyCollection<EventForAdminResponse>>> Handle(EventsForAdminQuery request, CancellationToken cancellationToken)
        {
            var events = await _repositoryClient.Events.ListAsync(new GetEventsSpecs(), cancellationToken);

            if (!events.Any())
            {
                return Result.Failure<IReadOnlyCollection<EventForAdminResponse>>("No existen eventos registrados");
            }
            return Result.Success<IReadOnlyCollection<EventForAdminResponse>>(events);
        }
    }
}