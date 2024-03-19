namespace Investors.Client.Users.Domain.Aggregate
{
    internal static class UserValidationConstants
    {
        public const string UserNotFound = "El Usuario con id: {0} no fue encontrado!";

        public const string UserNotAllowedToEnterInvoice = "Solo pueden agregar facturas los usuarios Inversionistas";

        public const string UserNotAllowedToReedemCashback = "Solo pueden canjear cashback los usuarios Inversionistas";
        public const string UserNotAllowedToReedemNight = "Solo pueden canjear noches los usuarios Inversionistas";


    }
}