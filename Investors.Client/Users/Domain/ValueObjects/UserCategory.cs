namespace Investors.Client.Users.Domain.ValueObjects
{
    public class UserCategory : IValueObject
    {
        public enum UserCategoryEnum
        {
            Started = 3,
            FourStar = 4,
            FiveStar = 5,
            SixStar = 6
        }

        public static readonly UserCategory Started = new(UserCategoryEnum.Started);
        public static readonly UserCategory FourStar = new(UserCategoryEnum.FourStar);
        public static readonly UserCategory FiveStar = new(UserCategoryEnum.FiveStar);
        public static readonly UserCategory SixStar = new(UserCategoryEnum.SixStar);

        public static readonly UserCategory[] AllUserCategory =
        {
            Started, FourStar, FiveStar, SixStar
        };

        protected UserCategory()
        {

        }
        protected UserCategory(UserCategoryEnum value)
        {
            Value = (int)value;
        }
        public int Value { get; }

        public static UserCategory From(int i)
        {
            return AllUserCategory.Single(x => x.Value == i);
        }

        public override string ToString()
        {
            switch (Value)
            {
                case (int)UserCategoryEnum.Started:
                    return "Star";
                case (int)UserCategoryEnum.FourStar:
                    return "FourStar";
                case (int)UserCategoryEnum.FiveStar:
                    return "FiveStar";
                case (int)UserCategoryEnum.SixStar:
                    return "SixStar";
                default:
                    return "Usuario sin categoria";
            }
        }
    }
}