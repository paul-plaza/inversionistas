using Ardalis.Specification;
using Investors.Kernel.Shared.Catalogs.Domain.Aggregate;
using Investors.Shared.Constants;
using Investors.Shared.Domain.ValueObjects;

namespace Investors.Kernel.Shared.Catalogs.Specifications
{
    internal sealed class AllCategoryByIdSpecs : Specification<Catalog>, ISingleResultSpecification<Catalog>
    {
        public AllCategoryByIdSpecs()
        {
            Query
                .Include(x => x.Categories
                    .Where(cat => cat.Status == Status.Active));

            Query
                .Where(x => x.Id == (int)CatalogsEnum.Categories);

            Query
                .Where(x => x.Status == Status.Active)
                .Take(1);
        }
    }
}