using CSharpFunctionalExtensions;
using Investors.Kernel.Shared.Operations.Domain.Aggregate;
using Investors.Kernel.Shared.Operations.Specifications;
using Investors.Kernel.Shared.Shared.Infrastructure;
using MediatR;

namespace Investors.Kernel.Shared.Operations.Application.Querys.Restaurant
{
    public record GetOperationAndRestaurantByIdQuery(int OperationId, int RestaurantId) : IRequest<Result<OperationByIdIncludeRestaurantsResponse>>;

    public class GetOperationAndRestaurantByIdHandler : IRequestHandler<GetOperationAndRestaurantByIdQuery, Result<OperationByIdIncludeRestaurantsResponse>>
    {
        private readonly IRepositorySharedForReadManager _repositoryClient;
        public GetOperationAndRestaurantByIdHandler(IRepositorySharedForReadManager repositoryClient)
        {
            _repositoryClient = repositoryClient;
        }

        public async Task<Result<OperationByIdIncludeRestaurantsResponse>> Handle(GetOperationAndRestaurantByIdQuery request, CancellationToken cancellationToken)
        {
            var operation = await _repositoryClient.Operations.SingleOrDefaultAsync(new OperationAndRestaurantByIdSpecs(request.OperationId, request.RestaurantId, true), cancellationToken);

            if (operation == null)
            {
                return Result.Failure<OperationByIdIncludeRestaurantsResponse>("Operaciones no encontradas");
            }

            return Result.Success(operation);
        }
    }
}