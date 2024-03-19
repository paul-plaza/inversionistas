using Ardalis.Specification;
using Investors.Kernel.Shared.Catalogs.Domain.Aggregate;
using Investors.Shared.Constants;
using Investors.Shared.Domain.ValueObjects;

namespace Investors.Kernel.Shared.Catalogs.Specifications
{
    public sealed class TotalNightsToAccumulateSpecs : Specification<Catalog, int>, ISingleResultSpecification<Catalog, int>
    {
        public TotalNightsToAccumulateSpecs()
        {
            Query.Include(x =>
                    x.CatalogDetails
                        .Where(cadet => cadet.Status == Status.Active))
                .Where(x => x.Status == Status.Active && x.Id == (int)CatalogsEnum.TotalNightsToAccumulate);

            Query.Select(x => Convert.ToInt32(x.CatalogDetails.First().Value));
            Query.AsNoTracking();
        }
    }
}