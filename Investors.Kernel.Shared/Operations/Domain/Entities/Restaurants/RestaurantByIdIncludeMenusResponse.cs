using Investors.Kernel.Shared.Operations.Domain.Entities.MenuTypes;

namespace Investors.Kernel.Shared.Operations.Domain.Entities.Restaurants
{
    public record RestaurantResponse(
        int Id,
        string Description,
        string UrlLogo,
        string Email
        );
    public record RestaurantByIdIncludeMenusResponse(
        List<MenuTypeResponse> MenuTypes
        );
}