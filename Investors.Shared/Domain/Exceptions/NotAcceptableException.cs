namespace Investors.Shared.Domain.Exceptions
{
    public abstract class NotAcceptableException : AppException
    {
        protected NotAcceptableException(string message)
            : base(message)
        {
        }
    }
}