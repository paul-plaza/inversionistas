using CSharpFunctionalExtensions;
using Investors.Administrator.Users.Domain.Repositories;
using Investors.Administrator.Users.Specifications;
using MediatR;

namespace Investors.Administrator.Users.Application.Querys.GetUsersAdmin.UsersAdministrators
{
    public record AllUsersWithProfileQuery : IRequest<Result<List<UserAdministratorResponse>>>;

    public class AllUsersWithProfileHandler : IRequestHandler<AllUsersWithProfileQuery, Result<List<UserAdministratorResponse>>>
    {
        private readonly IRepositoryUserAdministratorForRead _repositoryUserAdmin;
        public AllUsersWithProfileHandler(IRepositoryUserAdministratorForRead repositoryUserAdmin)
        {
            _repositoryUserAdmin = repositoryUserAdmin;

        }
        public async Task<Result<List<UserAdministratorResponse>>> Handle(AllUsersWithProfileQuery request, CancellationToken cancellationToken)
        {
            var user = await _repositoryUserAdmin.ListAsync(new UsersAdminProfileSpecs(), cancellationToken);

            return Result.Success(user);
        }
    }

}