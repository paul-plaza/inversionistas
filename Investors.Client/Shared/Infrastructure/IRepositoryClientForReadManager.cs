using Investors.Client.Users.Domain.Repositories;
using Investors.Client.Users.Repositories;

namespace Investors.Client.Shared.Infrastructure
{
    public interface IRepositoryClientForReadManager
    {
        IUserReadRepository Users { get; }

        IMovimentReadRepository Movements { get; }

        IInvoiceReadRepository Invoices { get; }

        IReceiptReadRepository Receipts { get; }
    }
}