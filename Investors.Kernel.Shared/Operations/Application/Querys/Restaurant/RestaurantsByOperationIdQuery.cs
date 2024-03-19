using CSharpFunctionalExtensions;
using Investors.Kernel.Shared.Operations.Domain.Entities.Restaurants;
using Investors.Kernel.Shared.Operations.Specifications;
using Investors.Kernel.Shared.Shared.Infrastructure;
using MediatR;

namespace Investors.Kernel.Shared.Operations.Application.Querys.Restaurant
{
    public record RestaurantsByOperationIdQuery(int OperationId) : IRequest<Result<List<RestaurantResponse>>>;

    public class OperationByIdIncludeRestaurantsHandler : IRequestHandler<RestaurantsByOperationIdQuery, Result<List<RestaurantResponse>>>
    {
        private readonly IRepositorySharedForReadManager _repositoryClient;
        public OperationByIdIncludeRestaurantsHandler(IRepositorySharedForReadManager repositoryClient)
        {
            _repositoryClient = repositoryClient;
        }

        public async Task<Result<List<RestaurantResponse>>> Handle(RestaurantsByOperationIdQuery request, CancellationToken cancellationToken)
        {
            var operation = await _repositoryClient.Operations.SingleOrDefaultAsync(new OperationByIdIncludeRestaurantsSpecs(request.OperationId, true), cancellationToken);

            if (operation is null)
            {
                return Result.Failure<List<RestaurantResponse>>("Operación no encontrada");
            }
            if (!operation.Restaurants.Any())
            {
                return Result.Failure<List<RestaurantResponse>>("Restaurantes no encontrados");
            }
            return Result.Success(operation.Restaurants);
        }
    }
}