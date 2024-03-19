using Ardalis.Specification;
using Investors.Kernel.Shared.Investors.Domain.Aggregate;
using Investors.Shared.Domain.ValueObjects;

namespace Investors.Kernel.Shared.Investors.Specifications
{
    internal sealed class InvestorByIdentificationSpecs : Specification<Investor>, ISingleResultSpecification<Investor>
    {
        public InvestorByIdentificationSpecs(string identification)
        {
            Query.Where(x => x.Identification == identification && x.Status == Status.Active);
            Query.Include(x => x.InvestorOperations)
                .Take(1);
        }
    }
}