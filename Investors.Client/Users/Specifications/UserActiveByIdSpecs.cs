using Ardalis.Specification;
using Investors.Client.Users.Domain.Aggregate;

namespace Investors.Client.Users.Specifications
{
    internal sealed class UserActiveByIdSpecs : Specification<User>, ISingleResultSpecification<User>
    {
        /// <summary>
        /// Constructor specificacion consultar usuario por id
        /// </summary>
        /// <param name="idUser"></param>
        public UserActiveByIdSpecs(Guid idUser)
        {
            Query.Where(x => x.Id == idUser && x.Status == Status.Active);

            Query.Include(x => x.Profile);

        }
    }
}