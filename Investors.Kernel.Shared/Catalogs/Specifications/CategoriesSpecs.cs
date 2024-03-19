using Ardalis.Specification;
using Investors.Kernel.Shared.Catalogs.Domain.Aggregate;
using Investors.Shared.Constants;
using Investors.Shared.Domain.ValueObjects;

namespace Investors.Kernel.Shared.Catalogs.Specifications
{
    internal sealed class CategoryByIdSpecs : Specification<Catalog>, ISingleResultSpecification<Catalog>
    {
        public CategoryByIdSpecs(int categoryId)
        {
            Query
                .Include(x => x.Categories
                    .Where(cat => cat.Status == Status.Active && cat.Id == categoryId));

            Query
                .Where(x => x.Id == (int)CatalogsEnum.Categories);

            Query
                .Where(x => x.Status == Status.Active)
                .Take(1);
        }
    }
}