namespace Investors.Kernel.Shared.Events.Application.Querys.ShowEvents.Response
{
    public record EventResponse(
        int Id,
        string Description,
        int Order,
        string ItemType,
        List<EventDetailResponse> Children
        );
}