namespace Investors.Client.Users.Application.Querys.HistoryCashback
{
    public record UserIncludeMovementsResponse(
        List<MovementsCashbackResponse> Movements);

    public record MovementsCashbackResponse(
        Guid Id,
        DateTime Date,
        string Status,
        int StatusId,
        int PointToRedeem,
        int? TotalItems,
        string Identification,
        string UserId,
        string Names,
        string Mail
        );
}