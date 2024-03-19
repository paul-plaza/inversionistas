using Ardalis.Specification;
using Investors.Client.Users.Domain.Entities.Transactions;
using Investors.Client.Users.Domain.ValueObjects;

namespace Investors.Client.Users.Specifications
{
    internal sealed class CashbackHistoryDetailByIdSpecs : Specification<Movement>, ISingleResultSpecification<Movement>
    {
        public CashbackHistoryDetailByIdSpecs(Guid movementId)
        {
            Query.Include(x => x.CashbackDetails);

            Query.Where(x => x.Id == movementId && x.TransactionType == TransactionType.Cashback);

            Query.AsNoTracking();
        }
    }
}