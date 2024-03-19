using Ardalis.Specification.EntityFrameworkCore;
using Investors.Client.Users.Domain.Entities.Invoices;
using Investors.Client.Users.Repositories;
using Investors.Repository.EF.Shared;

namespace Investors.Repository.EF.Users
{
    public class ReceiptRepository : RepositoryBase<Receipt>, IReceiptReadRepository, IReceiptWriteRepository
    {
        public ReceiptRepository(InvestorsDbContext repositoryContext) : base(repositoryContext)
        {

        }
    }
}
