namespace Investors.Kernel.Shared.Events.Application.Querys.Admin.Events
{
    public record EventForAdminResponse(
        int Id,
        string Description,
        int Order,
        string ItemType
        );
}