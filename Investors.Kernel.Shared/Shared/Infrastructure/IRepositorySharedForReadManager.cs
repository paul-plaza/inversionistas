using Investors.Kernel.Shared.Catalogs.Repositories;
using Investors.Kernel.Shared.Events.Repositories;
using Investors.Kernel.Shared.Investors.Domain.Repositories;
using Investors.Kernel.Shared.Operations.Domain.Repositories;

namespace Investors.Kernel.Shared.Shared.Infrastructure
{
    public interface IRepositorySharedForReadManager
    {
        IInvestorReadRepository Investors { get; }
        IOperationReadRepository Operations { get; }

        IEventReadRepository Events { get; }

        ICatalogReadRepository Catalogs { get; }

    }
}