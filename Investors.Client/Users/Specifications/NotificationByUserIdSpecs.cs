using Ardalis.Specification;
using Investors.Client.Users.Domain.Aggregate;

namespace Investors.Client.Users.Specifications
{
    internal sealed class NotificationByUserIdSpecs : Specification<User, UserIncludeNotificationsByUserIdResponse>,
        ISingleResultSpecification<User, UserIncludeNotificationsByUserIdResponse>
    {
        /// <summary>
        /// Constructor specificacion consultar usuario por id
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="isNoTracking"></param>
        public NotificationByUserIdSpecs(Guid userId, bool isNoTracking = false)
        {
            Query.Select(user => new UserIncludeNotificationsByUserIdResponse(
                user.Id,
                user.Notifications.OrderByDescending(notification => notification.Id)
                    .Select(x => new NotificationsByUserIdResponse(
                        x.Id,
                        x.Title,
                        x.SubTitle,
                        x.Description,
                        x.NotificationType.ToString(),
                        x.Status.Value,
                        x.CreatedOn))
                    .Take(30)
                    .ToList()
                ));

            if (isNoTracking)
            {
                Query.AsNoTracking();
            }

            Query.Include(x => x.Notifications.Where(c => c.Status == Status.Active || c.Status == Status.Inactive));

            Query.Where(x => x.Status == Status.Active && x.Id == userId);

            Query.AsSplitQuery();

        }
    }
}