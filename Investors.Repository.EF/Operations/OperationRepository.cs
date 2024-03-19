using Ardalis.Specification.EntityFrameworkCore;
using Investors.Kernel.Shared.Operations.Domain.Aggregate;
using Investors.Kernel.Shared.Operations.Domain.Repositories;
using Investors.Repository.EF.Shared;

namespace Investors.Repository.EF.Operations
{
    public class OperationRepository : RepositoryBase<Operation>, IOperationWriteRepository, IOperationReadRepository
    {
        public OperationRepository(InvestorsDbContext repositoryContext) : base(repositoryContext)
        {

        }
    }
}
