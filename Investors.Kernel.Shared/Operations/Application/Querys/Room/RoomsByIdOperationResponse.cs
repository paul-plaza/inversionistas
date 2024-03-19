namespace Investors.Kernel.Shared.Operations.Application.Querys.Room
{
    public record RoomsByIdOperationResponse(
        int Id,
        string Description,
        string UrlLogo,
        string Title,
        string TypeRoom,
        int? RoomTypeId,
        string Observation,
        int Guests);
}