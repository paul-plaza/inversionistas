using Ardalis.Specification;
using Investors.Kernel.Shared.Catalogs.Domain.Aggregate;
using Investors.Shared.Constants;
using Investors.Shared.Domain.ValueObjects;

namespace Investors.Kernel.Shared.Catalogs.Specifications
{
    internal sealed class UnauthorizedRecordsForPointsSpecs : Specification<Catalog>
    {
        public UnauthorizedRecordsForPointsSpecs()
        {
            Query.Include(x =>
                    x.CatalogDetails
                        .Where(cadet => cadet.Status == Status.Active))
                .Where(x => x.Status == Status.Active
                            && (x.Id == (int)CatalogsEnum.UnauthorizedRecords || x.Id == (int)CatalogsEnum.Rooms));

            Query.AsNoTracking();
        }
    }
}