using Investors.Client.Users.Domain.ValueObjects;

namespace Investors.Client.Users.Domain.Aggregate
{
    public record UserIncludeNotificationsByUserIdResponse(
       Guid Id,
       List<NotificationsByUserIdResponse> Notifications);

    public record NotificationsByUserIdResponse(
       int Id,
       string Title,
       string? SubTitle,
       string Description,
       string NotificationType,
       int Status,
       DateTime CreatedOn);
}
