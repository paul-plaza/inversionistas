using Ardalis.Specification;
using Investors.Kernel.Shared.Investors.Domain.Aggregate;
using Investors.Shared.Domain.ValueObjects;

namespace Investors.Kernel.Shared.Investors.Specifications
{
    internal sealed class AllInvestorsSpecs : Specification<Investor>
    {
        public AllInvestorsSpecs()
        {
            Query
                .Include(x => x.InvestorOperations)
                .ThenInclude(x => x.Operation);
        }
    }
}