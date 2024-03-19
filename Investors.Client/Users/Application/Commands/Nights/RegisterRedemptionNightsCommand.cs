using Investors.Client.Users.Application.Commands.Requests;
using Investors.Client.Users.Domain.Services;
using MediatR;

namespace Investors.Client.Users.Application.Commands.Nights
{
    public sealed record RegisterRedemptionNightsCommand(Guid UserId, RegisterRedeemNightsRequest Item) : IRequest<Result<ResponseDefault>>;

    public class RegisterRedemptionNightsHandler : IRequestHandler<RegisterRedemptionNightsCommand, Result<ResponseDefault>>
    {
        private readonly IUserManager _userManager;
        public RegisterRedemptionNightsHandler(IUserManager userManager)
        {
            _userManager = userManager;
        }
        public async Task<Result<ResponseDefault>> Handle(RegisterRedemptionNightsCommand request, CancellationToken cancellationToken)
        {
            var register = await _userManager.Users.RegisterRedemptionNights(request.UserId,
                request.Item.OperationId, request.Item.Observation, request.Item.RoomId,
                request.Item.DateStart, request.Item.DateEnd, request.UserId, cancellationToken);
            return register;
        }
    }
}