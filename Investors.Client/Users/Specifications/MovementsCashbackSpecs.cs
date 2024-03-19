using Ardalis.Specification;
using Investors.Client.Users.Application.Querys.HistoryCashback;
using Investors.Client.Users.Domain.Aggregate;
using Investors.Client.Users.Domain.ValueObjects;
using System.Linq;

namespace Investors.Client.Users.Specifications
{
    internal sealed class MovementsCashbackSpecs : Specification<User, UserIncludeMovementsResponse>
    {
        public MovementsCashbackSpecs(int operationId, int restaurantId, TransactionState transactionState, DateTime date)
        {
            Query.Where(x => x.Status == Status.Active &&
                             x.UserType == UserType.Investor &&
                             x.Movements.Any(y => y.OperationId == operationId &&
                                                  y.RestaurantId == restaurantId &&
                                                  y.TransactionType == TransactionType.Cashback &&
                                                  y.TransactionState == transactionState &&
                                                  y.CreatedOn.Date == date.Date));

            Query.Select(user => new UserIncludeMovementsResponse(
                user.Movements
                    .Where(y => y.OperationId == operationId
                                && y.RestaurantId == restaurantId
                                && y.TransactionType == TransactionType.Cashback
                                && y.TransactionState == transactionState
                                && y.CreatedOn.Date == date.Date)
                    .Select(x => new MovementsCashbackResponse(
                        x.Id,
                        x.CreatedOn,
                        x.TransactionState.ToString(),
                        x.TransactionState.Value,
                        x.TotalToRedeem,
                        x.TotalItems,
                        user.Identification,
                        user.Id.ToString(),
                        user.DisplayName,
                        user.Email
                        ))
                    .ToList()
                ));

            Query.AsNoTracking();

            Query.AsSplitQuery();
        }
    }
}