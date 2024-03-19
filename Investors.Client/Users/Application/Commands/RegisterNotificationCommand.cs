using Investors.Client.Users.Domain.Services;
using Investors.Client.Users.Domain.ValueObjects;
using MediatR;

namespace Investors.Client.Users.Application.Commands
{
    public sealed record RegisterNotificationCommand
        (Guid UserId,
        string Title,
        string? SubTitle,
        NotificationType NotificationType,
        string Description,
        int? OperationId,
        Guid UserInSession) : IRequest<Result>;

    public class RegisterNotificationHandler : IRequestHandler<RegisterNotificationCommand, Result>
    {
        private readonly IUserManager _userManager;
        public RegisterNotificationHandler(IUserManager userManager)
        {
            _userManager = userManager;
        }
        public async Task<Result> Handle(RegisterNotificationCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.Users.RegisterNotification(
                request.UserId,
                request.Title,
                request.SubTitle,
                request.Description,
                request.NotificationType,
                request.OperationId,
                request.UserInSession,
                cancellationToken);
            return user;
        }
    }
}