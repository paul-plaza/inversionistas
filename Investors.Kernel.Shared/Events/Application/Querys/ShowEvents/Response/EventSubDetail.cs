namespace Investors.Kernel.Shared.Events.Application.Querys.ShowEvents.Response
{
    public record EventSubDetailResponse(
        int Id,
        string Title,
        string Description,
        string Image
        );
}