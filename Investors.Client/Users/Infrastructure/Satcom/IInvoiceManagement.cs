using Investors.Client.Users.Infrastructure.Satcom.DataObject;

namespace Investors.Client.Users.Infrastructure.Satcom
{
    public interface IInvoiceManagement
    {
        Task<Result<List<ResponseInvoice>>> GetInvoices(int operationId, string userName, string password, RequestInvoiceFromDates request, CancellationToken cancellationToken);
    }
}