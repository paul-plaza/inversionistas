using Ardalis.Specification;
using Investors.Client.Users.Domain.Aggregate;
using Investors.Client.Users.Domain.ValueObjects;

namespace Investors.Client.Users.Specifications
{
    internal sealed class UsersAndProfileSpecs : Specification<User>, ISingleResultSpecification<User>
    {
        /// <summary>
        /// Constructor specificacion consultar usuario por id
        /// </summary>
        /// <param name="idUser"></param>
        public UsersAndProfileSpecs()
        {
            Query.Where(x => x.Status == Status.Active && x.UserType == UserType.Investor);

            Query.Include(x => x.Profile);

        }
    }
}