using Investors.Client.Shared.Infrastructure;
using Investors.Client.Users.Specifications;
using Investors.Kernel.Shared.Operations.Application.Querys.Operation;
using MediatR;

namespace Investors.Client.Users.Application.Querys.HistoryNights
{

    public record HistoryNightsByUserIdQuery(Guid UserId) : IRequest<Result<List<HistoryNightsResponse>>>;

    public class HistoryNightsByUserIdHandler : IRequestHandler<HistoryNightsByUserIdQuery, Result<List<HistoryNightsResponse>>>
    {
        private readonly ISender _sender;
        private readonly IRepositoryClientForReadManager _repositoryClient;
        public HistoryNightsByUserIdHandler(IRepositoryClientForReadManager repositoryClient, ISender sender)
        {
            _repositoryClient = repositoryClient;
            _sender = sender;
        }

        public async Task<Result<List<HistoryNightsResponse>>> Handle(HistoryNightsByUserIdQuery request, CancellationToken cancellationToken)
        {
            var historyCashback = await _repositoryClient.Movements.ListAsync(new NightsHistoryByUserIdSpecs(request.UserId), cancellationToken);

            var operations = await _sender.Send(new GetOperationsQuery(), cancellationToken);

            if (operations.IsFailure)
            {
                return Result.Failure<List<HistoryNightsResponse>>(operations.Error);
            }

            var response = (from history in historyCashback
                join operation in operations.Value on history.OperationId equals operation.Id
                select history with
                {
                    Image = operation.UrlLogo,
                    OperationName = operation.Description,
                }).ToList();

            return Result.Success(response);
        }
    }
}