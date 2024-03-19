using Investors.Client.Shared.Infrastructure;
using Investors.Client.Users.Specifications;
using Investors.Kernel.Shared.Operations.Application.Querys.Menu;
using MediatR;

namespace Investors.Client.Users.Application.Querys.HistoryCashbackDetail
{


    public record HistoryCashbackDetailQuery(Guid MovementId) : IRequest<Result<List<HistoryCashbackDetailResponse>>>;

    public class HistoryCashbackDetailHandler : IRequestHandler<HistoryCashbackDetailQuery, Result<List<HistoryCashbackDetailResponse>>>
    {
        private readonly ISender _sender;
        private readonly IRepositoryClientForReadManager _repositoryClient;
        public HistoryCashbackDetailHandler(IRepositoryClientForReadManager repositoryClient, ISender sender)
        {
            _repositoryClient = repositoryClient;
            _sender = sender;
        }

        public async Task<Result<List<HistoryCashbackDetailResponse>>> Handle(HistoryCashbackDetailQuery request, CancellationToken cancellationToken)
        {
            var movement = await _repositoryClient.Movements.SingleOrDefaultAsync(new CashbackHistoryDetailByIdSpecs(request.MovementId), cancellationToken);

            if (movement is null)
            {
                return Result.Failure<List<HistoryCashbackDetailResponse>>("No se encontró el movimiento");
            }

            if (movement.RestaurantId is null)
            {
                return Result.Failure<List<HistoryCashbackDetailResponse>>("El registro seleccionado no tiene un restaurante asociado");
            }
            var menus = await _sender
                .Send(new MenuByOperationIdAndRestaurantIdQuery(movement.OperationId, movement.RestaurantId.Value!), cancellationToken);

            if (menus.IsFailure)
            {
                return Result.Failure<List<HistoryCashbackDetailResponse>>(menus.Error);
            }

            var response = (from details in movement.CashbackDetails
                    join menu in menus.Value.SelectMany(x => x.Menus).DefaultIfEmpty()
                        on details.MenuId equals menu?.Id
                    select new HistoryCashbackDetailResponse
                    {
                        Id = details.Id,
                        Title = menu?.Title ?? "Producto no disponible",
                        Description = menu?.Description ?? "Descripción no disponible",
                        Points = menu?.Points ?? 0
                    }
                ).ToList();


            return Result.Success(response);
        }
    }
}