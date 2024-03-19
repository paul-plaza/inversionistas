namespace Investors.Kernel.Shared.Operations.Application.Querys.Operation.OperationAndRoomById
{
    public record OperationByIdIncludeRoomResponse(
        int OperationId,
        string Description,
        string UrlLogo,
        string Email,
        RoomByIdOperationResponse Room);
}