using Ardalis.Specification.EntityFrameworkCore;
using Investors.Client.Users.Domain.Entities.Transactions;
using Investors.Client.Users.Domain.Repositories;
using Investors.Repository.EF.Shared;

namespace Investors.Repository.EF.Users
{
    public class MovementRepository : RepositoryBase<Movement>, IMovimentReadRepository
    {
        public MovementRepository(InvestorsDbContext repositoryContext) : base(repositoryContext)
        {

        }
    }
}