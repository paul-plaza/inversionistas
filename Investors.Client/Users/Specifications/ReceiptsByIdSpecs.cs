using Ardalis.Specification;
using Investors.Client.Users.Domain.Entities.Invoices;

namespace Investors.Client.Users.Specifications
{
    internal sealed class ReceiptsByIdSpecs : Specification<Receipt>, ISingleResultSpecification<Receipt>
    {
        /// <summary>
        /// Constructor specificacion consultar recibo por id
        /// </summary>
        /// <param name="idReceipt"></param>
        public ReceiptsByIdSpecs(int idReceipt)
        {
            Query.Where(x => x.Id == idReceipt && x.Status == Status.Active);
        }
    }
}