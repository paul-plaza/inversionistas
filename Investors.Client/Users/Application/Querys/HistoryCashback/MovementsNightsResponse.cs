namespace Investors.Client.Users.Application.Querys.HistoryCashback
{
    public record UserIncludeMovementsNightsResponse(
        List<MovementsNightsResponse> Movements);

    public record MovementsNightsResponse(
        Guid Id,
        Guid UserId,
        string Identification,
        string Names,
        string Mail,
        int RoomTypeId,
        string RoomType,
        string RoomDetail,
        int NightsToRedeem,
        DateOnly CheckIn,
        DateOnly CheckOut,
        string Status,
        int StatusId,
        string Observation
        );
}