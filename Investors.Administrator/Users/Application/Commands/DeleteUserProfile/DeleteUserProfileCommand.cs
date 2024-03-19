using CSharpFunctionalExtensions;
using Investors.Administrator.Users.Domain.Repositories;
using Investors.Administrator.Users.Specifications;
using MediatR;

namespace Investors.Administrator.Users.Application.Commands.DeleteUserProfile
{
    public sealed record DeleteUserProfileCommand(int UserId, Guid userInSession) : IRequest<IResult>;

    public class DeleteUserHandler : IRequestHandler<DeleteUserProfileCommand, IResult>
    {
        private readonly IUserAdministratorWriteRepository _repositoryUserAdmin;

        public DeleteUserHandler(IUserAdministratorWriteRepository repositoryUserAdmin)
        {
            _repositoryUserAdmin = repositoryUserAdmin;

        }
        public async Task<IResult> Handle(DeleteUserProfileCommand request, CancellationToken cancellationToken)
        {
            var user = await _repositoryUserAdmin.SingleOrDefaultAsync(new UserAdminByIdSpecs(request.UserId), cancellationToken);

            if (user is null)
            {
                return Result.Failure("Usuario no encontrado");
            }

            user.DeleteUserProfile(request.userInSession);

            await _repositoryUserAdmin.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}