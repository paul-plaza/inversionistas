using Ardalis.Specification;
using Investors.Kernel.Shared.Operations.Domain.Aggregate;
using Investors.Shared.Domain.ValueObjects;

namespace Investors.Kernel.Shared.Operations.Specifications
{
    internal sealed class OperationsActiveSpecs : Specification<Operation, OperationResponse>
    {
        public OperationsActiveSpecs(bool isNoTracking = false)
        {
            Query.Select(operation => new OperationResponse(
                operation.Id,
                operation.Description,
                operation.Alias,
                operation.UrlLogo,
                operation.Order,
                operation.UserName,
                operation.Password,
                operation.Email
                ));

            if (isNoTracking)
            {
                Query.AsNoTracking();
            }

            Query.Where(x => x.Status == Status.Active);
        }
    }
}