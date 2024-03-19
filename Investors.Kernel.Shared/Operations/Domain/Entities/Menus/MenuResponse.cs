namespace Investors.Kernel.Shared.Operations.Domain.Entities.Menus
{
    public record MenuResponse(
        int Id,
        string Title,
        string Description,
        int Points
        );
}