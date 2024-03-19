using CSharpFunctionalExtensions;
using Investors.Kernel.Shared.Operations.Domain.Entities.MenuTypes;
using Investors.Kernel.Shared.Operations.Specifications;
using Investors.Kernel.Shared.Shared.Infrastructure;
using MediatR;

namespace Investors.Kernel.Shared.Operations.Application.Querys.Menu
{
    public record MenuByOperationIdAndRestaurantIdQuery(int IdOperation, int IdRestaurant) : IRequest<Result<IEnumerable<MenuTypeResponse>>>;

    public class MenuByOperationIdAndRestaurantIdQueryHandler : IRequestHandler<MenuByOperationIdAndRestaurantIdQuery, Result<IEnumerable<MenuTypeResponse>>>
    {
        private readonly IRepositorySharedForReadManager _repositoryClient;
        public MenuByOperationIdAndRestaurantIdQueryHandler(IRepositorySharedForReadManager repositoryClient)
        {
            _repositoryClient = repositoryClient;
        }

        public async Task<Result<IEnumerable<MenuTypeResponse>>> Handle(MenuByOperationIdAndRestaurantIdQuery request, CancellationToken cancellationToken)
        {
            var operation = await _repositoryClient.Operations.SingleOrDefaultAsync(new RestaurantByIdIncludeMenusSpecs(request.IdOperation, request.IdRestaurant, true)
                , cancellationToken);

            if (operation is null)
            {
                return Result.Failure<IEnumerable<MenuTypeResponse>>("Operación no encontrada");
            }
            if (!operation.Restaurants.Any())
            {
                return Result.Failure<IEnumerable<MenuTypeResponse>>("Restaurantes no encontrados");
            }
            if (!operation.Restaurants.SelectMany(x => x.MenuTypes).Any())
            {
                return Result.Failure<IEnumerable<MenuTypeResponse>>("El Restaurante seleccionado no tiene menús");
            }
            var menuTypes = operation.Restaurants.SelectMany(x => x.MenuTypes).AsEnumerable();
            return Result.Success(menuTypes);
        }
    }

}