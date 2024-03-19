using Investors.Client.Users.Domain.Aggregate;
using Investors.Client.Users.Domain.ValueObjects;

namespace Investors.Client.Users.Domain.Entities.Notifications
{
    public class Notification : BaseEntity<int>
    {
        public Guid UserId { get; private set; }
        public string Title { get; private set; } = string.Empty;
        public string? SubTitle { get; private set; } = string.Empty;
        public string Description { get; private set; } = string.Empty;
        public NotificationType NotificationType { get; private set; }

        public virtual User User { get; private set; }

        public int? OperationId { get; private set; }

        protected Notification()
        {
        }

        private Notification(Guid userId,
            string title,
            string? subTitle,
            string description,
            NotificationType notificationType,
            int? operationId,
            Guid createBy,
            DateTime createOn,
            Status status) : base(createBy, createOn, status)
        {
            UserId = userId;
            Title = title;
            SubTitle = subTitle;
            Description = description;
            NotificationType = notificationType;
            OperationId = operationId;
        }

        public static Result<Notification> CreateNotification(
            Guid userId,
            string title,
            string? subTitle,
            string description,
            NotificationType notificationType,
            int? operationId,
            Guid createBy)
        {
            Guard.Against.NullOrWhiteSpace(title);
            Guard.Against.NullOrWhiteSpace(description);

            if (Errors.Any())
                return Result.Failure<Notification>(GetErrors());

            return Result.Success(new Notification(
                userId,
                title,
                subTitle,
                description,
                notificationType,
                operationId,
                createBy,
                DateTime.Now,
                Status.Active));
        }

        /// <summary>
        /// Lee la notificación seleccionada
        /// </summary>
        /// <param name="status">stado de leída</param>
        /// <param name="userInSession">usuario en sesion</param>
        /// <returns></returns>
        public Result ReadNotification(Status status, Guid userInSession)
        {
            Status = status;
            UpdatedBy = userInSession;
            UpdatedOn = DateTime.Now;
            return Result.Success();
        }
    }
}