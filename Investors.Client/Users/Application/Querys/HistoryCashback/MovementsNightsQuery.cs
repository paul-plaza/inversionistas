using Investors.Client.Shared.Infrastructure;
using Investors.Client.Users.Domain.ValueObjects;
using Investors.Client.Users.Specifications;
using MediatR;

namespace Investors.Client.Users.Application.Querys.HistoryCashback
{

    public record MovementsNightsQuery(int OperationId, TransactionState TransactionState, DateTime Date) : IRequest<Result<List<MovementsNightsResponse>>>;

    public class MovementsNightsHandler : IRequestHandler<MovementsNightsQuery, Result<List<MovementsNightsResponse>>>
    {
        private readonly ISender _sender;
        private readonly IRepositoryClientForReadManager _repositoryClient;
        public MovementsNightsHandler(IRepositoryClientForReadManager repositoryClient, ISender sender)
        {
            _repositoryClient = repositoryClient;
            _sender = sender;
        }

        public async Task<Result<List<MovementsNightsResponse>>> Handle(MovementsNightsQuery request, CancellationToken cancellationToken)
        {
            var users = await _repositoryClient.Users.ListAsync(new MovementsNightsSpecs(request.OperationId, request.TransactionState, request.Date), cancellationToken);

            if (!users.Any())
            {
                return Result.Failure<List<MovementsNightsResponse>>("movimientos no encontrados");
            }

            return Result.Success(users.SelectMany(x => x.Movements).ToList());
        }
    }
}