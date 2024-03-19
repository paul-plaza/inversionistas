using CSharpFunctionalExtensions;
using Investors.Kernel.Shared.Operations.Specifications;
using Investors.Kernel.Shared.Shared.Infrastructure;
using MediatR;

namespace Investors.Kernel.Shared.Operations.Application.Querys.Operation.OperationsAndRestaurantsNames
{
    public record OperationsAndRestaurantsNamesQuery : IRequest<Result<IReadOnlyCollection<OperationsNamesResponse>>>;

    public class OperationsAndRestaurantsNamesHandler : IRequestHandler<OperationsAndRestaurantsNamesQuery, Result<IReadOnlyCollection<OperationsNamesResponse>>>
    {
        private readonly IRepositorySharedForReadManager _repositoryClient;
        public OperationsAndRestaurantsNamesHandler(IRepositorySharedForReadManager repositoryClient)
        {
            _repositoryClient = repositoryClient;
        }

        public async Task<Result<IReadOnlyCollection<OperationsNamesResponse>>> Handle(OperationsAndRestaurantsNamesQuery request, CancellationToken cancellationToken)
        {
            var operations = await _repositoryClient.Operations.ListAsync(new OperationsAndRestaurantsNamesSpecs(), cancellationToken);

            if (!operations.Any())
            {
                return Result.Failure<IReadOnlyCollection<OperationsNamesResponse>>("Operaciones no encontradas");
            }

            return Result.Success<IReadOnlyCollection<OperationsNamesResponse>>(operations);
        }
    }
}