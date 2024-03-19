using Investors.Shared.Domain.Exceptions;

namespace Investors.Client.Users.Domain.Exceptions
{
    public class ConnectionToProviderInvoiceException : BadRequestException
    {
        public ConnectionToProviderInvoiceException(string message)
            : base(message)
        {

        }
    }
}