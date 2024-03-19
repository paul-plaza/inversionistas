using Ardalis.Specification.EntityFrameworkCore;
using Investors.Administrator.Users.Domain.Entities;
using Investors.Administrator.Users.Domain.Repositories;
using Investors.Repository.EF.Shared;

namespace Investors.Repository.EF.UserAdministrators
{
    public class RepositoryOptionFor : RepositoryBase<Option>, IRepositoryOptionForRead
    {
        public RepositoryOptionFor(InvestorsDbContext repositoryContext) : base(repositoryContext)
        {

        }
    }
}