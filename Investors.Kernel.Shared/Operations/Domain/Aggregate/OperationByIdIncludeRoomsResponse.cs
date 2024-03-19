using Investors.Kernel.Shared.Operations.Application.Querys.Room;
using Investors.Kernel.Shared.Operations.Domain.Entities.Rooms;

namespace Investors.Kernel.Shared.Operations.Domain.Aggregate
{

    public record OperationByIdIncludeRoomsResponse(
        int OperationId,
        string Description,
        string UrlLogo,
        List<RoomsByIdOperationResponse> Rooms);
}
