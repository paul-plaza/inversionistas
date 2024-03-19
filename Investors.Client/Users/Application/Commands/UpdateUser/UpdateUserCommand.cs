using Investors.Client.Users.Domain.Services;
using MediatR;

namespace Investors.Client.Users.Application.Commands.UpdateUser
{
    public sealed record UpdateUserCommand(Guid UserId, UpdateUserRequest Item) : IRequest<Result<ResponseDefault>>;

    public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, Result<ResponseDefault>>
    {
        private readonly IUserManager _userManager;
        public UpdateUserHandler(IUserManager userManager)
        {
            _userManager = userManager;
        }

        public async Task<Result<ResponseDefault>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.Users.Update(
                userId: request.UserId,
                displayName: request.Item.DisplayName,
                identification: request.Item.Identification,
                name: request.Item.Name,
                surname: request.Item.Surname,
                cancellationToken: cancellationToken);
            if (user.IsFailure)
                return Result.Failure<ResponseDefault>(user.Error);

            ResponseDefault result = new ResponseDefault("Usuario modificado correctamente");
            return result;
        }
    }
}