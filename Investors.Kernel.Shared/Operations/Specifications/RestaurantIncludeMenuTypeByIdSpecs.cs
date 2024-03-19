using Ardalis.Specification;
using Investors.Kernel.Shared.Operations.Domain.Aggregate;
using Investors.Kernel.Shared.Operations.Domain.Entities.Restaurants;
using Investors.Shared.Domain.ValueObjects;

namespace Investors.Kernel.Shared.Operations.Specifications
{
    internal sealed class RestaurantIncludeMenuTypeByIdSpecs : Specification<Operation>, ISingleResultSpecification<Operation>
    {
        public RestaurantIncludeMenuTypeByIdSpecs(int idRestaurant, int idOperation, int menuTypeId)
        {
            Query.Include(x =>
                    x.Restaurants.Where(y => y.Id == idRestaurant && y.Status == Status.Active))
                .ThenInclude(x => x.MenuTypes.Where(y => y.Id == menuTypeId));
            Query.Where(x => x.Id == idOperation && x.Status == Status.Active);

            Query.AsSplitQuery();
        }
    }
}