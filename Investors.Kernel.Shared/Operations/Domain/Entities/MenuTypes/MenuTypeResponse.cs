using Investors.Kernel.Shared.Operations.Domain.Entities.Menus;

namespace Investors.Kernel.Shared.Operations.Domain.Entities.MenuTypes
{
    public record MenuTypeResponse(
        int Id,
        string Description,
        string UrlLogo,
        IReadOnlyCollection<MenuResponse> Menus);
}