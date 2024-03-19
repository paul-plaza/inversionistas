using Ardalis.Specification;
using Investors.Client.Users.Domain.Aggregate;

namespace Investors.Client.Users.Specifications
{
    public sealed class TotalNotificationUnReadSpecs : Specification<User, int>
    {
        public TotalNotificationUnReadSpecs(Guid idUser)
        {
            Query.AsNoTracking();

            Query.Where(x => x.Id == idUser && x.Status == Status.Active);

            Query.Include(x => x.Notifications);

            Query.Select(x => x.Notifications.Count(n => n.Status == Status.Active));

            Query.AsSplitQuery();
        }
    }
}