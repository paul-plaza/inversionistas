using Ardalis.Specification;
using Investors.Kernel.Shared.Investors.Domain.Aggregate;
using Investors.Shared.Domain.ValueObjects;

namespace Investors.Kernel.Shared.Investors.Specifications
{
    internal sealed class AllInvestorsActiveSpecs : Specification<Investor>
    {
        public AllInvestorsActiveSpecs()
        {
            Query
                .Where(x => x.Status == Status.Active)
                .Include(x => x.InvestorOperations.Where(y => y.Status == Status.Active))
                .ThenInclude(x => x.Operation);

            Query.AsNoTracking();
        }
    }
}