using Ardalis.Specification;
using Investors.Client.Users.Application.Querys.HistoryCashback;
using Investors.Client.Users.Domain.Entities.Transactions;
using Investors.Client.Users.Domain.ValueObjects;

namespace Investors.Client.Users.Specifications
{
    internal sealed class CashbackHistoryByUserIdSpecs : Specification<Movement, HistoryCashbackResponse>
    {
        public CashbackHistoryByUserIdSpecs(Guid userId)
        {
            Query.Where(x => x.UserId == userId && x.TransactionType == TransactionType.Cashback);

            Query.OrderByDescending(x => x.CreatedOn);

            Query.Select(x => new HistoryCashbackResponse
            {
                Id = x.Id,
                Date = x.CreatedOn,
                OperationId = x.OperationId,
                Amount = x.TotalToRedeem,
                TotalItems = x.TotalItems ?? 0,
                Status = x.TransactionState,
                RestaurantId = x.RestaurantId ?? 0,
            });

            Query.AsNoTracking();

            Query.Take(30);
        }
    }
}