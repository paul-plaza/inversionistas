using Ardalis.Specification;
using Investors.Client.Users.Application.Querys.HistoryCashback;
using Investors.Client.Users.Domain.Aggregate;
using Investors.Client.Users.Domain.ValueObjects;

namespace Investors.Client.Users.Specifications
{
    internal sealed class MovementsNightsSpecs : Specification<User, UserIncludeMovementsNightsResponse>
    {
        public MovementsNightsSpecs(int operationId, TransactionState transactionState, DateTime date)
        {
            Query
                .Include(user => user.Movements)
                .ThenInclude(movement => movement.Room)
                .Include(user => user.Movements)
                .ThenInclude(movement => movement.NightsDetail)
                .Where(x => x.Status == Status.Active &&
                            x.UserType == UserType.Investor &&
                            x.Movements.Any(y => y.OperationId == operationId &&
                                                 y.TransactionType == TransactionType.Nights &&
                                                 y.TransactionState == transactionState &&
                                                 y.CreatedOn.Date == date.Date));

            Query.Select(user => new UserIncludeMovementsNightsResponse(
                user.Movements
                    .Where(y => y.OperationId == operationId
                                && y.TransactionType == TransactionType.Nights
                                && y.TransactionState == transactionState
                                && y.CreatedOn.Date == date.Date)
                    .Select(x => new MovementsNightsResponse(
                        x.Id,
                        user.Id,
                        user.Identification,
                        user.DisplayName,
                        user.Email,
                        x.Room.RoomTypeId,
                        x.Room.Title,
                        x.Room.Description,
                        x.TotalToRedeem,
                        x.NightsDetail.DateStart,
                        x.NightsDetail.DateEnd,
                        x.TransactionState.ToString(),
                        x.TransactionState.Value,
                        x.NightsDetail.Observation
                        ))
                    .ToList()
                ));

            Query.AsNoTracking();

            Query.AsSplitQuery();
        }
    }
}