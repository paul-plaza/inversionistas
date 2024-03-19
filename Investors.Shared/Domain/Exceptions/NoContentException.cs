namespace Investors.Shared.Domain.Exceptions
{
    public abstract class NoContentException : AppException
    {
        protected NoContentException(string message)
            : base(message)
        {
        }
    }
}