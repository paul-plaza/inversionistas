using Investors.Client.Users.Domain.Services;
using Investors.Client.Users.Domain.ValueObjects;
using MediatR;

namespace Investors.Client.Users.Application.Commands.Nights
{
    public sealed record RedeemNightsCommand(Guid UserId, Guid MovementId, TransactionState TransactionState, Guid UserInSession) : IRequest<Result<ResponseDefault>>;

    public class RedeemNightsHandler : IRequestHandler<RedeemNightsCommand, Result<ResponseDefault>>
    {
        private readonly IUserManager _userManager;

        public RedeemNightsHandler(IUserManager userManager)
        {
            _userManager = userManager;
        }

        public async Task<Result<ResponseDefault>> Handle(RedeemNightsCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.Users.RedeemNights(request.UserId, request.MovementId, request.TransactionState, request.UserInSession, cancellationToken);
            return user;
        }
    }
}