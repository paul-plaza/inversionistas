using Investors.Client.Users.Domain.Services;
using Investors.Client.Users.Domain.ValueObjects;
using MediatR;

namespace Investors.Client.Users.Application.Commands.CashBacks
{
    public sealed record RedeemCashBackCommand(Guid UserId, Guid MovementId, TransactionState TransactionState, Guid UserInSession) : IRequest<Result<ResponseDefault>>;

    public class ReedemCashBackHandler : IRequestHandler<RedeemCashBackCommand, Result<ResponseDefault>>
    {
        private readonly IUserManager _userManager;
        public ReedemCashBackHandler(IUserManager userManager)
        {
            _userManager = userManager;
        }
        public async Task<Result<ResponseDefault>> Handle(RedeemCashBackCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.Users.RedeemCashBack(request.UserId, request.MovementId, request.UserInSession, request.TransactionState, cancellationToken);
            return user;
        }
    }
}