using Investors.Client.Shared.Infrastructure;
using Investors.Client.Users.Specifications;
using Investors.Kernel.Shared.Operations.Application.Querys.Operation.OperationsAndRestaurantsNames;
using MediatR;

namespace Investors.Client.Users.Application.Querys.HistoryCashback
{

    public record HistoryCashbackByUserIdQuery(Guid UserId) : IRequest<Result<List<HistoryCashbackResponse>>>;

    public class HistoryCashbackByUserIdHandler : IRequestHandler<HistoryCashbackByUserIdQuery, Result<List<HistoryCashbackResponse>>>
    {
        private readonly ISender _sender;
        private readonly IRepositoryClientForReadManager _repositoryClient;
        public HistoryCashbackByUserIdHandler(IRepositoryClientForReadManager repositoryClient, ISender sender)
        {
            _repositoryClient = repositoryClient;
            _sender = sender;
        }

        public async Task<Result<List<HistoryCashbackResponse>>> Handle(HistoryCashbackByUserIdQuery request, CancellationToken cancellationToken)
        {
            var historyCashback = await _repositoryClient.Movements.ListAsync(new CashbackHistoryByUserIdSpecs(request.UserId), cancellationToken);

            var operations = await _sender.Send(new OperationsAndRestaurantsNamesQuery(), cancellationToken);

            if (operations.IsFailure)
            {
                return Result.Failure<List<HistoryCashbackResponse>>(operations.Error);
            }

            var response = (from history in historyCashback
                join operation in operations.Value on history.OperationId equals operation.Id
                let restaurant = operation.Restaurants.FirstOrDefault(r => history.RestaurantId == r.Id)
                where restaurant is not null
                select history with
                {
                    Image = restaurant.Image,
                    OperationName = operation.Name,
                    RestaurantName = restaurant.Name

                }).ToList();

            return Result.Success(response);
        }
    }
}