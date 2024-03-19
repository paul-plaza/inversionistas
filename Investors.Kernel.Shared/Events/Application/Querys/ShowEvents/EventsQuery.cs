using CSharpFunctionalExtensions;
using Investors.Kernel.Shared.Events.Application.Querys.Admin;
using Investors.Kernel.Shared.Events.Application.Querys.ShowEvents.Response;
using Investors.Kernel.Shared.Events.Specifications;
using Investors.Kernel.Shared.Investors.Application.Querys;
using Investors.Kernel.Shared.Shared.Infrastructure;
using MediatR;

namespace Investors.Kernel.Shared.Events.Application.Querys.ShowEvents
{
    public record EventsQuery(string InvestorId) : IRequest<Result<IReadOnlyCollection<EventResponse>>>;

    public class EventsHandler : IRequestHandler<EventsQuery, Result<IReadOnlyCollection<EventResponse>>>
    {
        private readonly IRepositorySharedForReadManager _repositoryClient;
        private readonly ISender _sender;

        public EventsHandler(IRepositorySharedForReadManager repositoryClient, ISender sender)
        {
            _repositoryClient = repositoryClient;
            _sender = sender;
        }

        public async Task<Result<IReadOnlyCollection<EventResponse>>> Handle(EventsQuery request, CancellationToken cancellationToken)
        {
            var events = await _repositoryClient.Events.ListAsync(new EventsSpecs(true), cancellationToken);
            if (!events.Any())
            {
                return Result.Failure<IReadOnlyCollection<EventResponse>>("No existen eventos registrados");
            }

            var investorOperations = await _sender.Send(new GetInvestorByIdentificationQuery(request.InvestorId), cancellationToken);

            //obtengo los eventos globales
            foreach (var x in events)
            {
                for (int i = 0; i < x.Children.Count; i++)
                {
                    var child = x.Children[i];

                    if (child.OperationId != 0)
                    {
                        if (investorOperations.IsSuccess)
                        {
                            if (!investorOperations.Value.Operations.Select(op => op.OperationId).Contains(child.OperationId))
                            {
                                x.Children.RemoveAt(i);
                                i--; // retroceder el índice
                            }
                        }
                        else
                        {
                            x.Children.RemoveAt(i);
                            i--; // retroceder el índice
                        }
                    }
                }
            }
            return Result.Success<IReadOnlyCollection<EventResponse>>(events);
        }
    }
}