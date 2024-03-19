using Ardalis.Specification;
using Investors.Client.Users.Domain.Aggregate;

namespace Investors.Client.Users.Specifications
{
    internal sealed class UserAndNotificationByUserIdSpecs : Specification<User>,
        ISingleResultSpecification<User>
    {
        /// <summary>
        /// Constructor specificacion consultar usuario por id
        /// </summary>
        /// <param name="userId"></param>
        public UserAndNotificationByUserIdSpecs(Guid userId)
        {           
            Query.Include(x => x.Notifications.Where(c => c.Status == Status.Active || c.Status == Status.Inactive));

            Query.Where(x => x.Status == Status.Active && x.Id == userId);

            Query.AsSplitQuery();

        }
    }
}