using Investors.Client.Shared.Infrastructure;
using Investors.Client.Users.Domain.ValueObjects;
using Investors.Client.Users.Specifications;
using MediatR;

namespace Investors.Client.Users.Application.Querys.HistoryCashback
{

    public record MovementsCashbackQuery(int OperationId, int RestaurantId, TransactionState TransactionState, DateTime Date) : IRequest<Result<List<MovementsCashbackResponse>>>;

    public class MovementsCashbackHandler : IRequestHandler<MovementsCashbackQuery, Result<List<MovementsCashbackResponse>>>
    {
        private readonly ISender _sender;
        private readonly IRepositoryClientForReadManager _repositoryClient;
        public MovementsCashbackHandler(IRepositoryClientForReadManager repositoryClient, ISender sender)
        {
            _repositoryClient = repositoryClient;
            _sender = sender;
        }

        public async Task<Result<List<MovementsCashbackResponse>>> Handle(MovementsCashbackQuery request, CancellationToken cancellationToken)
        {
            var users = await _repositoryClient
                .Users.ListAsync(new MovementsCashbackSpecs(
                        request.OperationId,
                        request.RestaurantId,
                        request.TransactionState,
                        request.Date),
                    cancellationToken);

            return Result.Success(users.SelectMany(x => x.Movements).ToList());
        }
    }
}