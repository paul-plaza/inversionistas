using Ardalis.Specification;
using Investors.Client.Users.Domain.Entities.Invoices;

namespace Investors.Client.Users.Repositories
{
    public interface IReceiptReadRepository : IReadRepositoryBase<Receipt>
    {
    }

    public interface IReceiptWriteRepository : IRepositoryBase<Receipt>
    {
    }
}
