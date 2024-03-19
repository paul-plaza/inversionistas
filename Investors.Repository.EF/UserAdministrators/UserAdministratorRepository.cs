using Ardalis.Specification.EntityFrameworkCore;
using Investors.Administrator.Users.Domain.Aggregate;
using Investors.Administrator.Users.Domain.Repositories;
using Investors.Repository.EF.Shared;

namespace Investors.Repository.EF.UserAdministrators
{
    public class RepositoryUserAdministratorFor : RepositoryBase<UserAdministrator>, IRepositoryUserAdministratorForRead, IUserAdministratorWriteRepository
    {
        public RepositoryUserAdministratorFor(InvestorsDbContext repositoryContext) : base(repositoryContext)
        {

        }
    }
}