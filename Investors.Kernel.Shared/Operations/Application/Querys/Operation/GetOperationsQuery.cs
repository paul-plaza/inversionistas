using CSharpFunctionalExtensions;
using Investors.Kernel.Shared.Operations.Domain.Aggregate;
using Investors.Kernel.Shared.Operations.Specifications;
using Investors.Kernel.Shared.Shared.Infrastructure;
using MediatR;

namespace Investors.Kernel.Shared.Operations.Application.Querys.Operation
{
    public record GetOperationsQuery : IRequest<Result<IReadOnlyCollection<OperationResponse>>>;

    public class GetOperationsHandler : IRequestHandler<GetOperationsQuery, Result<IReadOnlyCollection<OperationResponse>>>
    {
        private readonly IRepositorySharedForReadManager _repositoryClient;
        public GetOperationsHandler(IRepositorySharedForReadManager repositoryClient)
        {
            _repositoryClient = repositoryClient;
        }

        public async Task<Result<IReadOnlyCollection<OperationResponse>>> Handle(GetOperationsQuery request, CancellationToken cancellationToken)
        {
            var operation = await _repositoryClient.Operations.ListAsync(new OperationsActiveSpecs(true), cancellationToken);

            if (!operation.Any())
            {
                return Result.Failure<IReadOnlyCollection<OperationResponse>>("Operaciones no encontradas");
            }

            return Result.Success<IReadOnlyCollection<OperationResponse>>(operation);
        }
    }
}