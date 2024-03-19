using Investors.Shared.Domain.ValueObjects;

namespace Investors.Administrator.Users.Domain.ValueObjects
{
    public record Icon : IValueObject
    {
        public enum IconType
        {
            Dashboard,
            QrCodeScanner,
            Domain,
            Feed,
            SupervisorAccount,
            FastFood,
            BedTime,
            Settings,
            Logout,
            Summarize
        }

        public static readonly Icon Dashboard = new(IconType.Dashboard);
        public static readonly Icon QrCodeScanner = new(IconType.QrCodeScanner);
        public static readonly Icon Domain = new(IconType.Domain);
        public static readonly Icon Feed = new(IconType.Feed);
        public static readonly Icon SupervisorAccount = new(IconType.SupervisorAccount);
        public static readonly Icon FastFood = new(IconType.FastFood);
        public static readonly Icon BedTime = new(IconType.BedTime);
        public static readonly Icon Settings = new(IconType.Settings);
        public static readonly Icon Summarize = new(IconType.Summarize);
        public static readonly Icon Logout = new(IconType.Logout);

        public static readonly Icon[] AllIcons =
        {
            Dashboard, QrCodeScanner, Domain, Feed, SupervisorAccount, FastFood, BedTime, Settings, Summarize, Logout
        };

        protected Icon()
        {

        }
        protected Icon(IconType value)
        {

            switch (value)
            {
                case IconType.Dashboard:
                    Value = "dashboard";
                    break;

                case IconType.QrCodeScanner:
                    Value = "qr_code_scanner";
                    break;

                case IconType.Domain:
                    Value = "domain";
                    break;

                case IconType.Feed:
                    Value = "feed";
                    break;

                case IconType.SupervisorAccount:
                    Value = "supervisor_account";
                    break;

                case IconType.Settings:
                    Value = "settings";
                    break;

                case IconType.BedTime:
                    Value = "bedtime";
                    break;

                case IconType.FastFood:
                    Value = "fastfood";
                    break;

                case IconType.Summarize:
                    Value = "summarize";
                    break;

                case IconType.Logout:
                    Value = "logout";
                    break;

                default:
                    Value = "home";
                    break;
            }
        }
        public string Value { get; }

        public static Icon From(string i)
        {
            var icon = AllIcons.SingleOrDefault(x => x.Value == i);

            if (icon is null)
            {
                return AllIcons[0];
            }

            return icon;
        }
    }
}