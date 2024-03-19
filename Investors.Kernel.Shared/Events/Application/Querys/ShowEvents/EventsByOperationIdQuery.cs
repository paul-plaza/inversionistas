using CSharpFunctionalExtensions;
using Investors.Kernel.Shared.Events.Application.Querys.Admin;
using Investors.Kernel.Shared.Events.Application.Querys.ShowEvents.Response;
using Investors.Kernel.Shared.Events.Specifications;
using Investors.Kernel.Shared.Shared.Infrastructure;
using MediatR;

namespace Investors.Kernel.Shared.Events.Application.Querys.ShowEvents
{
    public record EventsByOperationIdQuery(int OperationId) : IRequest<Result<IReadOnlyCollection<EventResponse>>>;

    public class EventsByOperationIdHandler : IRequestHandler<EventsByOperationIdQuery, Result<IReadOnlyCollection<EventResponse>>>
    {
        private readonly IRepositorySharedForReadManager _repositoryClient;
        private readonly ISender _sender;

        public EventsByOperationIdHandler(IRepositorySharedForReadManager repositoryClient, ISender sender)
        {
            _repositoryClient = repositoryClient;
            _sender = sender;
        }

        public async Task<Result<IReadOnlyCollection<EventResponse>>> Handle(EventsByOperationIdQuery request, CancellationToken cancellationToken)
        {
            var events = await _repositoryClient
                .Events.ListAsync(new EventsByOperationIdSpecs(new List<int>
                {
                    0,
                    request.OperationId
                }, true), cancellationToken);
            if (!events.Any())
            {
                return Result.Failure<IReadOnlyCollection<EventResponse>>("No existen eventos registrados");
            }
            return Result.Success<IReadOnlyCollection<EventResponse>>(events);
        }
    }
}