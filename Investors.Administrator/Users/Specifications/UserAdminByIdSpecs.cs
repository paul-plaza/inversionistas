using Ardalis.Specification;
using Investors.Administrator.Users.Domain.Aggregate;

namespace Investors.Administrator.Users.Specifications
{
    public sealed class UserAdminByIdSpecs : Specification<UserAdministrator>, ISingleResultSpecification<UserAdministrator>
    {
        public UserAdminByIdSpecs(int userId)
        {
            Query.Where(x => x.Id == userId);

            Query.Include(x => x.Operations);

            Query.Include(x => x.Options);

            Query.AsSplitQuery();
        }
    }
}