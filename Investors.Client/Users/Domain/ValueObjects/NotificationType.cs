namespace Investors.Client.Users.Domain.ValueObjects
{
    public class NotificationType : IValueObject
    {
        public enum NotificationTypeEnum
        {
            Points = 1,
            Cashback = 2,
            Nights = 3,
            News = 4,
            General = 5,
        }

        public static readonly NotificationType Points = new(NotificationTypeEnum.Points);
        public static readonly NotificationType Cashback = new(NotificationTypeEnum.Cashback);
        public static readonly NotificationType Nights = new(NotificationTypeEnum.Nights);
        public static readonly NotificationType News = new(NotificationTypeEnum.News);
        public static readonly NotificationType General = new(NotificationTypeEnum.General);

        public static readonly NotificationType[] AllNotificationType =
        {
            Points, Cashback, Nights, News, General
        };

        protected NotificationType()
        {

        }
        protected NotificationType(NotificationTypeEnum value)
        {
            Value = (int)value;
        }
        public int Value { get; }

        public static NotificationType From(int i)
        {
            return AllNotificationType.Single(x => x.Value == i);
        }

        public override string ToString()
        {
            switch (Value)
            {
                case (int)NotificationTypeEnum.Points:
                    return "points";
                case (int)NotificationTypeEnum.Cashback:
                    return "cashback";
                case (int)NotificationTypeEnum.Nights:
                    return "nights";
                case (int)NotificationTypeEnum.News:
                    return "notice";
                case (int)NotificationTypeEnum.General:
                    return "general";
                default:
                    return "No existe este tipo";
            }
        }
    }
}