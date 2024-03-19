using CSharpFunctionalExtensions;
using Investors.Administrator.Users.Domain.Repositories;
using Investors.Administrator.Users.Specifications;
using MediatR;

namespace Investors.Administrator.Users.Application.Querys.GetUsersAdmin.UserAdminProfile
{
    public sealed record OptionsByUserQuery(string Email) : IRequest<Result<UserAdministratorResponse>>;

    public class OptionsByUserQueryHandler : IRequestHandler<OptionsByUserQuery, Result<UserAdministratorResponse>>
    {
        private readonly IRepositoryUserAdministratorForRead _repositoryUserAdmin;
        public OptionsByUserQueryHandler(IRepositoryUserAdministratorForRead repositoryUserAdmin)
        {
            _repositoryUserAdmin = repositoryUserAdmin;
        }

        public async Task<Result<UserAdministratorResponse>> Handle(OptionsByUserQuery request, CancellationToken cancellationToken)
        {
            var user = await _repositoryUserAdmin.SingleOrDefaultAsync(new UserAdminProfileSpecs(request.Email), cancellationToken);

            if (user is null)
            {
                return Result.Failure<UserAdministratorResponse>("Usuario no configurado");
            }

            if (!user.Options.Any())
            {
                return Result.Failure<UserAdministratorResponse>("Usuario no tiene configurado accesos");
            }

            if (!user.Operations.Any())
            {
                return Result.Failure<UserAdministratorResponse>("Usuario no tiene asignada ninguna operacion");
            }

            return Result.Success(user);
        }
    }
}