namespace Investors.Kernel.Shared.Events.Application.Querys.ShowEvents.Response
{
    public record EventDetailResponse(
        int Id,
        int OperationId,
        string Description,
        string Title,
        string UrlLogo,
        string UrlToOpen,
        DateTime CreatedOn,
        IReadOnlyCollection<EventSubDetailResponse> Children
        );
}