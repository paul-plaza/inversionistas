using Ardalis.Specification;
using Investors.Client.Users.Domain.Entities.Invoices;

namespace Investors.Client.Users.Specifications
{
    internal sealed class GetInvoicesSpecs : Specification<Invoice>
    {
        public GetInvoicesSpecs()
        {
               Query.Where(x =>
                   x.Status == Status.Active);
        }
    }
}
