using Ardalis.Specification;
using Investors.Kernel.Shared.Operations.Domain.Aggregate;
using Investors.Kernel.Shared.Operations.Domain.Entities.Menus;
using Investors.Kernel.Shared.Operations.Domain.Entities.MenuTypes;
using Investors.Kernel.Shared.Operations.Domain.Entities.Restaurants;
using Investors.Shared.Domain.ValueObjects;

namespace Investors.Kernel.Shared.Operations.Specifications
{
    internal sealed class RestaurantByIdIncludeMenusSpecs : Specification<Operation, OperationByIdIncludeRestaurantsIncludeMenResponse>,
        ISingleResultSpecification<Operation, OperationByIdIncludeRestaurantsIncludeMenResponse>
    {
        public RestaurantByIdIncludeMenusSpecs(int operationId, int restaurantId, bool isNoTracking = false)
        {
            Query.Where(x =>
                    x.Id == operationId
                    && x.Status == Status.Active
                );

            Query.Take(1);

            Query.Select(op =>
                new OperationByIdIncludeRestaurantsIncludeMenResponse(
                    op.Restaurants
                        .Where(x => x.Id == restaurantId && x.Status == Status.Active)
                        .Select(res => new RestaurantByIdIncludeMenusResponse(
                            res.MenuTypes.Where(x => x.RestaurantId == restaurantId
                                                     && x.Status == Status.Active)
                                .Select(mt => new MenuTypeResponse(
                                    mt.Id,
                                    mt.Description,
                                    mt.UrlLogo,
                                    mt.Menus.Where(x => x.Status == Status.Active).Select(m => new MenuResponse(
                                        m.Id,
                                        m.Title,
                                        m.Description,
                                        m.Points
                                        )).ToList()
                                    )).ToList()
                            )).ToList()));

            if (isNoTracking)
            {
                Query.AsNoTracking();
            }

            Query.AsSplitQuery();




        }
    }
}