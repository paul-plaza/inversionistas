using Investors.Client.Shared.Infrastructure;
using Investors.Client.Users.Application.Commands.Requests;
using Investors.Client.Users.Specifications;
using MediatR;

namespace Investors.Client.Users.Application.Commands.CashBacks
{

    public sealed record RedeemCashBackFromHistoryCommand(Guid UserId, Guid MovementId) : IRequest<Result<ResponseDefault>>;

    public class RedeemCashBackFromHistoryHandler : IRequestHandler<RedeemCashBackFromHistoryCommand, Result<ResponseDefault>>
    {
        private readonly ISender _sender;
        private readonly IRepositoryClientForReadManager _repositoryClient;
        public RedeemCashBackFromHistoryHandler(IRepositoryClientForReadManager repositoryClient, ISender sender)
        {
            _repositoryClient = repositoryClient;
            _sender = sender;
        }

        public async Task<Result<ResponseDefault>> Handle(RedeemCashBackFromHistoryCommand request, CancellationToken cancellationToken)
        {
            var movement = await _repositoryClient.Movements.SingleOrDefaultAsync(new CashbackHistoryDetailByIdSpecs(request.MovementId), cancellationToken);

            if (movement is null)
            {
                return Result.Failure<ResponseDefault>("No se encontró el movimiento");
            }

            if (movement.RestaurantId is null)
            {
                return Result.Failure<ResponseDefault>("El registro seleccionado no tiene un restaurante asociado");
            }

            var requestRedeem = new ReedemCashBackRequest(
                movement.RestaurantId.Value,
                movement.OperationId,
                movement.CashbackDetails
                    .Select(x => x.MenuId)
                    .ToList());

            return await _sender.Send(new RegisterRedemptionCashBackCommand(request.UserId, requestRedeem), cancellationToken);
        }
    }
}