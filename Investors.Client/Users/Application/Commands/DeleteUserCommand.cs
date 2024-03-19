using Investors.Client.Users.Domain.Services;
using Investors.Shared.Infrastructure;
using MediatR;

namespace Investors.Client.Users.Application.Commands
{
    public sealed record DeleteUserCommand(Guid UserId) : IRequest<Result<ResponseDefault>>;

    public class DeleteUserHandler : IRequestHandler<DeleteUserCommand, Result<ResponseDefault>>
    {
        private readonly IUserManager _userManager;
        private readonly IIdentityProvider _identityProvider;
        public DeleteUserHandler(IUserManager userManager, IIdentityProvider identityProvider)
        {
            _userManager = userManager;
            _identityProvider = identityProvider;
        }

        public async Task<Result<ResponseDefault>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            string token = await _identityProvider.GetAccessTokenAsync();
            var response = await _identityProvider.DeleteUserAsync(token, request.UserId.ToString());
            if (response.IsFailure)
                return Result.Failure<ResponseDefault>(response.Error);

            var user = await _userManager.Users.DeleteUser(request.UserId, cancellationToken);
            if (user.IsFailure)
                return Result.Failure<ResponseDefault>(user.Error);

            ResponseDefault result = new ResponseDefault("Usuario eliminado correctamente");
            return result;
        }
    }
}