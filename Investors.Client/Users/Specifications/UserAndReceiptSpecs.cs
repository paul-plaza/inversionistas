using Ardalis.Specification;
using Investors.Client.Users.Domain.Aggregate;
using Investors.Client.Users.Domain.ValueObjects;

namespace Investors.Client.Users.Specifications
{
    internal sealed class UserAndReceiptSpecs : Specification<User>
    {
        public UserAndReceiptSpecs()
        {
            Query
                .Include(x => x.Receipts.Where(y => !y.IsSync))
                .ThenInclude(y => y.Invoices)
                .Include(x => x.Profile)
                .AsSplitQuery()
                .Where(x =>
                    x.Status == Status.Active
                    && x.UserType == UserType.Investor
                    && x.Receipts.Any());
        }
    }
}