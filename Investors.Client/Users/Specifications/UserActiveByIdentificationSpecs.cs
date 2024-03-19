using Ardalis.Specification;
using Investors.Client.Users.Domain.Aggregate;

namespace Investors.Client.Users.Specifications
{
    internal sealed class UserActiveByIdentificationSpecs : Specification<User>, ISingleResultSpecification<User>
    {
        /// <summary>
        /// Constructor specificacion consultar usuario por identificacion
        /// </summary>
        /// <param name="identification"></param>
        public UserActiveByIdentificationSpecs(string identification)
        {
            Query.Where(x => x.Identification == identification && x.Status == Status.Active);

            Query.AsNoTracking();

        }
    }
}