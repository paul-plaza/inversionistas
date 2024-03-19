using Ardalis.Specification;
using Investors.Kernel.Shared.Operations.Domain.Aggregate;
using Investors.Shared.Domain.ValueObjects;

namespace Investors.Kernel.Shared.Operations.Specifications
{

    internal sealed class OperationIncludeRoomsByOperationIdSpecs : Specification<Operation>,
        ISingleResultSpecification<Operation>
    {
        public OperationIncludeRoomsByOperationIdSpecs(int operationId)
        {
            Query.Where(x => x.Id == operationId && x.Status == Status.Active);
            Query.Include(x => x.Rooms.Where(y => y.Status == Status.Active));

        }
    }
}