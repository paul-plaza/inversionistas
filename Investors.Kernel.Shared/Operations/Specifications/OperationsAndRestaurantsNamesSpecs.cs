using Ardalis.Specification;
using Investors.Kernel.Shared.Operations.Application.Querys.Operation.OperationsAndRestaurantsNames;
using Investors.Kernel.Shared.Operations.Domain.Aggregate;
using Investors.Shared.Domain.ValueObjects;

namespace Investors.Kernel.Shared.Operations.Specifications
{
    internal sealed class OperationsAndRestaurantsNamesSpecs : Specification<Operation, OperationsNamesResponse>
    {
        public OperationsAndRestaurantsNamesSpecs()
        {
            Query.Include(x => x.Restaurants);

            Query.Where(x => x.Status == Status.Active);

            Query.Select(operation => new OperationsNamesResponse
            {
                Id = operation.Id,
                Name = operation.Description,
                Restaurants = operation.Restaurants
                    .Where(x => x.Status == Status.Active)
                    .Select(restaurant => new RestaurantsNamesResponse
                    {
                        OperationId = restaurant.OperationId,
                        Id = restaurant.Id,
                        Image = restaurant.UrlLogo,
                        Name = restaurant.Description
                    }).ToList()
            });

            Query.AsNoTracking();


        }
    }
}