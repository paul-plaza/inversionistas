using Ardalis.Specification.EntityFrameworkCore;
using Investors.Client.Users.Domain.Entities.Invoices;
using Investors.Client.Users.Repositories;
using Investors.Repository.EF.Shared;

namespace Investors.Repository.EF.Users
{

    public class InvoiceRepository : RepositoryBase<Invoice>, IInvoiceReadRepository
    {
        public InvoiceRepository(InvestorsDbContext repositoryContext) : base(repositoryContext)
        {

        }
    }
}
