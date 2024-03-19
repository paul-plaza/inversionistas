using Ardalis.Specification;
using Investors.Kernel.Shared.Operations.Domain.Aggregate;
using Investors.Shared.Domain.ValueObjects;

namespace Investors.Kernel.Shared.Operations.Specifications
{
    internal sealed class OperationAndRestaurantByDescriptionSpecs : Specification<Operation>,
        ISingleResultSpecification<Operation>
    {
        public OperationAndRestaurantByDescriptionSpecs(int operationId, int restaurantId, int menuTypeId)
        {
            Query.Where(x => x.Id == operationId && x.Status == Status.Active);
            Query
                .Include(x => x.Restaurants.Where(y => y.Id == restaurantId && y.Status == Status.Active))
                .ThenInclude(y => y.MenuTypes.Where(y => y.Id == menuTypeId && y.Status == Status.Active))
                .ThenInclude(z => z.Menus.Where(y => y.Status == Status.Active));

            Query.AsSplitQuery();
        }
    }
}