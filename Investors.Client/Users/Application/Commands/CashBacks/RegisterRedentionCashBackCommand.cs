using Investors.Client.Users.Application.Commands.Requests;
using Investors.Client.Users.Domain.Services;
using MediatR;

namespace Investors.Client.Users.Application.Commands.CashBacks
{
    public sealed record RegisterRedemptionCashBackCommand(Guid UserId, ReedemCashBackRequest Item) : IRequest<Result<ResponseDefault>>;

    public class RegisterRedemptionCashBackHandler : IRequestHandler<RegisterRedemptionCashBackCommand, Result<ResponseDefault>>
    {
        private readonly IUserManager _userManager;
        public RegisterRedemptionCashBackHandler(IUserManager userManager)
        {
            _userManager = userManager;
        }
        public async Task<Result<ResponseDefault>> Handle(RegisterRedemptionCashBackCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.Users.RegisterRedemptionCashback(
                request.Item.OperationId,
                request.Item.MenuIds,
                request.Item.RestaurantId,
                request.UserId,
                request.UserId,
                cancellationToken);
            return user;
        }
    }
}