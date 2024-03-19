using Ardalis.Specification;
using Investors.Kernel.Shared.Catalogs.Domain.Aggregate;
using Investors.Shared.Constants;
using Investors.Shared.Domain.ValueObjects;

namespace Investors.Kernel.Shared.Catalogs.Specifications
{

    public sealed class UrlAdministratorApiSpecs : Specification<Catalog, string>, ISingleResultSpecification<Catalog, string>
    {
        public UrlAdministratorApiSpecs()
        {
            Query.Include(x =>
                    x.CatalogDetails
                        .Where(cadet => cadet.Status == Status.Active))
                .Where(x => x.Status == Status.Active && x.Id == (int)CatalogsEnum.UrlAdministratorApi);

            Query.Select(x => x.CatalogDetails.First().Value);
            Query.AsNoTracking();
        }
    }
}