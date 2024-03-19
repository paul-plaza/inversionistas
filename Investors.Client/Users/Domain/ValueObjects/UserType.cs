namespace Investors.Client.Users.Domain.ValueObjects
{
    public record UserType : IValueObject
    {
        public enum UserTypeEnum
        {
            Invited = 1,
            Investor = 2
        }

        public static readonly UserType Invited = new(UserTypeEnum.Invited);
        public static readonly UserType Investor = new(UserTypeEnum.Investor);

        public static readonly UserType[] AllUserTypes =
        {
            Invited, Investor
        };

        protected UserType()
        {

        }
        protected UserType(UserTypeEnum value)
        {
            Value = (int)value;
        }
        public int Value { get; }

        public static UserType From(int i)
        {
            return AllUserTypes.Single(x => x.Value == i);
        }

        public override string ToString()
        {
            switch (Value)
            {
                case (int)UserTypeEnum.Invited:
                    return "Invitado";
                case (int)UserTypeEnum.Investor:
                    return "Inversionista";
                default:
                    return "Usuario sin tipo";
            }
        }
    }
}