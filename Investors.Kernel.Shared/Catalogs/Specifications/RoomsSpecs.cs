using Ardalis.Specification;
using Investors.Kernel.Shared.Catalogs.Domain.Aggregate;
using Investors.Shared.Constants;
using Investors.Shared.Domain.ValueObjects;

namespace Investors.Kernel.Shared.Catalogs.Specifications
{
    internal sealed class RoomsSpecs : Specification<Catalog>
    {
        public RoomsSpecs()
        {
            Query.Include(x =>
                    x.CatalogDetails
                        .Where(cadet => cadet.Status == Status.Active))
                .Where(x => x.Status == Status.Active && x.Id == (int)CatalogsEnum.Rooms);

            Query.AsNoTracking();
        }
    }
}