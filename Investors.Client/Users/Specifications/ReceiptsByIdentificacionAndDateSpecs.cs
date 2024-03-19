using Ardalis.Specification;
using Investors.Client.Users.Domain.Entities.Invoices;

namespace Investors.Client.Users.Specifications
{
    internal sealed class ReceiptsByIdentificacionAndDateSpecs : Specification<Receipt>, ISingleResultSpecification<Receipt>
    {
        /// <summary>
        /// Constructor specificacion consultar recibo por identificacion y fecha
        /// </summary>
        /// <param name="identification"></param>
        /// <param name="date"></param>
        public ReceiptsByIdentificacionAndDateSpecs(string identification, DateOnly date)
        {
            Query.Where(x => x.Identification == identification && x.Date == date && x.Status == Status.Active);
        }
    }
}
