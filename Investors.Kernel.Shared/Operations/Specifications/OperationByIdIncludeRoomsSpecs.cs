using Ardalis.Specification;
using Investors.Kernel.Shared.Operations.Application.Querys.Room;
using Investors.Kernel.Shared.Operations.Domain.Aggregate;
using Investors.Kernel.Shared.Operations.Domain.Entities.Rooms;
using Investors.Shared.Domain.ValueObjects;

namespace Investors.Kernel.Shared.Operations.Specifications
{
    internal sealed class OperationByIdIncludeRoomsSpecs : Specification<Operation, OperationByIdIncludeRoomsResponse>,
        ISingleResultSpecification<Operation, OperationByIdIncludeRoomsResponse>
    {
        public OperationByIdIncludeRoomsSpecs(int operationId, bool isNoTracking = false)
        {
            Query.Include(x => x.Rooms);

            Query.Where(x => x.Id == operationId && x.Status == Status.Active);

            Query.Select(operation => new OperationByIdIncludeRoomsResponse(
                operation.Id,
                operation.Description,
                operation.UrlLogo,
                operation.Rooms.Where(c => c.Status == Status.Active)
                    .Select(x => new RoomsByIdOperationResponse(
                        x.Id,
                        x.Description,
                        x.UrlLogo,
                        x.Title,
                        "",
                        x.RoomTypeId,
                        x.Observation,
                        x.Guests
                        )).ToList()
                ));

            if (isNoTracking)
            {
                Query.AsNoTracking();
            }

            Query.AsSplitQuery();
        }
    }
}