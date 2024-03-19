using Ardalis.Specification;
using Investors.Kernel.Shared.Investors.Domain.Aggregate;

namespace Investors.Kernel.Shared.Investors.Domain.Repositories
{
    public interface IInvestorWriteRepository : IRepositoryBase<Investor>
    {
        Task ExecuteSyncUserInvestor(CancellationToken cancellationToken);
    }
}