namespace Investors.Client.Users.Infrastructure.Notifications
{
    public interface IManagementNotifications
    {
        Task<Result> SentEmailToUserId(Guid userId, Guid templateId, string subject, string to, CancellationToken cancellationToken);
        Task<Result> SentEmailToUserId(Guid userId, Guid templateId, string subject, CancellationToken cancellationToken);
        Task<Result> SentEmailToUserId(Guid userId, Guid templateId, string subject, string to, object item, CancellationToken cancellationToken);
        Task<Result> SentEmailToUserId(Guid userId, Guid templateId, string subject, object item, CancellationToken cancellationToken);

        Task<Result> SentPushToUserId(Guid userId, Guid templateId, CancellationToken cancellationToken);

        Task<Result> SentPushToUserId(Guid userId, Guid templateId, object item, CancellationToken cancellationToken);

        Task<Result> EditUserTag(Guid userId, object item, CancellationToken cancellationToken);

        Task<Result> SentEmailToAdmin(Guid templateId, string subject, string to, object item, CancellationToken cancellationToken);

    }
}