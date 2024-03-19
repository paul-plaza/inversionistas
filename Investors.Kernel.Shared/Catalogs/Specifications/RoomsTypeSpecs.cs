using Ardalis.Specification;
using Investors.Kernel.Shared.Catalogs.Domain.Aggregate;
using Investors.Shared.Constants;
using Investors.Shared.Domain.ValueObjects;

namespace Investors.Kernel.Shared.Catalogs.Specifications
{
    internal sealed class RoomsTypeSpecs : Specification<Catalog>
    {
        public RoomsTypeSpecs()
        {
            Query.Include(x =>
                    x.CatalogDetails
                        .Where(cadet => cadet.Status == Status.Active))
                .Where(x => x.Status == Status.Active && x.Id == (int)CatalogsEnum.RoomsType);

            Query.AsNoTracking();
        }
    }
}