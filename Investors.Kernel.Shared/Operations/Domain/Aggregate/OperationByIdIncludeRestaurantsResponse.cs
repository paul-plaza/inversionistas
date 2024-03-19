using Investors.Kernel.Shared.Operations.Domain.Entities.Restaurants;

namespace Investors.Kernel.Shared.Operations.Domain.Aggregate
{
    public record OperationByIdIncludeRestaurantsResponse(
        int OperationId,
        string Description,
        string UrlLogo,
        string Email,
        List<RestaurantResponse> Restaurants);

    public record OperationByIdIncludeRestaurantsIncludeMenResponse(
        List<RestaurantByIdIncludeMenusResponse> Restaurants);
}