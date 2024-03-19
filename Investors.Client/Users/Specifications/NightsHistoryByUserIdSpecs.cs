using Ardalis.Specification;
using Investors.Client.Users.Application.Querys.HistoryCashback;
using Investors.Client.Users.Application.Querys.HistoryNights;
using Investors.Client.Users.Domain.Entities.Transactions;
using Investors.Client.Users.Domain.ValueObjects;

namespace Investors.Client.Users.Specifications
{
    internal sealed class NightsHistoryByUserIdSpecs : Specification<Movement, HistoryNightsResponse>
    {
        public NightsHistoryByUserIdSpecs(Guid userId)
        {
            Query.Where(x => x.UserId == userId && x.TransactionType == TransactionType.Nights);

            Query.Include(x => x.NightsDetail);

            Query.OrderByDescending(x => x.CreatedOn);

            Query.Select(x => new HistoryNightsResponse
            {
                Id = x.Id,
                RequestDate = x.CreatedOn,
                OperationId = x.OperationId,
                Status = x.TransactionState,
                CheckIn = x.NightsDetail.DateStart.ToDateTime(new TimeOnly()),
                CheckOut = x.NightsDetail.DateEnd.ToDateTime(new TimeOnly()),
                TotalDays = x.TotalToRedeem,
            });

            Query.AsNoTracking();

            Query.Take(30);
        }
    }
}