using Ardalis.Specification;
using Investors.Kernel.Shared.Operations.Domain.Aggregate;
using Investors.Shared.Domain.ValueObjects;

namespace Investors.Kernel.Shared.Operations.Specifications
{
    internal sealed class OperationIncludeRoomByIdSpecs : Specification<Operation>,
        ISingleResultSpecification<Operation>
    {
        public OperationIncludeRoomByIdSpecs(int operationId, int roomId)
        {
            Query
                .Include(x => x.Rooms.Where(y => y.Id == roomId && y.Status == Status.Active));
            Query.Where(x => x.Id == operationId && x.Status == Status.Active);
        }
    }
}