namespace Investors.Shared.Domain.Exceptions
{
    public abstract class UserForbiddenException : AppException
    {
        protected UserForbiddenException(string message)
            : base(message)
        {
        }
    }
}