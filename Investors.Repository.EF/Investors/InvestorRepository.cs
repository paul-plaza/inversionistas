using Ardalis.Specification.EntityFrameworkCore;
using Investors.Kernel.Shared.Investors.Domain.Aggregate;
using Investors.Kernel.Shared.Investors.Domain.Repositories;
using Investors.Repository.EF.Shared;
using Microsoft.EntityFrameworkCore;

namespace Investors.Repository.EF.Investors
{
    public class InvestorRepository : RepositoryBase<Investor>, IInvestorWriteRepository, IInvestorReadRepository
    {
        private readonly InvestorsDbContext _repositoryContext;
        public InvestorRepository(InvestorsDbContext repositoryContext) : base(repositoryContext)
        {
            _repositoryContext = repositoryContext;

        }
        public async Task ExecuteSyncUserInvestor(CancellationToken cancellationToken)
        {
            await _repositoryContext.Database.ExecuteSqlInterpolatedAsync($"spUpdateUserProfile", cancellationToken: cancellationToken);
        }
    }
}