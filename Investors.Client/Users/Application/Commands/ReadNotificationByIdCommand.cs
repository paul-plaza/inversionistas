using Investors.Client.Users.Domain.Services;
using MediatR;

namespace Investors.Client.Users.Application.Commands
{
    public sealed record ReadNotificationByIdCommand(Guid UserId, int NotificationId, Guid UserInSession) : IRequest<Result<ResponseDefault>>;

    public class ReadNotificationByIdHandler : IRequestHandler<ReadNotificationByIdCommand, Result<ResponseDefault>>
    {
        private readonly IUserManager _userManager;

        public ReadNotificationByIdHandler(IUserManager userManager)
        {
            _userManager = userManager;
        }

        public async Task<Result<ResponseDefault>> Handle(ReadNotificationByIdCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.Users.ReadNotification(request.UserId, request.NotificationId, request.UserInSession, cancellationToken);
            return user;
        }
    }
}
