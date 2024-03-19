using Ardalis.Specification;
using Investors.Kernel.Shared.Operations.Domain.Aggregate;
using Investors.Kernel.Shared.Operations.Domain.Entities.Restaurants;
using Investors.Shared.Domain.ValueObjects;

namespace Investors.Kernel.Shared.Operations.Specifications
{
    internal sealed class RestaurantByIdSpecs : Specification<Operation>,
        ISingleResultSpecification<Operation>
    {
        public RestaurantByIdSpecs(int idRestaurant, int idOperation)
        {
            Query.Include(x => x.Restaurants.Where(y => y.Id == idRestaurant && y.Status == Status.Active));
            Query.Where(x => x.Id == idOperation && x.Status == Status.Active);
        }
    }
}