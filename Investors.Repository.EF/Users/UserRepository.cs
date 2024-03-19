using Investors.Repository.EF.Shared;
using Ardalis.Specification.EntityFrameworkCore;
using Investors.Client.Users.Domain.Aggregate;
using Investors.Client.Users.Domain.Repositories;

namespace Investors.Repository.EF.Users
{
    public class UserRepository : RepositoryBase<User>, IUserWriteRepository, IUserReadRepository
    {
        public UserRepository(InvestorsDbContext repositoryContext) : base(repositoryContext)
        {

        }
    }
}