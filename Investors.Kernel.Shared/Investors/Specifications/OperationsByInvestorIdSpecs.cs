using Ardalis.Specification;
using Investors.Kernel.Shared.Investors.Domain.Aggregate;

namespace Investors.Kernel.Shared.Investors.Specifications
{
    internal sealed class OperationsByInvestorIdSpecs : Specification<Investor>, ISingleResultSpecification<Investor>
    {
        public OperationsByInvestorIdSpecs(string identification)
        {
            Query.Include(x => x.InvestorOperations);
            Query.Where(x => x.Identification == identification)
                .Take(1);
        }
    }
}