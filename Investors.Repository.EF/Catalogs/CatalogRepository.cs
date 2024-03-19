using Ardalis.Specification.EntityFrameworkCore;
using Investors.Kernel.Shared.Catalogs.Domain.Aggregate;
using Investors.Kernel.Shared.Catalogs.Repositories;
using Investors.Repository.EF.Shared;

namespace Investors.Repository.EF.Catalogs
{
    internal class CatalogRepository : RepositoryBase<Catalog>, ICatalogWriteRepository, ICatalogReadRepository
    {
        public CatalogRepository(InvestorsDbContext repositoryContext) : base(repositoryContext)
        {

        }
    }
}
